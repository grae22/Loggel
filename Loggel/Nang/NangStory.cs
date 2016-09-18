using System;

namespace Loggel.Nang
{
  public abstract class IStory
  {
    public abstract string GetName();
    public abstract IValue.Type GetValueType();
  }

  //===========================================================================

  internal class NangStory : IStory
  {
    //-------------------------------------------------------------------------

    public string Name { get; set; }
    public NangValue Value { get; private set; }
    public NangCondition Condition { get; private set; }
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
      IValue.Type valueType )
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
