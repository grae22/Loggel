using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;

namespace Loggel_Test
{
  [TestClass]
  public class Circuit_Test
  {
    //-------------------------------------------------------------------------
    // Test if setting a signal's value works.

    [TestMethod]
    public void ValueSet()
    {
      Circuit<double> deltaTime = new Circuit<double>( 1.0 );
      Circuit<double> timeCheck = new Circuit<double>( 0.0 );
      Router<double> processor = new Router<double>();
      timeCheck.EntryProcessor = processor;
      processor.Circuit_ComparisonValue = deltaTime;

      timeCheck.Process();

      //Assert.AreEqual(  )
    }

    //-------------------------------------------------------------------------
  }
}
