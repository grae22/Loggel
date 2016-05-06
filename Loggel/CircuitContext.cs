using System;
using System.Collections.Generic;

namespace Loggel
{
  public class CircuitContext
  {
    //-------------------------------------------------------------------------

    public static Dictionary<uint, CircuitContext> AllContexts { get; private set; } = new Dictionary<uint, CircuitContext>();

    public dynamic Value { get; set; }
    public Dictionary<uint, Component> Components { get; private set; } = new Dictionary<uint, Component>();

    private Circuit Circuit { get; set; }

    //-------------------------------------------------------------------------

    public uint Id
    {
      get
      {
        return Circuit.Id;
      }
    }

    //-------------------------------------------------------------------------

    public string Name
    {
      get
      {
        return Circuit.Name;
      }
    }

    //-------------------------------------------------------------------------

    public static CircuitContext CreateContext( Circuit circuit )
    {
      CircuitContext context = new CircuitContext( circuit );

      AllContexts.Add( context.Id, context );

      return context;
    }

    //-------------------------------------------------------------------------

    private CircuitContext( Circuit circuit )
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

      // Check name isn't already used.
      foreach( Component c in Components.Values )
      {
        if( c.Name == name )
        {
          throw new ArgumentException( "The name '" + name + "' is already in use." );
        }
      }

      // Create it?
      component = ComponentFactory.Create<T>( name, description, this );

      if( component == null )
      {
        throw new Exception(
          "Failed to create component '" + name + "' of type '" + typeof( T ).Name + "'." );
      }

      Components.Add( component.Id, component );

      return component;
    }

    //-------------------------------------------------------------------------

    public Component CreateComponent(
      string typeName,
      uint componentId )
    {
      Component component = null;

      // Must have a type name.
      if( typeName.Length == 0 )
      {
        throw new ArgumentException( "Component must have a type-name." );
      }

      // Create it?
      component = ComponentFactory.Create( typeName, componentId, this );

      if( component == null )
      {
        throw new Exception(
          "Failed to create component of type '" + typeName + "." );
      }

      Components.Add( component.Id, component );

      return component;
    }
    
    //-------------------------------------------------------------------------

    public Component GetComponent( string name )
    {
      foreach( Component component in Components.Values )
      {
        if( component.Name == name )
        {
          return component;
        }
      }

      return null;
    }

    //-------------------------------------------------------------------------

    // Converts values in string form into the type of this circuit. This is
    // typically used when restoring processors from xml.

    public dynamic ConvertToCircuitValueType( string valueToConvert )
    {
      //valueToConvert = ( valueToConvert.Length == 0 ? "0" : valueToConvert );
      if( valueToConvert.Length == 0 )
      {
        return null;
      }

      return Convert.ChangeType( valueToConvert, Value.GetType() );
    }

    //-------------------------------------------------------------------------
  }
}
