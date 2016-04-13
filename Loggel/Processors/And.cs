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
      public Circuit ValueSource { get; set; }
      public dynamic ComparisonValue { get; set; }
      public ValueComparison.Comparison ComparisonType { get; set; }
    }

    //-------------------------------------------------------------------------

    // This processor's output socket.
    public Socket OutputSocket { get; set; }

    // List of conditions's which we'll 'and' when processing.
    public List<Condition> Conditions { get; set; } = new List<Condition>();

    //-------------------------------------------------------------------------

    public And(
      string name,
      Circuit.CircuitContext circuitContext )
    :
      base( name, circuitContext )
    {
      OutputSocket = GetNewOutputSocket( "Result", "Will be live when AND condition is satisfied." );
    }

    //-------------------------------------------------------------------------
    
    // If all the conditions are met then we can return the connected
    // processor (if there is one) as the next processor to be processed.

    public override Processor Process()
    {
      Processor nextProcessor = null;
      bool allConditionsMet = true;

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
        nextProcessor = OutputSocket.ConnectedProcessor;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      base.GetAsXml( parent );

      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement andElement = ownerDoc.CreateElement( "And" );
      parent.AppendChild( andElement );

      // TODO: Conditions.
      
      return andElement; 
    }

    //-------------------------------------------------------------------------
  }
}
