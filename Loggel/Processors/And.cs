using System.Collections.Generic;
using Loggel.System;

namespace Loggel.Processors
{
  public class And : Processor
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

    // List of conditions's which we'll 'and' when processing.
    public List<Condition> Conditions { get; set; } = new List<Condition>();

    //-------------------------------------------------------------------------
    // METHODS.

    public And()
    {
      OutputSocket = AddOutputSocket( "Result", "Will be live when AND condition is satisfied." );
    }

    //-------------------------------------------------------------------------
    // If all the conditions are met then we can return the connected
    // processor (if there is one) as the next processor to be processed.

    public override Processor Process( Circuit.CircuitContext context )
    {
      Processor nextProcessor = null;
      bool allConditionsMet = true;

      // Evaluate each condition.
      foreach( Condition condition in Conditions )
      {
        if( condition.Circuit != null &&
            condition.TestValue != null )
        {
          if( condition.Circuit.Value != condition.TestValue )
          {
            allConditionsMet = false;
            break;
          }
        }
      }

      if( allConditionsMet )
      {
        nextProcessor = OutputSocket.ConnectedProcessor;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------
  }
}
