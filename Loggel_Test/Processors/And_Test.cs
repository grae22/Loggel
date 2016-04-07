using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;
using Loggel.Processors;

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
      Maths maths = new Maths();
      maths.Operator = '+';
      maths.Value2 = 1;

      m_and = new And();
      m_and.OutputSocket.ConnectedProcessor = maths;

      m_circuit = new Circuit( 0 );
      m_circuit.EntryProcessor = m_and;
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
      Circuit circuit1 = new Circuit( 123 );
      Comparer condition1 = new Comparer();
      condition1.ComparisonValue = 123;

      Circuit circuit2 = new Circuit( 456 );
      Comparer condition2 = new Comparer();
      condition2.ComparisonValue = 456;

      m_and.Conditions.Add( condition1 );
      m_and.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 1, m_circuit.Value, "Signal should have passed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void ConditionsNotMet()
    {
      And.Condition condition1 = new And.Condition();
      condition1.Circuit = new Circuit( 123 );
      condition1.TestValue = 123;

      And.Condition condition2 = new And.Condition();
      condition2.Circuit = new Circuit( 999 );
      condition2.TestValue = 456;

      m_and.Conditions.Add( condition1 );
      m_and.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 0, m_circuit.Value, "Signal should not have passed." );
    }

    //-------------------------------------------------------------------------
  }
}
