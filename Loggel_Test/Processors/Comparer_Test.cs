﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
      m_circuit = new Circuit( "", "", 0 );

      Maths mathsInRange = m_circuit.Context.CreateComponent<Maths>( "InRange", "" );
      mathsInRange.Operator = '=';
      mathsInRange.Value2 = c_inRangeValue;

      Maths mathsNotInRange = m_circuit.Context.CreateComponent<Maths>( "NotInRange", "" );
      mathsNotInRange.Operator = '=';
      mathsNotInRange.Value2 = c_notInRangeValue;

      Maths mathsEqual = m_circuit.Context.CreateComponent<Maths>( "Equal", "" );
      mathsEqual.Operator = '=';
      mathsEqual.Value2 = c_equalValue;

      Maths mathsGreater = m_circuit.Context.CreateComponent<Maths>( "Greater", "" );
      mathsGreater.Operator = '=';
      mathsGreater.Value2 = c_greaterValue;

      Maths mathsLesser = m_circuit.Context.CreateComponent<Maths>( "Lesser", "" );
      mathsLesser.Operator = '=';
      mathsLesser.Value2 = c_lesserValue;

      Maths mathsNotEqual = m_circuit.Context.CreateComponent<Maths>( "NotEqual", "" );
      mathsNotEqual.Operator = '=';
      mathsNotEqual.Value2 = c_notEqualValue;

      m_comparer = m_circuit.Context.CreateComponent<Comparer>( "Comparer", "" );
      m_comparer.Processor_InRange = mathsInRange;
      m_comparer.Processor_NotInRange = mathsNotInRange;
      m_comparer.Processor_Equal = mathsEqual;
      m_comparer.Processor_Greater = mathsGreater;
      m_comparer.Processor_Lesser = mathsLesser;
      m_comparer.Processor_NotEqual = mathsNotEqual;
      m_circuit.EntryProcessor = m_comparer;
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
        m_circuit.Context.Value,
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
        m_circuit.Context.Value,
        "Comparer did not select NotInRange connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Equal()
    {
      // 'Range' checks take precedence over other types, so remove them.
      m_comparer.Processor_InRange = null;
      m_comparer.Processor_NotInRange = null;

      // Test equal.
      m_comparer.ComparisonValue = 0;
      m_circuit.Process();
      Assert.AreEqual(
        c_equalValue,
        m_circuit.Context.Value,
        "Comparer did not select Equal connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Greater()
    {
      // 'Range' checks take precedence over other types, so remove them.
      m_comparer.Processor_InRange = null;
      m_comparer.Processor_NotInRange = null;

      // Test greater.
      m_comparer.ComparisonValue = -1;
      m_circuit.Process();
      Assert.AreEqual(
        c_greaterValue,
        m_circuit.Context.Value,
        "Comparer did not select Greater connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void Lesser()
    {
      // 'Range' checks take precedence over other types, so remove them.
      m_comparer.Processor_InRange = null;
      m_comparer.Processor_NotInRange = null;

      // Test lesser.
      m_comparer.ComparisonValue = 1;
      m_circuit.Process();
      Assert.AreEqual(
        c_lesserValue,
        m_circuit.Context.Value,
        "Comparer did not select Lesser connected processor." );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NotEqual()
    {
      // 'Range' checks take precedence over other types, so remove them.
      m_comparer.Processor_InRange = null;
      m_comparer.Processor_NotInRange = null;
      m_comparer.Processor_Lesser = null;

      // Test not equal.
      m_comparer.ComparisonValue = 1;
      m_circuit.Process();
      Assert.AreEqual(
        c_notEqualValue,
        m_circuit.Context.Value,
        "Comparer did not select NotEqual connected processor." );
    }

    //-------------------------------------------------------------------------
  }
}
