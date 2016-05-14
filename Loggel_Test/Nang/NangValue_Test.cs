using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.IO;
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
    public void NangValueState_InstantiateFromXml()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load( "../../Resources/Nang/NangValue_InstantiateFromXml.xml" );
      XmlElement element = doc[ "NangValue" ];

      NangValue testOb = new NangValueState( element );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangValueState_WriteAsXml()
    {
      // Create a test object.
      NangValueState testOb = new NangValueState();
      testOb.Name = "Test Name";
      testOb.ValueType = NangValue.NangValueType.STATE;
      testOb.Unit = "Test Unit";
      testOb.StateNames.Add( "State 1" );
      testOb.StateNames.Add( "State 2" );
      testOb.SetValue( "State 2" );

      // Write the test object as xml to file.
      XmlElement xml;
      testOb.GetAsXml( out xml );
      XmlDocument doc = new XmlDocument();
      doc.AppendChild( doc.ImportNode( xml, true ) );
      doc.Save( "NangValueState_WriteAsXml.xml" );

      // Load the reference & test object from file.
      string reference =
        File.ReadAllText( 
          "../../Resources/Nang/NangValue_InstantiateFromXml.xml" );

      string content =
        File.ReadAllText( 
          "NangValueState_WriteAsXml.xml" );

      Assert.AreEqual( reference, content );
    }

    //-------------------------------------------------------------------------
  }
}
