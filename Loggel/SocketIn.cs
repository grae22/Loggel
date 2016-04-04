using System;

namespace Loggel
{
  public class SocketIn
  {
    //-------------------------------------------------------------------------

    public Wire WireIn { get; set; }

    //-------------------------------------------------------------------------

    public bool IsLive
    {
      get
      {
        bool isLive = false;

        if( WireIn != null )
        {
          isLive = WireIn.IsLive;
        }

        return isLive;
      }
    }

    //-------------------------------------------------------------------------
  }
}
