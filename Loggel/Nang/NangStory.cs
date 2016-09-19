﻿using System;
using System.Collections.Generic;
using Loggel.Processors;

namespace Loggel.Nang
{
  //===========================================================================

  public abstract class IStory
  {
    public abstract string GetName();
    public abstract IValue.Type GetValueType();
    public abstract dynamic GetValue();
    public abstract void GetConditions( out List< ICondition > conditions );
  }

  //===========================================================================

  internal class NangStory : IStory
  {
    //-------------------------------------------------------------------------

    public string Name { get; set; }
    public NangValue Value { get; private set; }
    public List< NangCondition > Conditions { get; private set; } = new List< NangCondition >();
    public Circuit StoryCircuit { get; private set; }

    //-------------------------------------------------------------------------

    override public string GetName()
    {
      return Name;
    }

    //-------------------------------------------------------------------------

    override public IValue.Type GetValueType()
    {
      return Value.ValueType;
    }

    //-------------------------------------------------------------------------

    override public dynamic GetValue()
    {
      return Value.GetValue();
    }

    //-------------------------------------------------------------------------

    override public void GetConditions( out List< ICondition > conditions )
    {
      conditions = new List< ICondition >();

      foreach( ICondition condition in Conditions )
      {
        conditions.Add( condition );
      }
    }

    //=========================================================================

    public NangStory( string name, IValue.Type valueType )
    {
      Name = name;
      
      ChangeType( valueType );
    }

    //-------------------------------------------------------------------------

    public Circuit BuildCircuit()
    {
      if( Name == null || Name.Length == 0 )
      {
        throw new Exception( "Story has no name." );
      }

      Router router = null;

      if( StoryCircuit == null )
      {
        StoryCircuit =
          ComponentFactory.CreateCircuit(
            Name,
            Value.GetValue() );

        router =
          StoryCircuit.Context.CreateComponent< Router >(
            Name + "_Router", "" );

        StoryCircuit.EntryProcessor = router;
      }

      router = (Router)StoryCircuit.EntryProcessor;
      router.Routes.Clear();

      foreach( NangCondition condition in Conditions )
      {
        Comparer comparer =
          condition.BuildCircuit( StoryCircuit.Context );

        if( comparer != null )
        {
          router.Routes.Add( comparer );
        }
      }

      return StoryCircuit;
    }

    //-------------------------------------------------------------------------

    public void ChangeType( IValue.Type type )
    {
      Value = NangValue.Create( type );
    }

    //-------------------------------------------------------------------------

    public NangCondition CreateCondition(
      NangStory referenceStory,
      ICondition.ComparisonType comparisonType,
      dynamic comparisonValue,
      ICondition.ActionType actionWhenTrue,
      dynamic actionValueWhenTrue,
      ICondition.ActionType actionWhenFalse,
      dynamic actionValueWhenFalse )
    {
      NangCondition condition =
        new NangCondition( Name + '_' + referenceStory?.Name );

      condition.ReferenceStory = referenceStory;
      condition.Comparison = comparisonType;
      condition.ComparisonValue = comparisonValue;
      condition.ActionWhenTrue = actionWhenTrue;
      condition.ActionValueWhenTrue = actionValueWhenTrue;
      condition.ActionWhenFalse = actionWhenFalse;
      condition.ActionValueWhenFalse = actionValueWhenFalse;

      Conditions.Add( condition );

      return condition;
    }

    //-------------------------------------------------------------------------
  }
}
