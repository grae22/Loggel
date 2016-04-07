using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;

namespace Loggel_Test
{
  [TestClass]
  public class MathsProcessor_Test
  {
    //-------------------------------------------------------------------------

    [TestMethod]
    public void Integer()
    {
      // Create a circuit.
      Maths maths = new Maths();
      Maths maths2 = new Maths();

      Circuit comparisonValue = new Circuit( 1 );

      Comparer comparer = new Comparer();
      comparer.Circuit_ComparisonValue = comparisonValue;
      comparer.OutputSocket_NotEqual.ConnectedProcessor = maths;
      comparer.OutputSocket_Equal.ConnectedProcessor = maths;

      Circuit valueManipulator = new Circuit( 0 );
      valueManipulator.EntryProcessor = comparer;

      // Simple addition test.
      maths.Operator = '+';
      maths.Value2 = 1;
      valueManipulator.Process();
      Assert.AreEqual( 1, valueManipulator.Value, "Addition failed." );

      // Simple multiplication test.
      maths.Operator = 'x';
      maths.Value2 = 10;
      valueManipulator.Process();
      Assert.AreEqual( 10, valueManipulator.Value, "Multiplication failed." );

      // Simple division test.
      maths.Operator = '/';
      maths.Value2 = 2;
      valueManipulator.Process();
      Assert.AreEqual( 5, valueManipulator.Value, "Division failed." );

      // Simple subtraction test.
      maths.Operator = '-';
      maths.Value2 = 5;
      valueManipulator.Process();
      Assert.AreEqual( 0, valueManipulator.Value, "Subtraction failed." );

      // Add an additional maths processor after the existing one, check
      // that the chain produces the correct result.
      maths.OutputSocket.ConnectedProcessor = maths2;
      maths.Operator = '+';
      maths.Value2 = 1000;
      maths2.Operator = '+';
      maths2.Value2 = 123;
      valueManipulator.Process();
      Assert.AreEqual( 1123, valueManipulator.Value, "Chained operation failed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Double()
    {
      // Create a circuit.
      Maths maths = new Maths();
      Maths maths2 = new Maths();

      Circuit comparisonValue = new Circuit( 1.0 );

      Comparer comparer = new Comparer();
      comparer.Circuit_ComparisonValue = comparisonValue;
      comparer.OutputSocket_NotEqual.ConnectedProcessor = maths;
      comparer.OutputSocket_Equal.ConnectedProcessor = maths;

      Circuit valueManipulator = new Circuit( 0.0 );
      valueManipulator.EntryProcessor = comparer;

      // Simple addition test.
      maths.Operator = '+';
      maths.Value2 = 1.0;
      valueManipulator.Process();
      Assert.AreEqual( 1.0, valueManipulator.Value, "Addition failed." );

      // Simple multiplication test.
      maths.Operator = 'x';
      maths.Value2 = 10.0;
      valueManipulator.Process();
      Assert.AreEqual( 10.0, valueManipulator.Value, "Multiplication failed." );

      // Simple division test.
      maths.Operator = '/';
      maths.Value2 = 2.0;
      valueManipulator.Process();
      Assert.AreEqual( 5.0, valueManipulator.Value, "Division failed." );

      // Simple subtraction test.
      maths.Operator = '-';
      maths.Value2 = 5.0;
      valueManipulator.Process();
      Assert.AreEqual( 0.0, valueManipulator.Value, "Subtraction failed." );

      // Add an additional maths processor after the existing one, check
      // that the chain produces the correct result.
      maths.OutputSocket.ConnectedProcessor = maths2;
      maths.Operator = '+';
      maths.Value2 = 1000.0;
      maths2.Operator = '+';
      maths2.Value2 = 123.4;
      valueManipulator.Process();
      Assert.AreEqual( 1123.4, valueManipulator.Value, "Chained operation failed." );
    }

    //-------------------------------------------------------------------------
  }
}
