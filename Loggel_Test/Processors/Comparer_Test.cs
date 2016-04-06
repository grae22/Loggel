using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel.System;
using Loggel.Processors;

namespace Loggel_Test
{
  [TestClass]
  public class ComparerProcessor_Test
  {
    //-------------------------------------------------------------------------

    private Circuit circuit;
    private Comparer comparer;
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
      Maths mathsInRange = new Maths();
      mathsInRange.Operator = '=';
      mathsInRange.Value2 = c_inRangeValue;

      Maths mathsNotInRange = new Maths();
      mathsNotInRange.Operator = '=';
      mathsNotInRange.Value2 = c_notInRangeValue;

      Maths mathsEqual = new Maths();
      mathsEqual.Operator = '=';
      mathsEqual.Value2 = c_equalValue;

      Maths mathsGreater = new Maths();
      mathsGreater.Operator = '=';
      mathsGreater.Value2 = c_greaterValue;

      Maths mathsLesser = new Maths();
      mathsLesser.Operator = '=';
      mathsLesser.Value2 = c_lesserValue;

      Maths mathsNotEqual = new Maths();
      mathsNotEqual.Operator = '=';
      mathsNotEqual.Value2 = c_notEqualValue;

      comparer = new Comparer();
      comparer.OutputSocket_InRange.ConnectedProcessor = mathsInRange;
      comparer.OutputSocket_NotInRange.ConnectedProcessor = mathsNotInRange;
      comparer.OutputSocket_Equal.ConnectedProcessor = mathsEqual;
      comparer.OutputSocket_Greater.ConnectedProcessor = mathsGreater;
      comparer.OutputSocket_Lesser.ConnectedProcessor = mathsLesser;
      comparer.OutputSocket_NotEqual.ConnectedProcessor = mathsNotEqual;

      circuit = new Circuit( 0 );
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
      comparer.OutputSocket_InRange.ConnectedProcessor = null;
      comparer.OutputSocket_NotInRange.ConnectedProcessor = null;

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
      comparer.OutputSocket_InRange.ConnectedProcessor = null;
      comparer.OutputSocket_NotInRange.ConnectedProcessor = null;

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
      comparer.OutputSocket_InRange.ConnectedProcessor = null;
      comparer.OutputSocket_NotInRange.ConnectedProcessor = null;

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
      comparer.OutputSocket_InRange.ConnectedProcessor = null;
      comparer.OutputSocket_NotInRange.ConnectedProcessor = null;
      comparer.OutputSocket_Lesser.ConnectedProcessor = null;

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
