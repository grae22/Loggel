using System;

namespace Loggel
{
  class ComponentFactory
  {
    //-------------------------------------------------------------------------

    public static T Create<T>(
      string name,
      CircuitContext circuitContext ) where T : Component
    {
      // Components take a name & circuit-context as constructor args.
      object[] args = { name, circuitContext };

      T component = (T)Activator.CreateInstance( typeof( T ), args );

      if( component == null )
      {
        throw new Exception(
          "Failed to instantiate component '" + name + "' of type '"
          + typeof( T ).GetType().Name + "'." );
      }

      return component;
    }

    //-------------------------------------------------------------------------
  }
}
