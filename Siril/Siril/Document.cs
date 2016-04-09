namespace Siril
{
  public abstract class Document<T>
  {
    //-------------------------------------------------------------------------

    public abstract bool WriteToFile( string absFilename, DataNode rootNode );
    protected abstract T GenerateDataNodeContent( DataNode node, T parent, out T content );

    //-------------------------------------------------------------------------

    public void Generate(
      T documentRoot,
      SirilObject rootObject )
    {
      rootObject.PerformSnapshot();

      RecursiveGenerateDataNodeContent( documentRoot, rootNode );
    }

    //-------------------------------------------------------------------------

    private void RecursiveGenerateDataNodeContent(
      T parent,
      DataNode node )
    {
      T content;
      T subNodeCollection = GenerateDataNodeContent( node, parent, out content );

      foreach( DataNode subNode in node.DataNodes.Values )
      {
        RecursiveGenerateDataNodeContent( subNodeCollection, subNode );
      }
    }

    //-------------------------------------------------------------------------
  }
}
