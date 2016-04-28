using System;

namespace Loggel
{
  public class ComponentFactory
  {
    //-------------------------------------------------------------------------

    private static uint NextComponentId { get; set; } = 0;

    //-------------------------------------------------------------------------

    public static T Create<T>(
      string name,
      string description,
      CircuitContext circuitContext ) where T : Component
    {
      // Components take a name, description & circuit-context as constructor args.
      object[] args = { NextComponentId++, name, description, circuitContext };

      T component = (T)Activator.CreateInstance( typeof( T ), args );

      if( component == null )
      {
        throw new Exception(
          "Failed to instantiate component '" + name + "' of type '"
          + typeof( T ).GetType().Name + "'." );
      }

      component.Description = description;

      return component;
    }

    //-------------------------------------------------------------------------

    public static Component Create(
      string typeName,
      uint componentId,
      CircuitContext circuitContext )
    {
      // Components take a name, description & circuit-context as constructor args.
      object[] args = { componentId, "(pending)", "", circuitContext };

      Component component =
        (Component)Activator.CreateInstance(
          Type.GetType( typeName ),
          args );

      // Update the 'next component id' with this id + 1.
      if( NextComponentId <= componentId )
      {
        NextComponentId = componentId + 1;
      }

      return component;
    }

    //-------------------------------------------------------------------------

    public static Circuit CreateCircuit(
      string name,
      dynamic initialValue )
    {
      return
        new Circuit(
          NextComponentId++,
          name,
          "",
          initialValue );
    }

    //-------------------------------------------------------------------------

    public static Circuit CreateCircuit( uint componentId )
    {
      Circuit circuit = new Circuit( componentId );

      // Update the 'next component id' with this id + 1.
      if( NextComponentId <= componentId )
      {
        NextComponentId = componentId + 1;
      }

      return circuit;
    }

    //-------------------------------------------------------------------------
  }
}
