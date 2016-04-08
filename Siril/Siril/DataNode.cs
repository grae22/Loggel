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
        <DataNodeCollection>
        </DataNodeCollection>
      </DataNode>
    */

    //-------------------------------------------------------------------------
    
    public string Name { get; private set; }
    private Dictionary<string, string> Members { get; set; } = new Dictionary<string, string>();
    public Dictionary<string, DataNode> DataNodes { get; private set; } = new Dictionary<string, DataNode>();

    //-------------------------------------------------------------------------

    public DataNode( string name )
    {
      Name = name;
    }

    //-------------------------------------------------------------------------

    public void AddMember<T>(
      string name,
      T value )
    {
      Members.Add( name, value.ToString() );
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

    public DataNode AddDataNode( string name )
    {
      DataNode node = new DataNode( name );
      DataNodes.Add( name, node );
      return node;
    }

    //-------------------------------------------------------------------------
  }
}
