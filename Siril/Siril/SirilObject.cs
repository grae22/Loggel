using System.Collections.Generic;

namespace Siril
{
  public abstract class SirilObject
  {
    //-------------------------------------------------------------------------

    private DataNode Data { get; set; }
    private List<SirilObject> Children { get; set; } = new List<SirilObject>();

    //-------------------------------------------------------------------------

    public SirilObject( string name )
    {
      Data = new DataNode( name );
    }

    //-------------------------------------------------------------------------

    // Implementors should use the RegisterChild() method to register members
    // who should be snapshot'd too.
    protected abstract void RegisterChildrenToSnapshot();

    //-------------------------------------------------------------------------

    protected void RegisterChild( SirilObject ob )
    {
      if( Children.Contains( ob ) == false )
      {
        Children.Add( ob );
      }
    }

    //-------------------------------------------------------------------------

    // Implementors should use the SetSnapshotMember() method to save a
    // member's value.
    // Implementors should call the base() method to run the logic here.
    public virtual void PerformSnapshot()
    {
      foreach( SirilObject ob in Children )
      {
        ob.PerformSnapshot();
      }
    }

    //-------------------------------------------------------------------------

    protected void SetSnapshotMember<T>(
      string name,
      T value )
    {
      Data.SetMember<T>( name, value );
    }

    //-------------------------------------------------------------------------
  }
}
