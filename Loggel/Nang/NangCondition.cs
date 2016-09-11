using System;
using Loggel.Processors;

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

    public Processor BuildCircuit( CircuitContext context )
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

      //-- Create a comparer which will perform the condition logic.
      Comparer comparer =
        context.CreateComponent<Comparer>(
          "Condition",
          "" );

      // We're comparing the reference story's value with our comparison value.
      comparer.ExternalValueSource = ReferenceStory.StoryCircuit.Context;
      comparer.ComparisonValue = ComparisonValue;

      return comparer;
    }

    //-------------------------------------------------------------------------
  }
}
