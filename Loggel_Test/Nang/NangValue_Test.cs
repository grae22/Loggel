using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using Loggel.Nang;

namespace Loggel_Test
{
  [TestClass]
  public class NangValue_Test
  {
    //-------------------------------------------------------------------------

    [TestInitialize]
    public void Initialise()
    {

    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangValue_InstantiateFromXml()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load( "../../Resources/Nang/NangValue_InstantiateFromXml.xml" );
      XmlElement element = doc[ "NangValue" ];

      NangValue testOb = new NangValue( element );
    }

    //-------------------------------------------------------------------------
  }
}
