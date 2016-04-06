using System.Collections.Generic;
using Loggel.System;

namespace Loggel.Processors
{
  public class Or : Processor
  {
    //-------------------------------------------------------------------------
    // TYPES.

    public class Condition
    {
      public Circuit Circuit { get; set; }
      public dynamic TestValue { get; set; }
    }

    //-------------------------------------------------------------------------
    // PROPERTIES.

    // This processor's output socket.
    public Socket OutputSocket { get; set; }

    // List of conditions's which we'll 'or' when processing.
    public List<Condition> Conditions { get; set; } = new List<Condition>();

    //-------------------------------------------------------------------------
    // METHODS.

    public Or()
    {
      OutputSocket = AddOutputSocket( "Result", "Will be live when OR condition is satisfied." );
    }

    //-------------------------------------------------------------------------
    // If all the conditions are met then we can return the connected
    // processor (if there is one) as the next processor to be processed.

    public override Processor Process( Circuit.CircuitContext context )
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
        if( condition.Circuit != null &&
            condition.TestValue != null )
        {
          if( condition.Circuit.Value == condition.TestValue )
          {
            anyConditionsMet = true;
            break;
          }
        }
      }

      if( anyConditionsMet )
      {
        nextProcessor = OutputSocket.ConnectedProcessor;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------
  }
}
