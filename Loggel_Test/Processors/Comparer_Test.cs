using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;

namespace Loggel_Test
{
  [TestClass]
  public class Comparer_Test
  {
    //-------------------------------------------------------------------------

    private Circuit<int> circuit;
    private Comparer<int> comparer;
    private const int c_inRangeValue = 1;
    private const int c_notInRangeValue = 2;
    private const int c_equalValue = 3;
    private const int c_greaterValue = 4;
    private const int c_lesserValue = 5;
    private const int c_notEqualValue = 6;
    
    //-------------------------------------------------------------------------
    // Prepare a circuit with a comparer processor whose output sockets all
    // link to a different maths operation - we'll use this to test that the
    // correct comparison decision is made based on the circuit's end value.

    [TestInitialize]
    public void Initialise()
    {
      Maths<int> mathsInRange = new Maths<int>();
      mathsInRange.Operator = '=';
      mathsInRange.Value2 = c_inRangeValue;

      Maths<int> mathsNotInRange = new Maths<int>();
      mathsNotInRange.Operator = '=';
      mathsNotInRange.Value2 = c_notInRangeValue;

      Maths<int> mathsEqual = new Maths<int>();
      mathsEqual.Operator = '=';
      mathsEqual.Value2 = c_equalValue;

      Maths<int> mathsGreater = new Maths<int>();
      mathsGreater.Operator = '=';
      mathsGreater.Value2 = c_greaterValue;

      Maths<int> mathsLesser = new Maths<int>();
      mathsLesser.Operator = '=';
      mathsLesser.Value2 = c_lesserValue;

      Maths<int> mathsNotEqual = new Maths<int>();
      mathsNotEqual.Operator = '=';
      mathsNotEqual.Value2 = c_notEqualValue;

      comparer = new Comparer<int>();
      comparer.OutSocket_InRange.ConnectedProcessor = mathsInRange;
      comparer.OutSocket_NotInRange.ConnectedProcessor = mathsNotInRange;
      comparer.OutSocket_Equal.ConnectedProcessor = mathsEqual;
      comparer.OutSocket_Greater.ConnectedProcessor = mathsGreater;
      comparer.OutSocket_Lesser.ConnectedProcessor = mathsLesser;
      comparer.OutSocket_NotEqual.ConnectedProcessor = mathsNotEqual;

      circuit = new Circuit<int>( 0 );
      circuit.EntryProcessor = comparer;
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void InRange()
    {
      // Test in range.
      comparer.RangeMin = -1;
      comparer.RangeMax = 1;
      circuit.Process();
      Assert.AreEqual(
        c_inRangeValue,
        circuit.Value,
        "Comparer did not select InRange connected processor."  );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NotInRange()
    {
      // Test not in range.
      comparer.RangeMin = 1;
      comparer.RangeMax = 10;
      circuit.Process();
      Assert.AreEqual(
        c_notInRangeValue,
        circuit.Value,
        "Comparer did not select NotInRange connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Equal()
    {
      // 'Range' checks take precedence over other types, so remove them.
      comparer.OutSocket_InRange.ConnectedProcessor = null;
      comparer.OutSocket_NotInRange.ConnectedProcessor = null;

      // Test equal.
      comparer.ComparisonValue = 0;
      circuit.Process();
      Assert.AreEqual(
        c_equalValue,
        circuit.Value,
        "Comparer did not select Equal connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Greater()
    {
      // 'Range' checks take precedence over other types, so remove them.
      comparer.OutSocket_InRange.ConnectedProcessor = null;
      comparer.OutSocket_NotInRange.ConnectedProcessor = null;

      // Test greater.
      comparer.ComparisonValue = -1;
      circuit.Process();
      Assert.AreEqual(
        c_greaterValue,
        circuit.Value,
        "Comparer did not select Greater connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Lesser()
    {
      // 'Range' checks take precedence over other types, so remove them.
      comparer.OutSocket_InRange.ConnectedProcessor = null;
      comparer.OutSocket_NotInRange.ConnectedProcessor = null;

      // Test lesser.
      comparer.ComparisonValue = 1;
      circuit.Process();
      Assert.AreEqual(
        c_lesserValue,
        circuit.Value,
        "Comparer did not select Lesser connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NotEqual()
    {
      // 'Range' checks take precedence over other types, so remove them.
      comparer.OutSocket_InRange.ConnectedProcessor = null;
      comparer.OutSocket_NotInRange.ConnectedProcessor = null;
      comparer.OutSocket_Lesser.ConnectedProcessor = null;

      // Test not equal.
      comparer.ComparisonValue = 1;
      circuit.Process();
      Assert.AreEqual(
        c_notEqualValue,
        circuit.Value,
        "Comparer did not select NotEqual connected processor." );
    }

    //-------------------------------------------------------------------------
  }
}
