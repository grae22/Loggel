using System;

namespace Loggel
{
  class ComponentFactory
  {
    //-------------------------------------------------------------------------

    public static Component Create<T>( string name )
      where T : Component
    {
      T component = (T)Activator.CreateInstance<T>();

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
