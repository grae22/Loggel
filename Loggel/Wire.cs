namespace Loggel
{
  class Wire
  {
    //-------------------------------------------------------------------------

    public SocketIn SocketIn { get; set; }

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
