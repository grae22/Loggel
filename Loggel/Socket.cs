namespace Loggel
{
  public class Socket : BasicCircuitEntity
  {
    //-------------------------------------------------------------------------
    // PROPERTIES.

    public Processor ConnectedProcessor { get; set; } = null;

    //-------------------------------------------------------------------------

    public bool IsConnected
    {
      get
      {
        return ( ConnectedProcessor != null );
      }
    }

    //-------------------------------------------------------------------------
    // METHODS.

    public void Process( Circuit.CircuitContext context )
    {
      if( ConnectedProcessor != null )
      {
        ConnectedProcessor.Process();
      }
    }

    //-------------------------------------------------------------------------
  }
}
