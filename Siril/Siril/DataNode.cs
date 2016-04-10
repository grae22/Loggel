using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Siril
{
  public class DataNode
  {
    //-------------------------------------------------------------------------
    // Data-node visualised as xml.

    /*
      <DataNode>
        <Name>SomeObject</Name>
        <MemberCollection>
          <Member>
            <Name>SomeMember</Name>
            <Value>SomeValue</Value>
          </Member>
        </MemberCollection>
      </DataNode>
    */

    //-------------------------------------------------------------------------
    
    public string Name { get; private set; }
    public Dictionary<string, string> Members { get; private set; } = new Dictionary<string, string>();

    //-------------------------------------------------------------------------

    public DataNode( string name )
    {
      Name = name;
    }

    //-------------------------------------------------------------------------

    public void SetMember<T>(
      string name,
      T value )
    {
      if( Members.ContainsKey( name ) )
      {
        Members[ name ] = value.ToString();
      }
      else
      {
        Members.Add( name, value.ToString() );
      }
    }

    //-------------------------------------------------------------------------

    public T GetMember<T>( string name )
    {
      // Member unknown?
      if( Members.ContainsKey( name ) == false )
      {
        throw new KeyNotFoundException( "'" + name + "' member not found." );
      }

      // Try convert to required type.
      var converter = TypeDescriptor.GetConverter( typeof( T ) );

      if( converter == null )
      {
        throw new Exception(
          "No converter found to convert to type '" + typeof( T ).Name + "' for " +
          "member '" + name + "' with value '" + Members[ name ] + "'" );
      }

      return (T)converter.ConvertFromString( Members[ name ] );
    }

    //-------------------------------------------------------------------------
  }
}
