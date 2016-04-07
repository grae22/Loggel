using Loggel;

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

    public Maths()
    {
      OutputSocket =
        AddOutputSocket( "Result", "Result of mathematical operation." );
    }

    //-------------------------------------------------------------------------

    public override Processor Process( Circuit.CircuitContext context )
    {
      // Perform operation on circuit value.
      switch( Operator )
      {
        case '=':
          context.Value = Value2;
          break;

        case '+':
          context.Value += Value2;
          break;

        case '-':
          context.Value -= Value2;
          break;

        case 'x':
          context.Value *= Value2;
          break;

        case '/':
          context.Value /= Value2;
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
  }
}