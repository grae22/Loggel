using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Loggel;
using Loggel.Processors;

namespace Loggel_Test.Processors
{
  [TestClass]
  public class RouterProcessor_Test
  {
    //-------------------------------------------------------------------------

    private Circuit TestCircuit { get; set; }
    private Router TestRouter { get; set; }

    //-------------------------------------------------------------------------

    [TestInitialize]
    public void Initialise()
    {
      Circuit.Reset();

      TestCircuit = ComponentFactory.CreateCircuit( "Circuit", 0 );

      TestRouter = TestCircuit.Context.CreateComponent<Router>( "Router", "" );
      TestCircuit.EntryProcessor = TestRouter;
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Router_NoRoute()
    {
      TestCircuit.Process();

      Assert.AreEqual(
          0,
          TestCircuit.Context.Value,
          "Signal should have been routed to any processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Router_OneRoute()
    {
      Maths maths = TestCircuit.Context.CreateComponent<Maths>( "Maths", "" );
      maths.Operator = '+';
      maths.Value2 = 123;

      Comparer comparer = TestCircuit.Context.CreateComponent<Comparer>( "Comparer", "" );
      comparer.ComparisonValue = 0;
      comparer.Processor_Equal = maths;

      TestRouter.Routes.Add( comparer );

      TestCircuit.Process();

      Assert.AreEqual( 123, TestCircuit.Context.Value );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Router_OneRouteWithMultipleSubRoutes()
    {
      Maths maths1 = TestCircuit.Context.CreateComponent<Maths>( "Maths1", "" );
      maths1.Operator = '+';
      maths1.Value2 = 123;

      Maths maths2 = TestCircuit.Context.CreateComponent<Maths>( "Maths2", "" );
      maths2.Operator = '-';
      maths2.Value2 = 123;

      Comparer comparer1 = TestCircuit.Context.CreateComponent<Comparer>( "Comparer", "" );
      comparer1.Processor_Equal = maths1;
      comparer1.Processor_NotEqual = maths2;

      TestRouter.Routes.Add( comparer1 );

      // Test 1 - circuit value and comparison value match, so route should add 123.
      comparer1.ComparisonValue = 0;

      TestCircuit.Process();

      Assert.AreEqual( 123, TestCircuit.Context.Value );

      // Test 2 - circuit value and comparison value do not match, so route should sub 123.
      comparer1.ComparisonValue = 999;

      TestCircuit.Process();

      Assert.AreEqual( 0, TestCircuit.Context.Value );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Router_TwoRoutes()
    {
      Maths maths1 = TestCircuit.Context.CreateComponent<Maths>( "Maths1", "" );
      maths1.Operator = '+';
      maths1.Value2 = 123;

      Maths maths2 = TestCircuit.Context.CreateComponent<Maths>( "Maths2", "" );
      maths2.Operator = '-';
      maths2.Value2 = 123;

      Comparer comparer1 = TestCircuit.Context.CreateComponent<Comparer>( "Comparer1", "" );
      comparer1.Processor_Equal = maths1;

      Comparer comparer2 = TestCircuit.Context.CreateComponent<Comparer>( "Comparer2", "" );
      comparer2.Processor_Equal = maths2;

      TestRouter.Routes.Add( comparer1 );
      TestRouter.Routes.Add( comparer2 );

      // Route 1.
      comparer1.ComparisonValue = 0;
      comparer2.ComparisonValue = 1;

      TestCircuit.Process();

      Assert.AreEqual( 123, TestCircuit.Context.Value );

      // Route 2.
      comparer1.ComparisonValue = 0;
      comparer2.ComparisonValue = 123;

      TestCircuit.Process();

      Assert.AreEqual( 0, TestCircuit.Context.Value );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Router_GetAsXml()
    {
      Comparer comparer1 = TestCircuit.Context.CreateComponent<Comparer>( "Comparer1", "" );
      TestRouter.Routes.Add( comparer1 );
      Comparer comparer2 = TestCircuit.Context.CreateComponent<Comparer>( "Comparer2", "" );
      TestRouter.Routes.Add( comparer2 );

      CircuitBuilder.Save(
        @".\Router_Test\",
        TestCircuit );

      string content = File.ReadAllText( @"Router_Test\Circuit\Component_Router_Router.xml" );
      string reference = File.ReadAllText( @"..\..\Resources\Router_Test\Circuit\Component_Router_Router.xml" );
      Assert.AreEqual( reference, content );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Router_RestoreFromXml()
    {
      List<Circuit> circuits;

      CircuitBuilder.Load(
        @"..\..\Resources\Router_Test\",
        out circuits );

      TestCircuit = circuits[ 0 ];

      Router_NoRoute();
    }

    //-------------------------------------------------------------------------
  }
}
