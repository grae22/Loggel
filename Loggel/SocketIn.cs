using System;

namespace Loggel
{
  public class SocketIn
  {
    //-------------------------------------------------------------------------

    public Wire ConnectedWire { get; set; }

    //-------------------------------------------------------------------------

    public bool IsLive
    {
      get
      {
        bool isLive = false;

        if( ConnectedWire != null )
        {
          isLive = ConnectedWire.IsLive;
        }

        return isLive;
      }
    }

    //-------------------------------------------------------------------------
  }
}
