using System;
using Loggel.Processors;

namespace Loggel.Nang
{
  internal class NangCondition
  {
    //-------------------------------------------------------------------------

    // Name of this condition.
    public string Name { get; private set; }

    // The story whose value is used to test the condition.
    public NangStory ReferenceStory { get; set; }

    // The value that will be compared with the reference story's value.
    public dynamic ComparisonValue { get; set; }

    // The values that will be compared with the reference story's value
    // when using range comparisons.
    public dynamic ComparisonValueRangeMin { get; set; }
    public dynamic ComparisonValueRangeMax { get; set; }

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

    public NangCondition( string name )
    {
      Name = name;
    }

    //-------------------------------------------------------------------------

    // Creates the Loggel components required by this condition.

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
      Comparer.RangeMin = ComparisonValueRangeMin;
      Comparer.RangeMax = ComparisonValueRangeMax;

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
          Name + "_True",
          ActionWhenTrue,
          ActionValueWhenTrue,
          context );

      Processor processorWhenFalse =
        CreateActionProcessor(
          Name + "_False",
          ActionWhenFalse,
          ActionValueWhenFalse,
          context );

      // Clear all routes and re-map.
      Comparer.SetAllProcessors( null );

      switch( Comparison )
      {
        case ComparisonType.NOTHING:
          break;

        case ComparisonType.EQUAL:
          Comparer.Processor_Equal = processorWhenTrue;
          Comparer.Processor_NotEqual = processorWhenFalse;
          break;

        case ComparisonType.NOT_EQUAL:
          Comparer.Processor_NotEqual = processorWhenTrue;
          Comparer.Processor_Equal = processorWhenFalse;
          break;

        case ComparisonType.GREATER_OR_EQUAL:
          Comparer.Processor_Greater = processorWhenTrue;
          Comparer.Processor_Equal = processorWhenTrue;
          Comparer.Processor_Lesser = processorWhenFalse;
          break;

        case ComparisonType.GREATER:
          Comparer.Processor_Greater = processorWhenTrue;
          Comparer.Processor_Equal = processorWhenFalse;
          Comparer.Processor_Lesser = processorWhenFalse;
          break;

        case ComparisonType.LESSER_OR_EQUAL:
          Comparer.Processor_Lesser = processorWhenTrue;
          Comparer.Processor_Equal = processorWhenTrue;
          Comparer.Processor_Greater = processorWhenFalse;
          break;

        case ComparisonType.LESSER:
          Comparer.Processor_Lesser = processorWhenTrue;
          Comparer.Processor_Equal = processorWhenFalse;
          Comparer.Processor_Greater = processorWhenFalse;
          break;

        case ComparisonType.IN_RANGE:
          Comparer.Processor_InRange = processorWhenTrue;
          Comparer.Processor_NotInRange = processorWhenFalse;
          break;

        case ComparisonType.NOT_IN_RANGE:
          Comparer.Processor_NotInRange = processorWhenTrue;
          Comparer.Processor_InRange = processorWhenFalse;
          break;
      }
    }

    //-------------------------------------------------------------------------

    private static Processor CreateActionProcessor(
      string name,
      ActionType action,
      dynamic actionValue,
      CircuitContext context )
    {
      if( action == ActionType.NONE )
      {
        return null;
      }

      // Create the processor.
      Maths processor = context.CreateComponent<Maths>( name, "" );

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
