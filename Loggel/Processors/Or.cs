using System.Collections.Generic;
using Loggel.Helpers;
using System.Xml;

namespace Loggel.Processors
{
  public class Or : Processor
  {
    //-------------------------------------------------------------------------

    public struct Condition
    {
      public Circuit ValueSource { get; set; }
      public dynamic ComparisonValue { get; set; }
      public ValueComparison.Comparison ComparisonType { get; set; }
    }

    //-------------------------------------------------------------------------

    // This processor's output socket.
    public Socket OutputSocket { get; set; }

    // List of conditions's which we'll 'or' when processing.
    public List<Condition> Conditions { get; set; } = new List<Condition>();

    //-------------------------------------------------------------------------

    public Or(
      string name,
      CircuitContext circuitContext )
    :
      base( name, circuitContext )
    {
      OutputSocket = GetNewOutputSocket( "Result", "Will be live when OR condition is satisfied." );
    }

    //-------------------------------------------------------------------------

    // If all the conditions are met then we can return the connected
    // processor (if there is one) as the next processor to be processed.

    public override Processor Process()
    {
      Processor nextProcessor = null;
      bool anyConditionsMet = false;

      // We let the signal pass through if there are no conditions.
      if( Conditions.Count == 0 )
      {
        anyConditionsMet = true;
      }

      // Evaluate each condition.
      foreach( Condition condition in Conditions )
      {
        bool result =
          ValueComparison.Compare(
            condition.ValueSource.Value,
            condition.ComparisonValue,
            condition.ComparisonType );

        if( result == true )
        {
          anyConditionsMet = true;
          break;
        }
      }

      if( anyConditionsMet )
      {
        nextProcessor = OutputSocket.ConnectedProcessor;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.GetAsXml( parent );

      // 'Or' processor xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement orElement = ownerDoc.CreateElement( "Or" );
      parent.AppendChild( orElement );

      // Conditions.
      XmlElement conditionCollection = ownerDoc.CreateElement( "ConditionCollection" );
      orElement.AppendChild( conditionCollection );

      foreach( Condition condition in Conditions )
      {
        XmlElement conditionElement = ownerDoc.CreateElement( "Condition" );
        conditionCollection.AppendChild( conditionElement );

        XmlElement valueSourceNameElement = ownerDoc.CreateElement( "ValueSourceName" );
        conditionElement.AppendChild( valueSourceNameElement );
        if( condition.ValueSource != null )
        {
          valueSourceNameElement.InnerText = condition.ValueSource.Name;
        }

        XmlElement comparisonValueElement = ownerDoc.CreateElement( "ComparisonValue" );
        conditionElement.AppendChild( comparisonValueElement );
        if( condition.ValueSource != null )
        {
          comparisonValueElement.InnerText = condition.ComparisonValue.ToString();
        }

        XmlElement comparisonTypeElement = ownerDoc.CreateElement( "ComparisonType" );
        comparisonTypeElement.AppendChild( comparisonValueElement );
        if( condition.ValueSource != null )
        {
          comparisonTypeElement.InnerText =
            ValueComparison.ComparisonAsString( condition.ComparisonType ) ?? "";
        }
      }
      
      return orElement; 
    }

    //-------------------------------------------------------------------------
  }
}
