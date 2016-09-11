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

    // Type of comparison to be used.
    public enum ComparisonType
    {
      NOTHING,
      EQUAL,
      NOT_EQUAL,
      GREATER_OR_EQUAL,
      GREATER,
      LESSER_OR_EQUAL,
      LESSER,
      IN_RANGE,
      NOT_IN_RANGE
    }

    public ComparisonType Comparison { get; set; } = ComparisonType.NOTHING;

    // Action that will be applied to the story's value when
    // the condition is true/false.
    public enum ActionType
    {
      NONE,
      SET,
      ADD,
      SUBTRACT
    }

    public ActionType ActionWhenTrue { get; set; } = ActionType.NONE;
    public ActionType ActionWhenFalse { get; set; } = ActionType.NONE;

    // Values to be used when performing the actions.
    public dynamic ActionValueWhenTrue { get; set; }
    public dynamic ActionValueWhenFalse { get; set; }

    // Nested conditions that will be tested when this condition is true/false.
    public NangCondition SubConditionWhenTrue { get; set; }
    public NangCondition SubConditionWhenFalse { get; set; }

    // Comparer that will perform the conditional logic.
    private Comparer Comparer { get; set; }

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
      Comparer =
        context.CreateComponent<Comparer>(
          "Condition",
          "" );

      // We're comparing the reference story's value with our comparison value.
      Comparer.ExternalValueSource = ReferenceStory.StoryCircuit.Context;
      Comparer.ComparisonValue = ComparisonValue;

      BuildActions( context );

      return Comparer;
    }

    //-------------------------------------------------------------------------

    private void BuildActions( CircuitContext context )
    {
      // Must have a comparer.
      if( Comparer == null )
      {
        throw new Exception( "Comparer is null." );
      }

      // Create processors to perform actions.
      Processor processorWhenTrue =
        CreateActionProcessor(
          ActionWhenTrue,
          ActionValueWhenTrue,
          context );

      Processor processorWhenFalse =
        CreateActionProcessor(
          ActionWhenFalse,
          ActionValueWhenFalse,
          context );

      // Clear all routes and re-map.
      Comparer.ClearConnectedProcessors();

      switch( Comparison )
      {
        case ComparisonType.NOTHING:
          break;

        case ComparisonType.EQUAL:
          Comparer.Processor_Equal = processorWhenTrue;
          break;
      }
    }

    //-------------------------------------------------------------------------

    private static Processor CreateActionProcessor(
      ActionType action,
      dynamic actionValue,
      CircuitContext context )
    {
      if( action == ActionType.NONE )
      {
        return null;
      }

      // Create the processor.
      Maths processor = context.CreateComponent<Maths>( "Maths", "" );

      // Set its operation type.
      switch( action )
      {
        case ActionType.NONE:
          break;

        case ActionType.SET:
          processor.Operator = '=';
          break;

        case ActionType.ADD:
          processor.Operator = '+';
          break;

        case ActionType.SUBTRACT:
          processor.Operator = '-';
          break;

        default:
          // TODO
          System.Diagnostics.Debug.Assert( false );
          break;
      }

      // Set its value2.
      if( actionValue == null )
      {
        throw new Exception( "Action value is null." );
      }

      processor.Value2 = actionValue;

      // Return the new processor.
      return processor;
    }

    //-------------------------------------------------------------------------
  }
}
