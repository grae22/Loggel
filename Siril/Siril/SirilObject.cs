using System.Collections.Generic;

namespace Siril
{
  public abstract class SirilObject
  {
    //-------------------------------------------------------------------------

    public DataNode Data { get; private set; }

    //-------------------------------------------------------------------------

    public SirilObject( string name )
    {
      Data = new DataNode( name );
    }

    //-------------------------------------------------------------------------

    // Implementors should use the SetSnapshotMember() method to save a
    // member's value.
    //
    // children: List of object's children to be snapshot'd next.
    public abstract void PerformSnapshot( List<SirilObject> children );

    //-------------------------------------------------------------------------

    protected void SetSnapshotMember<T>(
      string name,
      T value )
    {
      if( value != null )
      {
        Data.SetMember<T>( name, value );
      }
    }

    //-------------------------------------------------------------------------
  }
}
