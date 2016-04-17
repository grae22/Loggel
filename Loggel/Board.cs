using System.Collections.Generic;

namespace Loggel
{
  public class Board
  {
    //-------------------------------------------------------------------------

    public Dictionary<uint, Circuit> Circuits { get; private set; } = new Dictionary<uint, Circuit>();

    //-------------------------------------------------------------------------
  }
}
