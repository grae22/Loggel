using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;

namespace Loggel_Test
{
  [TestClass]
  public class ComparerProcessor_Test
  {
    //-------------------------------------------------------------------------

    private Circuit m_circuit;
    private Comparer m_comparer;
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
      m_circuit = new Circuit( 0 );

      Maths mathsInRange = new Maths( null );
      mathsInRange.Operator = '=';
      mathsInRange.Value2 = c_inRangeValue;
      m_circuit.RegisterProcessor( mathsInRange, false );

      Maths mathsNotInRange = new Maths( null );
      mathsNotInRange.Operator = '=';
      mathsNotInRange.Value2 = c_notInRangeValue;
      m_circuit.RegisterProcessor( mathsNotInRange, false );

      Maths mathsEqual = new Maths( null );
      mathsEqual.Operator = '=';
      mathsEqual.Value2 = c_equalValue;
      m_circuit.RegisterProcessor( mathsEqual, false );

      Maths mathsGreater = new Maths( null );
      mathsGreater.Operator = '=';
      mathsGreater.Value2 = c_greaterValue;
      m_circuit.RegisterProcessor( mathsGreater, false );

      Maths mathsLesser = new Maths( null );
      mathsLesser.Operator = '=';
      mathsLesser.Value2 = c_lesserValue;
      m_circuit.RegisterProcessor( mathsLesser, false );

      Maths mathsNotEqual = new Maths( null );
      mathsNotEqual.Operator = '=';
      mathsNotEqual.Value2 = c_notEqualValue;
      m_circuit.RegisterProcessor( mathsNotEqual, false );

      m_comparer = new Comparer( null );
      m_comparer.OutputSocket_InRange.ConnectedProcessor = mathsInRange;
      m_comparer.OutputSocket_NotInRange.ConnectedProcessor = mathsNotInRange;
      m_comparer.OutputSocket_Equal.ConnectedProcessor = mathsEqual;
      m_comparer.OutputSocket_Greater.ConnectedProcessor = mathsGreater;
      m_comparer.OutputSocket_Lesser.ConnectedProcessor = mathsLesser;
      m_comparer.OutputSocket_NotEqual.ConnectedProcessor = mathsNotEqual;
      m_circuit.RegisterProcessor( m_comparer, true );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void InRange()
    {
      // Test in range.
      m_comparer.RangeMin = -1;
      m_comparer.RangeMax = 1;
      m_circuit.Process();
      Assert.AreEqual(
        c_inRangeValue,
        m_circuit.Value,
        "Comparer did not select InRange connected processor."  );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NotInRange()
    {
      // Test not in range.
      m_comparer.RangeMin = 1;
      m_comparer.RangeMax = 10;
      m_circuit.Process();
      Assert.AreEqual(
        c_notInRangeValue,
        m_circuit.Value,
        "Comparer did not select NotInRange connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Equal()
    {
      // 'Range' checks take precedence over other types, so remove them.
      m_comparer.OutputSocket_InRange.ConnectedProcessor = null;
      m_comparer.OutputSocket_NotInRange.ConnectedProcessor = null;

      // Test equal.
      m_comparer.ComparisonValue = 0;
      m_circuit.Process();
      Assert.AreEqual(
        c_equalValue,
        m_circuit.Value,
        "Comparer did not select Equal connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Greater()
    {
      // 'Range' checks take precedence over other types, so remove them.
      m_comparer.OutputSocket_InRange.ConnectedProcessor = null;
      m_comparer.OutputSocket_NotInRange.ConnectedProcessor = null;

      // Test greater.
      m_comparer.ComparisonValue = -1;
      m_circuit.Process();
      Assert.AreEqual(
        c_greaterValue,
        m_circuit.Value,
        "Comparer did not select Greater connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Lesser()
    {
      // 'Range' checks take precedence over other types, so remove them.
      m_comparer.OutputSocket_InRange.ConnectedProcessor = null;
      m_comparer.OutputSocket_NotInRange.ConnectedProcessor = null;

      // Test lesser.
      m_comparer.ComparisonValue = 1;
      m_circuit.Process();
      Assert.AreEqual(
        c_lesserValue,
        m_circuit.Value,
        "Comparer did not select Lesser connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NotEqual()
    {
      // 'Range' checks take precedence over other types, so remove them.
      m_comparer.OutputSocket_InRange.ConnectedProcessor = null;
      m_comparer.OutputSocket_NotInRange.ConnectedProcessor = null;
      m_comparer.OutputSocket_Lesser.ConnectedProcessor = null;

      // Test not equal.
      m_comparer.ComparisonValue = 1;
      m_circuit.Process();
      Assert.AreEqual(
        c_notEqualValue,
        m_circuit.Value,
        "Comparer did not select NotEqual connected processor." );
    }

    //-------------------------------------------------------------------------
  }
}
