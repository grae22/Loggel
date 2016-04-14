using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;
using Loggel.Helpers;

namespace Loggel_Test.Processors
{
  [TestClass]
  public class AndProcessor
  {
    //-------------------------------------------------------------------------

    private Circuit m_circuit;
    private And m_and;

    //-------------------------------------------------------------------------

    [TestInitialize]
    public void Initialise()
    {
      m_circuit = new Circuit( "", 0 );

      Maths maths = m_circuit.CreateProcessor<Maths>( "", "", false );
      maths.Operator = '+';
      maths.Value2 = 1;

      m_and = m_circuit.CreateProcessor<And>( "", "", true );
      m_and.OutputSocket.ConnectedProcessor = maths;
    }

    //-------------------------------------------------------------------------
    // Test that when no conditions are specified the processor simply selects
    // the connected processor for processing.

    [TestMethod]
    public void NoConditions()
    {
      m_circuit.Process();
      Assert.AreEqual( 1, m_circuit.Context.Value, "Signal should have passed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void ConditionsMet()
    {
      Circuit compareValue1 = new Circuit( "", 123 );
      And.Condition condition1 = new And.Condition();
      condition1.ValueSource = compareValue1.Context;
      condition1.ComparisonValue = 123;
      condition1.ComparisonType = ValueComparison.Comparison.EQUAL;

      Circuit compareValue2 = new Circuit( "", 456 );
      And.Condition condition2 = new And.Condition();
      condition2.ValueSource = compareValue2.Context;
      condition2.ComparisonValue = 456;
      condition2.ComparisonType = ValueComparison.Comparison.EQUAL;

      m_and.Conditions.Add( condition1 );
      m_and.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 1, m_circuit.Context.Value, "Signal should have passed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void ConditionsNotMet()
    {
      Circuit compareValue1 = new Circuit( "", 123 );
      And.Condition condition1 = new And.Condition();
      condition1.ValueSource = compareValue1.Context;
      condition1.ComparisonValue = 123;
      condition1.ComparisonType = ValueComparison.Comparison.EQUAL;

      Circuit compareValue2 = new Circuit( "", 999 );
      And.Condition condition2 = new And.Condition();
      condition2.ValueSource = compareValue2.Context;
      condition2.ComparisonValue = 123;
      condition2.ComparisonType = ValueComparison.Comparison.EQUAL;

      m_and.Conditions.Add( condition1 );
      m_and.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 0, m_circuit.Context.Value, "Signal should not have passed." );
    }

    //-------------------------------------------------------------------------
  }
}
