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
      NangStory story = new NangStory( name, NangValue.NangValueType.DECIMAL );

      story.BuildCircuit();

      Stories.Add( name, story );

      return story;
    }

    //-------------------------------------------------------------------------
  }
}