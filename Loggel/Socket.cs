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
    
    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.GetAsXml( parent );

      // Socket xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement socketElement = ownerDoc.CreateElement( "Socket" );
      parent.AppendChild( socketElement );
      
      // Connected processor name.
      //XmlElement connectedProcessorNameElement = ownerDoc.CreateElement( "ConnectedProcessorName" );
      //connectedProcessorNameElement.InnerText = ( ConnectedProcessor == null ? "" : ConnectedProcessor.Name );
      //socketElement.AppendChild( connectedProcessorNameElement );

      XmlElement connectedProcessorElement = ownerDoc.CreateElement( "ConnectedProcessor" );
      socketElement.AppendChild( connectedProcessorElement );
      if( ConnectedProcessor != null )
      {
        ConnectedProcessor.GetAsXml( connectedProcessorElement );
      }

      return socketElement;
    }

    //-------------------------------------------------------------------------
  }
}
