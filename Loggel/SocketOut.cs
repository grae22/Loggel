using System;

namespace Loggel
{
  public class SocketOut
  {
    //-------------------------------------------------------------------------

    // The wire that is connected to this socket.
    // NOTE: May be null.
    public Wire Wire { get; set; }
      
    // TODO: Shouldn't store the state! That's why we're resetting it after a get.
    private bool m_isLive;

    //-------------------------------------------------------------------------

    public bool IsLive
    {
      get
      {
        bool tmp = m_isLive;
        m_isLive = false;
        return tmp;
      }

      set
      {
        m_isLive = value;
      }
    }

    //-------------------------------------------------------------------------
  }
}
