using System.Collections.Generic;

namespace Siril
{
  public abstract class SirilStore<T>
  {
    //-------------------------------------------------------------------------

    public abstract void WriteToFile( string absFilename, List<SirilObject> rootObjects );
    public abstract void ReadFromFile( string absFilename, out List<SirilObject> rootObjects );
    protected abstract T GenerateContent( DataNode node, T parent, out T content );
    //protected abstract void GenerateObject( /* TODO */ );

    //-------------------------------------------------------------------------

    protected void PerformSnapshot(
      List<SirilObject> rootObjects,
      T documentRoot )
    {
      foreach( SirilObject ob in rootObjects )
      {
        if( ob == null )
        {
          continue;
        }

        List<SirilObject> childObjects = new List<SirilObject>();
        T content;

        // Perform a snapshot of the object.
        ob.PerformSnapshot( childObjects );

        // Generate the actual persistable content.
        T subNodeCollection =
          GenerateContent(
            ob.Data,
            documentRoot,
            out content );

        // Recurse.
        PerformSnapshot( childObjects, subNodeCollection );
      }
    }

    //-------------------------------------------------------------------------
  }
}
