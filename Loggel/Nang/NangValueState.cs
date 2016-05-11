using System.Xml;
using System.Collections.Generic;

namespace Loggel.Nang
{
  internal class NangValueState : NangValue
  {
    //-------------------------------------------------------------------------

    public List<string> StateNames { get; private set; } = new List<string>();

    //-------------------------------------------------------------------------

    public NangValueState( XmlElement xml )
    :
    base( xml )
    {

    }

    //-------------------------------------------------------------------------
  }
}
