using System;

namespace Loggel
{
  class ComponentFactory
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
      string type,
      string name )
    {
      return null;
    }

    //-------------------------------------------------------------------------
  }
}
