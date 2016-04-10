using System.Collections.Generic;
using Siril;

namespace Loggel.Processors
{
  public class Maths : Processor
  {
    //-------------------------------------------------------------------------
    // PROPERTIES.

    public char Operator { get; set; }
    public dynamic Value2 { get; set; }
    public Socket OutputSocket { get; set; }

    //-------------------------------------------------------------------------
    // METHODS.

    public Maths(
      string name,
      Circuit.CircuitContext circuitContext )
    :
      base( name, circuitContext )
    {
      OutputSocket =
        GetNewOutputSocket( "Result", "Result of mathematical operation." );
    }

    //-------------------------------------------------------------------------

    public override Processor Process()
    {
      // Perform operation on circuit value.
      switch( Operator )
      {
        case '=':
          CircuitContext.Value = Value2;
          break;

        case '+':
          CircuitContext.Value += Value2;
          break;

        case '-':
          CircuitContext.Value -= Value2;
          break;

        case 'x':
          CircuitContext.Value *= Value2;
          break;

        case '/':
          CircuitContext.Value /= Value2;
          break;

        default:
          // TODO
          break;
      }

      // Return the next processor if we have one.
      Processor nextProcessor = null;

      if( OutputSocket.ConnectedProcessor != null )
      {
        nextProcessor = OutputSocket.ConnectedProcessor;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------

    public override void PerformSnapshot( List<SirilObject> children )
    {
      // Currently there is nothing we want to save.
    }

    //-------------------------------------------------------------------------
  }
}