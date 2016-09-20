using System;
using System.Collections.Generic;
using Loggel.Processors;

namespace Loggel.Nang
{
  //===========================================================================

  public abstract class ICondition
  {
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

    public enum ActionType
    {
      NONE,
      SET,
      ADD,
      SUBTRACT
    }

    public abstract IStory GetReferenceStory();
  }

  //===========================================================================

  internal class NangCondition : ICondition
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
    public ComparisonType Comparison { get; set; } = ComparisonType.NOTHING;

    // Action that will be applied to the story's value.
    public ActionType Action { get; set; } = ActionType.NONE;

    // Value to be used when performing the action.
    public dynamic ActionValue { get; set; }

    // Processor for action.
    private Processor ActionProcessor { get; set; }

    // Comparer that will perform the conditional logic.
    private Comparer Comparer { get; set; }

    // Nested conditions that will be tested when this condition is true.
    public List< NangCondition > SubConditions { get; set; } = new List< NangCondition >();

    // Router to connect sub-conditions.
    private Router SubConditionRouter { get; set; }

    //-------------------------------------------------------------------------

    override public IStory GetReferenceStory()
    {
      return ReferenceStory;
    }

    //-------------------------------------------------------------------------

    public NangCondition( string name )
    {
      Name = name;
    }

    //-------------------------------------------------------------------------

    // Creates the Loggel components required by this condition.

    public Comparer BuildCircuit( CircuitContext context )
    {
      // No reference story?
      if( ReferenceStory == null )
      {
        return null;
        //throw new Exception( "Condition has no reference story." );
      }

      // No comparison value?
      if( ComparisonValue == null )
      {
        return null;
        //throw new Exception( "Condition has no comparison value." );
      }

      //-- Create a comparer which will perform the condition logic.
      if( Comparer == null )
      {
        Comparer =
          context.CreateComponent<Comparer>(
            "Condition_" + ReferenceStory.Name,
            "",
            true );
      }

      // We're comparing the reference story's value with our comparison value.
      Comparer.ExternalValueSource = ReferenceStory.StoryCircuit.Context;
      Comparer.ComparisonValue = ComparisonValue;
      Comparer.RangeMin = ComparisonValueRangeMin;
      Comparer.RangeMax = ComparisonValueRangeMax;

      // Sub conditions.
      if( SubConditionRouter != null )
      {
        SubConditionRouter.Routes.Clear();
      }

      foreach( NangCondition subCondition in SubConditions )
      {
        if( SubConditionRouter == null )
        {
          SubConditionRouter =
            context.CreateComponent< Router >(
              "SubConditionRouter_" + Name,
              "" );
        }

        Comparer comparer = subCondition.BuildCircuit( context );

        if( comparer != null )
        {
          SubConditionRouter.Routes.Add( comparer );
        }
      }

      // Build actions.
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
      if( SubConditionRouter == null )
      {
        if( ActionProcessor == null )
        {
          ActionProcessor =
            CreateActionProcessor(
              Name + "_Action",
              Action,
              ActionValue,
              context );
        }
        else if( ActionProcessor is Maths )
        {
          SetActionProcessorValues(
            (Maths)ActionProcessor,
            Action,
            ActionValue );
        }
      }
      else
      {
        ActionProcessor = SubConditionRouter;
      }

      // Clear all routes and re-map.
      Comparer.SetAllProcessors( null );

      switch( Comparison )
      {
        case ComparisonType.NOTHING:
          break;

        case ComparisonType.EQUAL:
          Comparer.Processor_Equal = ActionProcessor;
          break;

        case ComparisonType.NOT_EQUAL:
          Comparer.Processor_NotEqual = ActionProcessor;
          break;

        case ComparisonType.GREATER_OR_EQUAL:
          Comparer.Processor_Greater = ActionProcessor;
          Comparer.Processor_Equal = ActionProcessor;
          break;

        case ComparisonType.GREATER:
          Comparer.Processor_Greater = ActionProcessor;
          break;

        case ComparisonType.LESSER_OR_EQUAL:
          Comparer.Processor_Lesser = ActionProcessor;
          Comparer.Processor_Equal = ActionProcessor;
          break;

        case ComparisonType.LESSER:
          Comparer.Processor_Lesser = ActionProcessor;
          break;

        case ComparisonType.IN_RANGE:
          Comparer.Processor_InRange = ActionProcessor;
          break;

        case ComparisonType.NOT_IN_RANGE:
          Comparer.Processor_NotInRange = ActionProcessor;
          break;

        default:
          System.Diagnostics.Debug.Assert( false );
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
      Maths processor = context.CreateComponent< Maths >( name, "", true );

      // Set the values.
      SetActionProcessorValues(
        processor,
        action,
        actionValue );

      // Return the new processor.
      return processor;
    }

    //-------------------------------------------------------------------------

    private static void SetActionProcessorValues(
      Maths processor,
      ActionType action,
      dynamic actionValue )
    {
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
    }

    //-------------------------------------------------------------------------

    public NangCondition CreateSubCondition(
      NangStory referenceStory,
      ComparisonType comparisonType,
      dynamic comparisonValue,
      ActionType action,
      dynamic actionValue )
    {
      NangCondition condition =
        new NangCondition( Name + '_' + referenceStory?.Name );

      condition.ReferenceStory = referenceStory;
      condition.Comparison = comparisonType;
      condition.ComparisonValue = comparisonValue;
      condition.Action = action;
      condition.ActionValue = actionValue;

      SubConditions.Add( condition );

      return condition;
    }

    //-------------------------------------------------------------------------
  }
}
