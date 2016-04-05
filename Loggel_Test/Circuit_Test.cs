using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;

namespace Loggel_Test
{
  [TestClass]
  public class Circuit_Test
  {
    //-------------------------------------------------------------------------

    [TestMethod]
    public void MathsTest()
    {
      Circuit<double> deltaTime = new Circuit<double>( 1.0 );
      Circuit<double> timeCheck = new Circuit<double>( 0.0 );
      Router<double> router = new Router<double>();
      Maths<double> maths = new Maths<double>();
      Maths<double> maths2 = new Maths<double>();

      timeCheck.EntryProcessor = router;

      router.Circuit_ComparisonValue = deltaTime;
      router.OutSocket_NotEqual.ConnectedProcessor = maths;
      router.OutSocket_Equal.ConnectedProcessor = maths;

      maths.Operator = '+';
      maths.Value2 = 1.0;
      timeCheck.Process();
      Assert.AreEqual( 1.0, timeCheck.Value );

      maths.Operator = 'x';
      maths.Value2 = 10.0;
      timeCheck.Process();
      Assert.AreEqual( 10.0, timeCheck.Value );

      maths.Operator = '/';
      maths.Value2 = 2.0;
      timeCheck.Process();
      Assert.AreEqual( 5.0, timeCheck.Value );

      maths.Operator = '-';
      maths.Value2 = 5.0;
      timeCheck.Process();
      Assert.AreEqual( 0.0, timeCheck.Value );

      maths.OutputSocket.ConnectedProcessor = maths2;
      maths.Operator = '+';
      maths.Value2 = 0.0;
      maths2.Operator = '+';
      maths2.Value2 = 123.4;
      timeCheck.Process();
      Assert.AreEqual( 123.4, timeCheck.Value );
    }

    //-------------------------------------------------------------------------
  }
}
