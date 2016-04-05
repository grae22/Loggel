namespace Loggel
{
  public class Wire
  {
    //-------------------------------------------------------------------------

    // Socket this wire is connected to for input.
    // NOTE: May be null.
    public SocketIn SocketIn { get; set; }

    // Socket this wire is connected to for output.
    // NOTE: May be null.
    public SocketIn SocketOut { get; set; }

    //-------------------------------------------------------------------------

    public bool IsLive
    {
      get
      {
        bool isLive = false;

        if( SocketIn != null )
        {
          isLive = SocketIn.IsLive;
        }

        return isLive;
      }
    }

    //-------------------------------------------------------------------------
  }
}
