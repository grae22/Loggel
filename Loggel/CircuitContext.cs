using System.Collections.Generic;

namespace Loggel
{
  public class CircuitContext
  {
    //-------------------------------------------------------------------------

    public dynamic Value { get; set; }

    private Circuit Circuit { get; set; }
    private Dictionary<string, Component> Components { get; set; }

    //-------------------------------------------------------------------------

    public string Name
    {
      get
      {
        return Circuit.Name;
      }
    }

    //-------------------------------------------------------------------------

    public CircuitContext( Circuit circuit )
    {
      Circuit = circuit;
    }

    //-------------------------------------------------------------------------

    // Returns component if it already exists, creates one if it doesn't.

    public Component GetComponent<T>(
      string name,
      bool createIfNotFound ) where T : Component
    {
      Component component = null;

      // Already exists?
      if( Components.ContainsKey( name ) )
      {
        component = Components[ name ];
      }
      // Doesn't already exist, create it?
      else if( createIfNotFound )
      {
        component = ComponentFactory.Create<T>( name );
      }

      return component;
    }

    //-------------------------------------------------------------------------
  }
}
