using System.Collections.Generic;
using Siril;

namespace Loggel
{
  public class Socket : BasicCircuitEntity
  {
    //-------------------------------------------------------------------------

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

    public override void PerformSnapshot( List<SirilObject> children )
    {
      base.PerformSnapshot( children );

      children.Add( ConnectedProcessor );
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
