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
    public void NangValue_Create()
    {
      NangValue ob = NangValue.Create( IValue.Type.STATE );
      Assert.AreEqual( typeof( NangValueState ), ob.GetType() );

      ob = NangValue.Create( IValue.Type.DECIMAL );
      Assert.AreEqual( typeof( NangValueDecimal ), ob.GetType() );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangValueState_InstantiateFromXml()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load( "../../Resources/Nang/NangValueState_InstantiateFromXml.xml" );
      XmlElement element = doc[ "NangValue" ];

      NangValue testOb = new NangValueState( element );

      Assert.AreEqual( "State 2", testOb.GetValue() );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangValueState_WriteAsXml()
    {
      // Create a test object.
      NangValueState testOb = new NangValueState();
      testOb.Name = "Test Name";
      testOb.ValueType = IValue.Type.STATE;
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
          "../../Resources/Nang/NangValueState_InstantiateFromXml.xml" );

      string content =
        File.ReadAllText( 
          "NangValueState_WriteAsXml.xml" );

      Assert.AreEqual( reference, content );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangValueDecimal_InstantiateFromXml()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load( "../../Resources/Nang/NangValueDecimal_InstantiateFromXml.xml" );
      XmlElement element = doc[ "NangValue" ];

      NangValue testOb = new NangValueDecimal( element );

      Assert.AreEqual( 1.23f, testOb.GetValue() );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangValueDecimal_WriteAsXml()
    {
      // Create a test object.
      NangValueDecimal testOb = new NangValueDecimal();
      testOb.Name = "Test Name";
      testOb.ValueType = IValue.Type.DECIMAL;
      testOb.Unit = "Test Unit";
      testOb.SetValue( 1.23f );

      // Write the test object as xml to file.
      XmlElement xml;
      testOb.GetAsXml( out xml );
      XmlDocument doc = new XmlDocument();
      doc.AppendChild( doc.ImportNode( xml, true ) );
      doc.Save( "NangValueDecimal_WriteAsXml.xml" );

      // Load the reference & test object from file.
      string reference =
        File.ReadAllText( 
          "../../Resources/Nang/NangValueDecimal_InstantiateFromXml.xml" );

      string content =
        File.ReadAllText( 
          "NangValueDecimal_WriteAsXml.xml" );

      Assert.AreEqual( reference, content );
    }

    //-------------------------------------------------------------------------
  }
}
