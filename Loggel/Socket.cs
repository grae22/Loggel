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

    public Socket( string name )
    :
      base( name )
    {

    }

    //-------------------------------------------------------------------------

    public void Process( Circuit.CircuitContext context )
    {
      if( ConnectedProcessor != null )
      {
        ConnectedProcessor.Process();
      }
    }

    //-------------------------------------------------------------------------

    public override void PerformSnapshot()
    {
      // Currently there is nothing we want to save.
    }

    //-------------------------------------------------------------------------
  }
}
