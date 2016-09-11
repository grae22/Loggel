using System;

namespace Loggel.Nang
{
  internal class NangCondition
  {
    //-------------------------------------------------------------------------

    // The story whose value is used to test the condition.
    public NangStory ReferenceStory { get; set; }

    // The value that will be compared with the reference story's value.
    public dynamic ComparisonValue { get; set; }

    // Conditions.
    public enum Type
    {
      EQUAL,
      NOT_EQUAL,
      GREATER_OR_EQUAL,
      GREATER,
      LESSER_OR_EQUAL,
      LESSER,
      IN_RANGE,
      NOT_IN_RANGE
    }

    // A nested condition that will be tested when this condition is true.
    public NangCondition SubConditionWhenTrue { get; set; }

    // A nested condition that will be tested when this condition is false.
    public NangCondition SubConditionWhenFalse { get; set; }

    //-------------------------------------------------------------------------

    // Tests the condition and returns the result.

    public void BuildCircuit()
    {
      // No reference story?
      if( ReferenceStory == null )
      {
        throw new Exception( "Condition has no reference story." );
      }

      // No comparison value?
      if( ComparisonValue == null )
      {
        throw new Exception( "Condition has no comparison value." );
      }
    }

    //-------------------------------------------------------------------------
  }
}
