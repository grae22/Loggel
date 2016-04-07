using System.Collections.Generic;
using Loggel;

namespace Loggel.Processors
{
  public class And : Processor
  {
    //-------------------------------------------------------------------------
    // PROPERTIES.

    // This processor's output socket.
    public Socket OutputSocket { get; set; }

    // List of conditions's which we'll 'and' when processing.
    public List<Comparer> Conditions { get; set; } = new List<Comparer>();

    //-------------------------------------------------------------------------
    // METHODS.

    public And()
    {
      OutputSocket = GetNewOutputSocket( "Result", "Will be live when AND condition is satisfied." );
    }

    //-------------------------------------------------------------------------
    // If all the conditions are met then we can return the connected
    // processor (if there is one) as the next processor to be processed.

    public override Processor Process( Circuit.CircuitContext context )
    {
      Processor nextProcessor = null;
      bool allConditionsMet = true;

      // Evaluate each condition.
      foreach( Comparer condition in Conditions )
      {
        // If the condition comparer's process method returns null then
        // the comparison has failed.
        if( condition.Process( context ) == null  )
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
  }
}
