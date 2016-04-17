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

    public Socket(
      uint id,
      string name,
      string description,
      CircuitContext circuitContext )
    :
      base( id, name, description, circuitContext )
    {

    }

    //-------------------------------------------------------------------------

    public void Process( CircuitContext context )
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
      
      // Connected processor id.
      XmlElement connectedProcessorIdElement = ownerDoc.CreateElement( "ConnectedProcessorId" );
      connectedProcessorIdElement.InnerText = ( ConnectedProcessor == null ? "" : ConnectedProcessor.Id.ToString() );
      socketElement.AppendChild( connectedProcessorIdElement );

      return socketElement;
    }

    //-------------------------------------------------------------------------

    // Restore this instance from xml.

    public override XmlElement RestoreFromXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.RestoreFromXml( parent );

      // Socket.
      XmlElement socketElement = parent[ "Socket" ];

      // Connected processor.
      XmlElement connectedProcessorIdElement = socketElement[ "ConnectedProcessorId" ];
      if( connectedProcessorIdElement.InnerText.Length > 0 )
      {
        uint id = uint.Parse( connectedProcessorIdElement.InnerText );
        ConnectedProcessor = (Processor)Context.Components[ id ];
      }

      return socketElement;
    }

    //-------------------------------------------------------------------------
  }
}
