﻿using System;

namespace Loggel.Nang
{
  internal class NangStory
  {
    //-------------------------------------------------------------------------

    public string Name { get; set; }
    public NangValue Value { get; private set; }
    public NangCondition Condition { get; private set; }
    public Circuit StoryCircuit { get; private set; }

    //-------------------------------------------------------------------------

    public NangStory ReferenceStory
    {
      get
      {
        if( Condition != null )
        {
          return Condition.ReferenceStory;
        }
        
        return null;
      }

      set
      {
        if( Condition != null &&
            Condition.ReferenceStory == ReferenceStory )
        {
          return;
        }

        if( Condition == null )
        {
          Condition = new NangCondition( Name + "_Condition" );
        }

        Condition.ReferenceStory = value;
      }
    }

    //=========================================================================

    public NangStory(
      string name,
      NangValue.NangValueType valueType )
    {
      Name = name;
      Value = NangValue.Create( valueType );
    }

    //-------------------------------------------------------------------------

    public Circuit BuildCircuit()
    {
      if( Name == null || Name.Length == 0 )
      {
        throw new Exception( "Story has no name." );
      }

      StoryCircuit =
        ComponentFactory.CreateCircuit(
          Name,
          Value.GetValue() );

      if( Condition != null )
      {
        StoryCircuit.EntryProcessor =
          Condition.BuildCircuit( StoryCircuit.Context );
      }

      return StoryCircuit;
    }

    //-------------------------------------------------------------------------
  }
}
