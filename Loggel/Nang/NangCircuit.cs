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
  }
}