﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
      m_circuit = new Circuit( 0 );

      Maths maths = new Maths( null );
      maths.Operator = '+';
      maths.Value2 = 1;
      m_circuit.RegisterProcessor( maths, false );      

      m_and = new And( null );
      m_and.OutputSocket.ConnectedProcessor = maths;
      m_circuit.RegisterProcessor( m_and, true );
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
      And.Condition condition1 = new And.Condition();
      condition1.ValueSource = compareValue1;
      condition1.ComparisonValue = 123;
      condition1.ComparisonType = ValueComparison.Comparison.EQUAL;

      Circuit compareValue2 = new Circuit( 456 );
      And.Condition condition2 = new And.Condition();
      condition2.ValueSource = compareValue2;
      condition2.ComparisonValue = 456;
      condition2.ComparisonType = ValueComparison.Comparison.EQUAL;

      m_and.Conditions.Add( condition1 );
      m_and.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 1, m_circuit.Value, "Signal should have passed." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void ConditionsNotMet()
    {
      Circuit compareValue1 = new Circuit( 123 );
      And.Condition condition1 = new And.Condition();
      condition1.ValueSource = compareValue1;
      condition1.ComparisonValue = 123;
      condition1.ComparisonType = ValueComparison.Comparison.EQUAL;

      Circuit compareValue2 = new Circuit( 999 );
      And.Condition condition2 = new And.Condition();
      condition2.ValueSource = compareValue2;
      condition2.ComparisonValue = 123;
      condition2.ComparisonType = ValueComparison.Comparison.EQUAL;

      m_and.Conditions.Add( condition1 );
      m_and.Conditions.Add( condition2 );
      m_circuit.Process();
      Assert.AreEqual( 0, m_circuit.Value, "Signal should not have passed." );
    }

    //-------------------------------------------------------------------------
  }
}
