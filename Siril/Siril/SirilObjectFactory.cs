using System;
using System.Collections.Generic;

namespace Siril
{
  public class SirilObjectFactory
  {
    //-------------------------------------------------------------------------

    public static Dictionary<string, Type> Types { get; private set; } = new Dictionary<string, Type>();

    //-------------------------------------------------------------------------

    public static void RegisterType(
      string name,
      Type type )
    {
      if( Types.ContainsKey( name ) == false )
      {
        Types.Add( name, type );
      }
    }

    //-------------------------------------------------------------------------

    public static SirilObject CreateObject(
      string typeName,
      string name )
    {
      // Try get the 'type' using the type name.
      var type = Type.GetType( typeName );

      if( type == null )
      {
        // Maybe it was explicitly registered with us.
        if( Types.ContainsKey( typeName ) )
        {
          type = Types[ typeName ];
        }
        else
        {
          throw new ArgumentException( "Unknown type '" + typeName + "'." );
        }
      }

      // Type must inherit from SirilObject.
      if( type.IsSubclassOf( typeof( SirilObject ) ) == false )
      {
        throw new ArgumentException(
          "The specified type ('" + typeName + "') does not inherit from '" +
          typeof( SirilObject ).Name + "'." );
      }

      // Instantiate it - SirilObjects require a name as a constructor arg.
      object[] args = { name };

      SirilObject newObject = (SirilObject)Activator.CreateInstance( type, args );

      if( newObject == null )
      {
        throw new Exception(
          "Failed to instantiate object '" + name + "' of type '" + typeName + "'." );
      }

      return newObject;
    }

    //-------------------------------------------------------------------------
  }
}
