using System;
using System.Collections.Generic;

namespace Loggel
{
  public class CircuitContext
  {
    //-------------------------------------------------------------------------

    // Static collection of all contexts.
    public static Dictionary<uint, CircuitContext> AllContexts { get; private set; } = new Dictionary<uint, CircuitContext>();

    // All components in this circuit.
    public Dictionary<uint, Component> Components { get; private set; } = new Dictionary<uint, Component>();

    // The circuit.
    private Circuit Circuit { get; set; }

    //-------------------------------------------------------------------------

    // This stores the value of the circuit.

    private dynamic _value;
    private bool? _valueIsNumeric = null;

    public dynamic Value
    {
      get
      {
        return _value;
      }

      set
      {
        _value = value;

        if( _valueIsNumeric == null )
        {
          _valueIsNumeric = !( _value.GetType() == typeof( string ) );
        }
      }
    }

    public bool ValueIsNumeric
    {
      get
      {
        return ( _valueIsNumeric == true );
      }
    }

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
      string description,
      bool appendNumberIfNameExists = false ) where T : Component
    {
      T component = null;

      // Must have a name.
      if( name.Length == 0 )
      {
        throw new ArgumentException( "Component must have a name." );
      }

      // Check name isn't already used.
      bool checkingName = true;
      int number = 1;

      while( checkingName )
      {
        bool nameAlreadyExists = false;

        foreach( Component c in Components.Values )
        {
          if( c.Name == name )
          {
            if( appendNumberIfNameExists == false )
            {
              throw new ArgumentException( "The name '" + name + "' is already in use." );
            }

            // Append a number to the name and start again.
            nameAlreadyExists = true;
            name += number.ToString();
            number++;
            break;
          }
        }

        checkingName = nameAlreadyExists;
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
      if( valueToConvert.Length == 0 )
      {
        return null;
      }

      return Convert.ChangeType( valueToConvert, Value.GetType() );
    }

    //-------------------------------------------------------------------------
  }
}
