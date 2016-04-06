using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;

namespace Loggel_Test
{
  [TestClass]
  public class Maths_Test
  {
    //-------------------------------------------------------------------------

    [TestMethod]
    public void Integer()
    {
      // Create a circuit.
      Maths<int> maths = new Maths<int>();
      Maths<int> maths2 = new Maths<int>();

      Circuit<int> comparisonValue = new Circuit<int>( 1 );

      Comparer<int> comparer = new Comparer<int>();
      comparer.Circuit_ComparisonValue = comparisonValue;
      comparer.OutSocket_NotEqual.ConnectedProcessor = maths;
      comparer.OutSocket_Equal.ConnectedProcessor = maths;

      Circuit<int> valueManipulator = new Circuit<int>( 0 );
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
      Assert.AreEqual( 0.0, valueManipulator.Value, "Subtraction failed." );

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
      Maths<double> maths = new Maths<double>();
      Maths<double> maths2 = new Maths<double>();

      Circuit<double> comparisonValue = new Circuit<double>( 1.0 );

      Comparer<double> comparer = new Comparer<double>();
      comparer.Circuit_ComparisonValue = comparisonValue;
      comparer.OutSocket_NotEqual.ConnectedProcessor = maths;
      comparer.OutSocket_Equal.ConnectedProcessor = maths;

      Circuit<double> valueManipulator = new Circuit<double>( 0.0 );
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
