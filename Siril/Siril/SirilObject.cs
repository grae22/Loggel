using System.Collections.Generic;

namespace Siril
{
  public abstract class SirilObject
  {
    //-------------------------------------------------------------------------

    // TODO: Refactor so that this is only accessible within Siril.
    public DataNode Data { get; private set; }

    //-------------------------------------------------------------------------

    // Constructor to use when creating a new object.

    public SirilObject( string name )
    {
      Data = new DataNode( name );
    }

    //-------------------------------------------------------------------------

    // Constructor to use when restoring a saved object.

    public SirilObject( SirilObject ob )
    {
      Data = ob.Data;
    }

    //-------------------------------------------------------------------------

    // Implementors should use the SnapshotMember() method to save a
    // member's value.
    // Implementors should call the base method to ensure the snapshot is
    // performed all the way up the inheritance chain.
    //
    // children: List of object's children to be snapshot'd next.

    public abstract void PerformSnapshot( List<SirilObject> children );

    //-------------------------------------------------------------------------

    // Implementors should use the RestoreMember() method to save a
    // member's value.
    // Implementors should call the base method to ensure the snapshot is
    // restored all the way up the inheritance chain.

    public abstract void RestoreSnapshot();

    //-------------------------------------------------------------------------

    protected void SnapshotMember<T>(
      string name,
      T value )
    {
      if( value != null )
      {
        Data.SetMember<T>( name, value );
      }
    }

    //-------------------------------------------------------------------------

    protected T RestoreMember<T>(
      string name,
      T defaultValue )
    {
      T value;

      try
      {
        value = Data.GetMember<T>( name );
      }
      catch( KeyNotFoundException )
      {
        value = defaultValue;
      }

      return value;
    }

    //-------------------------------------------------------------------------
  }
}
