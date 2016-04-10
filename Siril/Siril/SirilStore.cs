using System.Collections.Generic;

namespace Siril
{
  public abstract class SirilStore<T>
  {
    //-------------------------------------------------------------------------

    public abstract bool WriteToFile( string absFilename, List<SirilObject> rootObjects );
    protected abstract T GenerateDataNodeContent( DataNode node, T parent, out T content );

    //-------------------------------------------------------------------------

    protected void Generate(
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
          GenerateDataNodeContent(
            ob.Data,
            documentRoot,
            out content );

        // Recurse.
        Generate( childObjects, subNodeCollection );
      }
    }

    //-------------------------------------------------------------------------
  }
}
