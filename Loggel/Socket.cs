namespace Loggel
{
  public class Socket : BasicCircuitEntity
  {
    //-------------------------------------------------------------------------

    // BasicCircuitEntity.
    public string Name { get; set; } = "Unnamed";
    public string Description { get; set; } = "";

    // The connected processor (if any).
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

    public Socket( string name )
    {
      Name = name;
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
  }
}
