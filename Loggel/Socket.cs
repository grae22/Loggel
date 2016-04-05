using System;

namespace Loggel
{
  public class Socket<T> : BasicCircuitEntity
    where T : IComparable
  {
    //-------------------------------------------------------------------------

    public Processor<T> ConnectedProcessor { get; set; } = null;

    //-------------------------------------------------------------------------

    public bool IsConnected
    {
      get
      {
        return ( ConnectedProcessor != null );
      }
    }

    //-------------------------------------------------------------------------

    public void Process( Circuit<T>.CircuitContext context )
    {
      if( ConnectedProcessor != null )
      {
        ConnectedProcessor.Process( context );
      }
    }

    //-------------------------------------------------------------------------
  }
}
