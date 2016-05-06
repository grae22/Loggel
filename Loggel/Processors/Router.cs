using System.Xml;
using System.Collections.Generic;

namespace Loggel.Processors
{
  public class Router : Processor
  {
    //-------------------------------------------------------------------------

    public Router(
      uint id,
      string name,
      string description,
      CircuitContext circuitContext )
    :
      base( id, name, description, circuitContext )
    {

    }

    //-------------------------------------------------------------------------

    public override Processor Process()
    {
      return null;
    }

    //-------------------------------------------------------------------------
  }
}
