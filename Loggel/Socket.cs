using System.Xml;

namespace Loggel
{
  public class Socket : Component
  {
    //-------------------------------------------------------------------------

    // The connected processor (if any).
    public Processor ConnectedProcessor { get; set; } = null;

    //-------------------------------------------------------------------------

    public bool IsConnected
    {
      get
      {
        return ( ConnectedProcessor != null );
      }
    }

    //-------------------------------------------------------------------------

    public Socket( string name )
    {
      Name = name;
    }

    //-------------------------------------------------------------------------

    public void Process( Circuit.CircuitContext context )
    {
      if( ConnectedProcessor != null )
      {
        ConnectedProcessor.Process();
      }
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.
    /*
    void Component.GetAsXml( XmlElement parent )
    {
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement socketElement = ownerDoc.CreateElement( "Socket" );
      parent.AppendChild( socketElement );
      
      XmlAttribute nameAttrib = ownerDoc.CreateAttribute( "name" );
      nameAttrib.Value = Name;
      socketElement.Attributes.Append( nameAttrib );

      XmlElement descriptionElement = ownerDoc.CreateElement( "Description" );
      descriptionElement.InnerText = Description;
      socketElement.AppendChild( descriptionElement );

      XmlElement connectedProcessorNameElement = ownerDoc.CreateElement( "ConnectedProcessorName" );
      connectedProcessorNameElement.InnerText = ( ConnectedProcessor == null ? "" : ConnectedProcessor.Name );
      socketElement.AppendChild( connectedProcessorNameElement );
    }
    */
    //-------------------------------------------------------------------------
  }
}
