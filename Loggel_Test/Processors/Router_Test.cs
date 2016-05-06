using Microsoft.VisualStudio.TestTools.UnitTesting;
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
      TestCircuit = ComponentFactory.CreateCircuit( "Circuit", 0 );

      Maths maths = TestCircuit.Context.CreateComponent<Maths>( "Maths", "" );
      maths.Operator = '+';
      maths.Value2 = 1;

      Comparer comparer = TestCircuit.Context.CreateComponent<Comparer>( "Comparer", "" );
      comparer.Processor_InRange = maths;

      TestRouter = TestCircuit.Context.CreateComponent<Router>( "Router", "" );
      //TestRouter.ConnectedProcessor = maths;
      TestCircuit.EntryProcessor = TestRouter;
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NoConditions()
    {
      TestCircuit.Process();
      Assert.AreEqual(
          0,
          TestCircuit.Context.Value,
          "Signal should have been routed to any processor." );
    }

    //-------------------------------------------------------------------------
  }
}
