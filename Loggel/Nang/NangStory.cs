using System;

namespace Loggel.Nang
{
  internal class NangStory
  {
    //-------------------------------------------------------------------------

    public string Name { get; set; }
    public NangValue Value { get; private set; }
    public NangCondition Condition { get; private set; } = new NangCondition();

    //-------------------------------------------------------------------------

    public NangStory(
      string name,
      NangValue.NangValueType valueType )
    {
      Name = name;
      Value = NangValue.Create( valueType );
    }

    //-------------------------------------------------------------------------

    public void BuildCircuit()
    {
      if( Name == null || Name.Length == 0 )
      {
        throw new Exception( "Story has no name." );
      }

      ComponentFactory.CreateCircuit(
        Name,
        Value.GetValue() );

      Condition.BuildCircuit();
    }

    //-------------------------------------------------------------------------
  }
}
