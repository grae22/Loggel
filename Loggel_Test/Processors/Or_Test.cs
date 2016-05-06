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
      Circuit.Reset();

      m_circuit = ComponentFactory.CreateCircuit( "", 0 );

      Maths maths = m_circuit.Context.CreateComponent<Maths>( "Maths", "" );
      maths.Operator = '+';
      maths.Value2 = 1;

      m_or = m_circuit.Context.CreateComponent<Or>( "Or", "" );
      m_or.ConnectedProcessor = maths;
      m_circuit.EntryProcessor = m_or;
    }

    //-------------------------------------------------------------------------
    // Test that when no conditions are specified the processor simply selects
    // the connected processor for processing.

    [TestMethod]
    public void Or_NoConditions()
    {
      m_circuit.Process();
      Assert.AreEqual( 1, m_circuit.Context.Value, "Signal should have passed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Or_ConditionsMet()
    {
      Circuit compareValue1 = ComponentFactory.CreateCircuit( "", 123 );
      Or.Condition condition1 = new Or.Condition();
      condition1.ValueSource = compareValue1.Context;
      condition1.ComparisonValue = 123;
      condition1.ComparisonType = ValueComparison.Comparison.EQUAL;

      Circuit compareValue2 = ComponentFactory.CreateCircuit( "", 999 );
      Or.Condition condition2 = new Or.Condition();
      condition2.ValueSource = compareValue1.Context;
      condition2.ComparisonValue = 123;
      condition2.ComparisonType = ValueComparison.Comparison.EQUAL;

      m_or.Conditions.Add( condition1 );
      m_or.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 1, m_circuit.Context.Value, "Signal should have passed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Or_ConditionsNotMet()
    {
      Circuit compareValue1 = ComponentFactory.CreateCircuit( "", 999 );
      Or.Condition condition1 = new Or.Condition();
      condition1.ValueSource = compareValue1.Context;
      condition1.ComparisonValue = 123;
      condition1.ComparisonType = ValueComparison.Comparison.EQUAL;

      Circuit compareValue2 = ComponentFactory.CreateCircuit( "", 999 );
      Or.Condition condition2 = new Or.Condition();
      condition2.ValueSource = compareValue1.Context;
      condition2.ComparisonValue = 456;
      condition2.ComparisonType = ValueComparison.Comparison.EQUAL;

      m_or.Conditions.Add( condition1 );
      m_or.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 0, m_circuit.Context.Value, "Signal should not have passed." );
    }

    //-------------------------------------------------------------------------
  }
}
