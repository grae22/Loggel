using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;
using Loggel.Helpers;

namespace Loggel_Test.Processors
{
  [TestClass]
  public class OrProcessor
  {
    //-------------------------------------------------------------------------

    private Circuit m_circuit;
    private Or m_or;

    //-------------------------------------------------------------------------

    [TestInitialize]
    public void Initialise()
    {
      m_circuit = new Circuit( 0 );

      Maths maths = m_circuit.CreateProcessor<Maths>( "", "", false );
      maths.Operator = '+';
      maths.Value2 = 1;

      m_or = m_circuit.CreateProcessor<Or>( "", "", true );
      m_or.OutputSocket.ConnectedProcessor = maths;
    }

    //-------------------------------------------------------------------------
    // Test that when no conditions are specified the processor simply selects
    // the connected processor for processing.

    [TestMethod]
    public void NoConditions()
    {
      m_circuit.Process();
      Assert.AreEqual( 1, m_circuit.Value, "Signal should have passed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void ConditionsMet()
    {
      Circuit compareValue1 = new Circuit( 123 );
      Or.Condition condition1 = new Or.Condition();
      condition1.ValueSource = compareValue1;
      condition1.ComparisonValue = 123;
      condition1.ComparisonType = ValueComparison.Comparison.EQUAL;

      Circuit compareValue2 = new Circuit( 999 );
      Or.Condition condition2 = new Or.Condition();
      condition2.ValueSource = compareValue1;
      condition2.ComparisonValue = 123;
      condition2.ComparisonType = ValueComparison.Comparison.EQUAL;

      m_or.Conditions.Add( condition1 );
      m_or.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 1, m_circuit.Value, "Signal should have passed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void ConditionsNotMet()
    {
      Circuit compareValue1 = new Circuit( 999 );
      Or.Condition condition1 = new Or.Condition();
      condition1.ValueSource = compareValue1;
      condition1.ComparisonValue = 123;
      condition1.ComparisonType = ValueComparison.Comparison.EQUAL;

      Circuit compareValue2 = new Circuit( 999 );
      Or.Condition condition2 = new Or.Condition();
      condition2.ValueSource = compareValue1;
      condition2.ComparisonValue = 456;
      condition2.ComparisonType = ValueComparison.Comparison.EQUAL;

      m_or.Conditions.Add( condition1 );
      m_or.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 0, m_circuit.Value, "Signal should not have passed." );
    }

    //-------------------------------------------------------------------------
  }
}
