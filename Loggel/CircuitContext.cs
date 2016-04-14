using System;
using System.Collections.Generic;

namespace Loggel
{
  public class CircuitContext
  {
    //-------------------------------------------------------------------------

    public dynamic Value { get; set; }
    public Dictionary<string, Component> Components { get; private set; } = new Dictionary<string, Component>();

    private Circuit Circuit { get; set; }

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

    public T CreateComponent<T>(
      string name,
      string description ) where T : Component
    {
      T component = null;

      // Must have a name.
      if( name.Length == 0 )
      {
        throw new ArgumentException( "Component must have a name." );
      }

      // Already exists?
      if( Components.ContainsKey( name ) )          
      {
        throw new Exception(
          "Component '" + name + "' (of type '" + typeof( T ).Name + "') already exists." );
      }

      // Doesn't already exist, create it?
      component = ComponentFactory.Create<T>( name, description, this );

      if( component == null )
      {
        throw new Exception(
          "Failed to create component '" + name + "' of type '" + typeof( T ).Name + "'." );
      }

      return component;
    }

    //-------------------------------------------------------------------------

    // Returns component if it already exists, null if not.

    public T GetComponent<T>( string name ) where T : Component
    {
      T component = null;

      if( Components.ContainsKey( name ) )          
      {
        component = (T)Components[ name ];

        if( component == null )
        {
          throw new Exception(
            "Component '" + name + "' is not of type '" +
            typeof( T ).Name + "'." );
        }
      }

      return component;
    }

    //-------------------------------------------------------------------------
  }
}
