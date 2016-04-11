using System.Collections.Generic;
using Loggel.Helpers;
using Siril;

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
      Circuit.CircuitContext circuitContext )
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

    public override void PerformSnapshot( List<SirilObject> children )
    {
      base.PerformSnapshot( children );

      children.Add( OutputSocket );
    }

    //-------------------------------------------------------------------------
    public override void RestoreSnapshot()
    {
      base.RestoreSnapshot();

      // TODO: Children.
    }

    //-------------------------------------------------------------------------
  }
}
