using System.Collections.Generic;

namespace Loggel.Nang
{
  public class NangCircuit
  {
    //-------------------------------------------------------------------------

    // All the stories belonging to this circuit.
    private Dictionary<string, NangStory> Stories { get; set; } =
      new Dictionary<string, NangStory>();

    //-------------------------------------------------------------------------

    public IStory CreateStory( string name )
    {
      NangStory story = new NangStory( name, IValue.Type.DECIMAL );

      story.BuildCircuit();

      Stories.Add( name, story );

      return story;
    }

    //-------------------------------------------------------------------------

    public void GetStories( out List< IStory > stories )
    {
      stories = new List< IStory >();

      foreach( IStory s in Stories.Values )
      {
        stories.Add( s );
      }
    }

    //-------------------------------------------------------------------------

    public IStory GetStory( string name )
    {
      if( Stories.ContainsKey( name ) )
      {
        return Stories[ name ];
      }

      return null;
    }

    //-------------------------------------------------------------------------

    public void GetCircuits( out List< Circuit > circuits )
    {
      circuits = new List< Circuit >();

      foreach( NangStory story in Stories.Values )
      {
        circuits.Add( story.StoryCircuit );
      }
    }

    //-------------------------------------------------------------------------

    public bool RenameStory( IStory story, string name )
    {
      if( Stories.ContainsKey( name ) )
      {
        return false;
      }

      Stories.Remove( story.GetName() );

      ((NangStory)story).Name = name;
      Stories.Add( name, (NangStory)story );

      return true;
    }

    //-------------------------------------------------------------------------

    public void ChangeStoryValueType(
      IStory story,
      IValue.Type type,
      dynamic value )
    {
      NangStory nangStory = (NangStory)story;
      nangStory.ChangeType( type );
      nangStory.Value.SetValue( value );
    }

    //-------------------------------------------------------------------------

    public ICondition CreateStoryCondition( IStory story )
    {
      NangStory nangStory = (NangStory)story;

      NangCondition condition =
        nangStory.CreateCondition(
          null,
          ICondition.ComparisonType.NOTHING,
          0,
          ICondition.ActionType.NONE,
          0,
          ICondition.ActionType.NONE,
          0 );
     
      return condition;
    }

    //-------------------------------------------------------------------------

    public void SetStoryConditionValues(
      ICondition condition,
      IStory story,
      IStory referenceStory,
      ICondition.ComparisonType comparisonType,
      dynamic comparisonValue,
      ICondition.ActionType actionWhenTrue,
      dynamic actionValueWhenTrue,
      ICondition.ActionType actionWhenFalse,
      dynamic actionValueWhenFalse )
    {
      NangCondition nangCondition = (NangCondition)condition;

      nangCondition.ReferenceStory = (NangStory)referenceStory;
      nangCondition.Comparison = comparisonType;
      nangCondition.ComparisonValue = comparisonValue;
      nangCondition.ActionWhenTrue = actionWhenTrue;
      nangCondition.ActionValueWhenTrue = actionValueWhenTrue;
      nangCondition.ActionWhenFalse = actionWhenFalse;
      nangCondition.ActionValueWhenFalse = actionValueWhenFalse;

      ((NangStory)story).BuildCircuit();
    }

    //-------------------------------------------------------------------------

    public ICondition CreateSubCondition( ICondition condition )
    {
      NangCondition nangCondition = (NangCondition)condition;

      NangCondition subCondition =
        nangCondition.CreateSubCondition(
          null,
          ICondition.ComparisonType.NOTHING,
          0,
          ICondition.ActionType.NONE,
          0,
          ICondition.ActionType.NONE,
          0 );
     
      return subCondition;
    }

    //-------------------------------------------------------------------------

    public void SetSubConditionValues(
      ICondition condition,
      IStory story,
      IStory referenceStory,
      ICondition.ComparisonType comparisonType,
      dynamic comparisonValue,
      ICondition.ActionType actionWhenTrue,
      dynamic actionValueWhenTrue,
      ICondition.ActionType actionWhenFalse,
      dynamic actionValueWhenFalse )
    {
      NangCondition nangCondition = (NangCondition)condition;

      nangCondition.ReferenceStory = (NangStory)referenceStory;
      nangCondition.Comparison = comparisonType;
      nangCondition.ComparisonValue = comparisonValue;
      nangCondition.ActionWhenTrue = actionWhenTrue;
      nangCondition.ActionValueWhenTrue = actionValueWhenTrue;
      nangCondition.ActionWhenFalse = actionWhenFalse;
      nangCondition.ActionValueWhenFalse = actionValueWhenFalse;

      ((NangStory)story).BuildCircuit();
    }

    //-------------------------------------------------------------------------

    public void RunCircuit()
    {
      foreach( NangStory story in Stories.Values )
      {
        foreach( Component component in story.StoryCircuit.Context.Components.Values )
        {
          component.HasProcessed = false;
        }
      }

      foreach( NangStory story in Stories.Values )
      {
        story.StoryCircuit.Process();
      }
    }

    //-------------------------------------------------------------------------
  }
}