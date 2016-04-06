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

      // Simple addition test.
      maths.Operator = '+';
      maths.Value2 = 1.0;
      timeCheck.Process();
      Assert.AreEqual( 1.0, timeCheck.Value );

      // Simple multiplication test.
      maths.Operator = 'x';
      maths.Value2 = 10.0;
      timeCheck.Process();
      Assert.AreEqual( 10.0, timeCheck.Value );

      // Simple division test.
      maths.Operator = '/';
      maths.Value2 = 2.0;
      timeCheck.Process();
      Assert.AreEqual( 5.0, timeCheck.Value );

      // Simple subtraction test.
      maths.Operator = '-';
      maths.Value2 = 5.0;
      timeCheck.Process();
      Assert.AreEqual( 0.0, timeCheck.Value );

      // Add an additional maths processor after the existing one, check
      // that the chain produces the correct result.
      maths.OutputSocket.ConnectedProcessor = maths2;
      maths.Operator = '+';
      maths.Value2 = 1000.0;
      maths2.Operator = '+';
      maths2.Value2 = 123.4;
      timeCheck.Process();
      Assert.AreEqual( 1123.4, timeCheck.Value );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void RoutingTest()
    {
      // Maths processor for adding 1 to the valueManipulator circuit value.
      Maths<double> mathsAdd = new Maths<double>();
      mathsAdd.Operator = '+';
      mathsAdd.Value2 = 1.0;

      // Maths processor for subtracting 1 to the valueManipulator circuit value.
      Maths<double> mathsSub = new Maths<double>();
      mathsSub.Operator = '-';
      mathsSub.Value2 = 1.0;

      // Circuit which produces the comparison value we will use.
      Circuit<double> comparisonValue = new Circuit<double>( 1.0 );

      // Router processor for valueManipulator circuit.
      Router<double> router = new Router<double>();
      router.Circuit_ComparisonValue = comparisonValue;
      router.OutSocket_NotEqual.ConnectedProcessor = mathsAdd;
      router.OutSocket_Equal.ConnectedProcessor = mathsSub;

      // Circuit whose value is manipulated.
      Circuit<double> valueManipulator = new Circuit<double>( 0.0 );
      valueManipulator.EntryProcessor = router;

      // Process circuit:
      // Value is initially 0.0.
      // Router will test value against comparison-value and should find it not-equal.
      // Router's output-socket for not-equal is mathsAdd processor.
      // mathsAdd processor should add 1 to the value.
      // Value should now equal 1.
      valueManipulator.Process();
      Assert.AreEqual( 1.0, valueManipulator.Value );

      // Process circuit:
      // Value is currently 1.0.
      // Router will test value against comparison-value and should find it equal.
      // Router's output-socket for equal is mathsSub processor.
      // mathsSub processor should subtract 1 from the value.
      // Value should now equal 0.
      valueManipulator.Process();
      Assert.AreEqual( 0.0, valueManipulator.Value );

      // Process circuit:
      // Value is currently 0.0 (again).
      // Router will test value against comparison-value and should find it not-equal.
      // Router's output-socket for not-equal is mathsAdd processor.
      // mathsAdd processor should add 1 to the value.
      // Value should now equal 1 (again).
      valueManipulator.Process();
      Assert.AreEqual( 1.0, valueManipulator.Value );
    }

    //-------------------------------------------------------------------------
  }
}
