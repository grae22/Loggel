using System.Collections.Generic;
using System.Xml;
using Loggel.Helpers;

namespace Loggel.Processors
{
  public class And : Processor
  {
    //-------------------------------------------------------------------------

    public struct Condition
    {
      public CircuitContext ValueSource { get; set; }
      public dynamic ComparisonValue { get; set; }
      public ValueComparison.Comparison ComparisonType { get; set; }
    }

    //-------------------------------------------------------------------------

    // The processor connected to this processor.
    public Processor ConnectedProcessor { get; set; }

    // List of conditions's which we'll 'and' when processing.
    public List<Condition> Conditions { get; set; } = new List<Condition>();

    //-------------------------------------------------------------------------

    public And(
      uint id,
      string name,
      string description,
      CircuitContext circuitContext )
    :
      base( id, name, description, circuitContext )
    {

    }

    //-------------------------------------------------------------------------
    
    // If all the conditions are met then we can return the connected
    // processor (if there is one) as the next processor to be processed.

    override public Component Process()
    {
      Processor nextProcessor = null;
      bool allConditionsMet = true;

      // Must call base class.
      base.Process();

      // Evaluate each condition.
      foreach( Condition condition in Conditions )
      {
        bool result =
          ValueComparison.Compare(
            condition.ValueSource.Value,
            condition.ComparisonValue,
            condition.ComparisonType );

        if( result == false )
        {
          allConditionsMet = false;
          break;
        }
      }

      if( allConditionsMet )
      {
        nextProcessor = ConnectedProcessor;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Compile a map of connected processors.
      Dictionary<string, Processor> processors = new Dictionary<string, Processor>();
      processors.Add( "default", ConnectedProcessor );

      // Must call base method.
      parent = base.GetAsXml( parent, processors );

      // 'And' processor xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement andElement = ownerDoc.CreateElement( "And" );
      parent.AppendChild( andElement );

      // Conditions.
      XmlElement conditionCollection = ownerDoc.CreateElement( "ConditionCollection" );
      andElement.AppendChild( conditionCollection );

      foreach( Condition condition in Conditions )
      {
        XmlElement conditionElement = ownerDoc.CreateElement( "Condition" );
        conditionCollection.AppendChild( conditionElement );

        XmlElement valueSourceIdElement = ownerDoc.CreateElement( "ValueSourceId" );
        conditionElement.AppendChild( valueSourceIdElement );
        if( condition.ValueSource != null )
        {
          valueSourceIdElement.InnerText = condition.ValueSource.Id.ToString();
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
      
      return andElement; 
    }

    //-------------------------------------------------------------------------

    // Restore this instance from xml.

    public override XmlElement RestoreFromXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.RestoreFromXml( parent );

      // 'And' processor.
      XmlElement andElement = parent[ "And" ];

      // Connected processor.
      ConnectedProcessor = GetConnectedProcessor( "default" );

      // Condition collection.
      XmlElement conditionCollection = andElement[ "ConditionCollection" ];

      foreach( XmlElement conditionElement in conditionCollection.SelectNodes( "Condition" ) )
      {
        Condition condition = new Condition();

        condition.ValueSource =
          CircuitContext.AllContexts[
            uint.Parse( conditionElement[ "ValueSourceId" ].InnerText ) ];

        condition.ComparisonValue = conditionElement[ "ComparisonValue" ].InnerText;

        condition.ComparisonType =
          ValueComparison.ComparisonFromString(
            conditionElement[ "ComparisonType" ].InnerText );
      }

      return andElement;
    }

    //-------------------------------------------------------------------------
  }
}
