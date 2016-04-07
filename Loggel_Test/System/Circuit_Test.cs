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
    public void Routing()
    {
      // Circuit whose value is manipulated.
      Circuit valueManipulator = new Circuit( 0.0 );

      // Maths processor for adding 1 to the valueManipulator circuit value.
      Maths mathsAdd = new Maths( null );
      mathsAdd.Operator = '+';
      mathsAdd.Value2 = 1.0;
      valueManipulator.RegisterProcessor( mathsAdd, false );

      // Maths processor for subtracting 1 to the valueManipulator circuit value.
      Maths mathsSub = new Maths( null );
      mathsSub.Operator = '-';
      mathsSub.Value2 = 1.0;
      valueManipulator.RegisterProcessor( mathsSub, false );

      // Circuit which produces the comparison value we will use.
      Circuit comparisonValue = new Circuit( 1.0 );

      // Comparer processor for valueManipulator circuit.
      Comparer comparer = new Comparer( null );
      comparer.Circuit_ComparisonValue = comparisonValue;
      comparer.OutputSocket_NotEqual.ConnectedProcessor = mathsAdd;
      comparer.OutputSocket_Equal.ConnectedProcessor = mathsSub;
      valueManipulator.RegisterProcessor( comparer, true );

      // Process circuit:
      // Value is initially 0.0.
      // Comparer will test value against comparison-value and should find it not-equal.
      // Comparer's output-socket for not-equal is mathsAdd processor.
      // mathsAdd processor should add 1 to the value.
      // Value should now equal 1.
      valueManipulator.Process();
      Assert.AreEqual( 1.0, valueManipulator.Value );

      // Process circuit:
      // Value is currently 1.0.
      // Comparer will test value against comparison-value and should find it equal.
      // Comparer's output-socket for equal is mathsSub processor.
      // mathsSub processor should subtract 1 from the value.
      // Value should now equal 0.
      valueManipulator.Process();
      Assert.AreEqual( 0.0, valueManipulator.Value );

      // Process circuit:
      // Value is currently 0.0 (again).
      // Comparer will test value against comparison-value and should find it not-equal.
      // Comparer's output-socket for not-equal is mathsAdd processor.
      // mathsAdd processor should add 1 to the value.
      // Value should now equal 1 (again).
      valueManipulator.Process();
      Assert.AreEqual( 1.0, valueManipulator.Value );
    }

    //-------------------------------------------------------------------------
  }
}
