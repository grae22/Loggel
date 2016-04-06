using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel.System;
using Loggel.Processors;

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
      Maths maths = new Maths();
      maths.Operator = '+';
      maths.Value2 = 1;

      m_or = new Or();
      m_or.OutputSocket.ConnectedProcessor = maths;

      m_circuit = new Circuit( 0 );
      m_circuit.EntryProcessor = m_or;
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
      Or.Condition condition1 = new Or.Condition();
      condition1.Circuit = new Circuit( 123 );
      condition1.TestValue = 123;

      Or.Condition condition2 = new Or.Condition();
      condition2.Circuit = new Circuit( 999 );
      condition2.TestValue = 456;

      m_or.Conditions.Add( condition1 );
      m_or.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 1, m_circuit.Value, "Signal should have passed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void ConditionsNotMet()
    {
      Or.Condition condition1 = new Or.Condition();
      condition1.Circuit = new Circuit( 999 );
      condition1.TestValue = 123;

      Or.Condition condition2 = new Or.Condition();
      condition2.Circuit = new Circuit( 999 );
      condition2.TestValue = 456;

      m_or.Conditions.Add( condition1 );
      m_or.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 0, m_circuit.Value, "Signal should not have passed." );
    }

    //-------------------------------------------------------------------------
  }
}
