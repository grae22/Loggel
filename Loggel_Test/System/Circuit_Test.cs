﻿using System.Reflection;
using System.Xml;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;

namespace Loggel_Test
{
  [TestClass]
  public class Circuit_Test
  {
    //-------------------------------------------------------------------------

    private Circuit m_valueManipulator;

    //-------------------------------------------------------------------------

    [TestInitialize]
    public void Initialise()
    {
      // Circuit whose value is manipulated.
      m_valueManipulator = new Circuit( "TestCircuit", "", 0.0 );

      // Maths processor for adding 1 to the valueManipulator circuit value.
      Maths mathsAdd = m_valueManipulator.Context.CreateComponent<Maths>( "Add", "" );
      mathsAdd.Operator = '+';
      mathsAdd.Value2 = 1.0;

      // Maths processor for subtracting 1 to the valueManipulator circuit value.
      Maths mathsSub = m_valueManipulator.Context.CreateComponent<Maths>( "Subtract", "" );
      mathsSub.Operator = '-';
      mathsSub.Value2 = 1.0;

      // Circuit which produces the comparison value we will use.
      Circuit comparisonValue = new Circuit( "ComparisonValue", "", 1.0 );

      // Comparer processor for valueManipulator circuit.
      Comparer comparer = m_valueManipulator.Context.CreateComponent<Comparer>( "Comparer", "" );
      comparer.ComparisonValueSource = comparisonValue.Context;
      comparer.Processor_NotEqual = mathsAdd;
      comparer.Processor_Equal = mathsSub;
      m_valueManipulator.EntryProcessor = comparer;
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Routing()
    {
      // Process circuit:
      // Value is initially 0.0.
      // Comparer will test value against comparison-value and should find it not-equal.
      // Comparer's output-socket for not-equal is mathsAdd processor.
      // mathsAdd processor should add 1 to the value.
      // Value should now equal 1.
      m_valueManipulator.Process();
      Assert.AreEqual( 1.0, m_valueManipulator.Context.Value );

      // Process circuit:
      // Value is currently 1.0.
      // Comparer will test value against comparison-value and should find it equal.
      // Comparer's output-socket for equal is mathsSub processor.
      // mathsSub processor should subtract 1 from the value.
      // Value should now equal 0.
      m_valueManipulator.Process();
      Assert.AreEqual( 0.0, m_valueManipulator.Context.Value );

      // Process circuit:
      // Value is currently 0.0 (again).
      // Comparer will test value against comparison-value and should find it not-equal.
      // Comparer's output-socket for not-equal is mathsAdd processor.
      // mathsAdd processor should add 1 to the value.
      // Value should now equal 1 (again).
      m_valueManipulator.Process();
      Assert.AreEqual( 1.0, m_valueManipulator.Context.Value );
    }

    //-------------------------------------------------------------------------

    // Test that the xml that is generated to persist the circuit matches
    // what we're expecting.

    [TestMethod]
    public void GetAsXml()
    {
      string path =
        Path.GetDirectoryName(
          Assembly.GetExecutingAssembly().Location ) + @"\Circuit_Test.GetAsXml\";

      try
      {
        Directory.Delete( path, true );
      }
      catch { }

      CircuitBuilder.Save( path, m_valueManipulator );

      //string content = File.ReadAllText( "Circuit_Test.GetAsXml.xml" );
      //string reference = File.ReadAllText( @"..\..\Resources\Circuit_Test.GetAsXml.xml" );
      //Assert.AreEqual( reference, content );
    }

    //-------------------------------------------------------------------------

    // Test that we can restore a circuit from xml.

    [TestMethod]
    public void RestoreFromXml()
    {
      Circuit circuit =
        CircuitBuilder.Load( @"..\..\Resources\TestCircuit" );

      // Now dump it back to xml file, reload and test everything's the same.
      //XmlDocument xmlDoc = new XmlDocument();
      //XmlElement rootElement = xmlDoc.CreateElement( "Root" );
      //xmlDoc.AppendChild( rootElement );
      //circuit.GetAsXml( rootElement );
      //xmlDoc.Save( "Circuit_Test.GetAsXml.xml" );

      //string content = File.ReadAllText( "Circuit_Test.GetAsXml.xml" );
      //string reference = File.ReadAllText( @"..\..\Resources\Circuit_Test.GetAsXml.xml" );
      //Assert.AreEqual( reference, content );
    }

    //-------------------------------------------------------------------------
  }
}
