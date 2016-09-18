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

    public void SetReferenceStory( IStory story, IStory referenceStory )
    {
      NangStory nangStory = (NangStory)story;
      NangStory nangRefStory = (NangStory)referenceStory;
      
      nangStory.ReferenceStory = nangRefStory;
      nangStory.BuildCircuit();
    }

    //-------------------------------------------------------------------------

    public void RunCircuit()
    {
      foreach( NangStory story in Stories.Values )
      {
        story.StoryCircuit.Process();
      }
    }

    //-------------------------------------------------------------------------
  }
}