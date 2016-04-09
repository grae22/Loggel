using System.Reflection;

namespace Siril
{
  public abstract class SirilObject
  {
    //-------------------------------------------------------------------------

    private DataNode Data { get; set; }

    //-------------------------------------------------------------------------

    public SirilObject( string name )
    {
      Data = new DataNode( name );
    }

    //-------------------------------------------------------------------------

    // Implementors should use the SetSnapshotMember() method to save a
    // member's value.
    public abstract void PerformSnapshot();

    //-------------------------------------------------------------------------

    protected void SetSnapshotMember<T>(
      string name,
      T value )
    {
      Data.SetMember<T>( name, value );
    }

    //-------------------------------------------------------------------------

    // Performs a snapshot of the given object, then checks that object for
    // properties & member vars that are also SirilObjects and recurses
    // on them.
    public static void RecursivePerformSnapshot( SirilObject ob )
    {
      ob.PerformSnapshot();

      // Fields.
      foreach( FieldInfo info in ob.GetType().GetFields() )
      {
        if( info.DeclaringType is SirilObject )
        {
          RecursivePerformSnapshot( ob );//as SirilObject );
        }
      }

      // Properties.
      foreach( PropertyInfo info in ob.GetType().GetProperties() )
      {
        if( info.PropertyType is SirilObject )
        {
          RecursivePerformSnapshot( ob );//as SirilObject );
        }
      }
    }

    //-------------------------------------------------------------------------
  }
}
