using System.Collections.Generic;
using System.Xml;

namespace Loggel
{
  public abstract class Processor : Component
  {
    //-------------------------------------------------------------------------

    // List of this processor's output sockets.
    private Dictionary<string, Socket> OutputSockets { get; set; } = new Dictionary<string, Socket>();

    //-------------------------------------------------------------------------

    public Processor(
      uint id,
      string name,
      string description,
      CircuitContext circuitContext )
    :
      base( id, name, description, circuitContext )
    {

    }

    //-------------------------------------------------------------------------

    protected Socket CreateOutputSocket(
      string name,
      string description )
    {
      Socket socket =
        Context.CreateComponent<Socket>(
          name,
          description );

      OutputSockets.Add( name, socket );

      return socket;
    }

    //-------------------------------------------------------------------------

    protected Socket GetOutputSocket( string name )
    {
      Socket socket = null;

      if( OutputSockets.ContainsKey( name ) )
      {
        socket = OutputSockets[ name ];
      }

      return null;
    }

    //-------------------------------------------------------------------------

    // When a processor's input-socket is live, this will be called to allow
    // the processor to perform logic.
    // The return value should be the next processor whose logic must be
    // performed. A return value of null indicates processing is complete for
    // this circuit pass.
    public abstract Processor Process();

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.GetAsXml( parent );

      // Processor xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement processorElement = ownerDoc.CreateElement( "Processor" );
      parent.AppendChild( processorElement );

      // Sockets.
      XmlElement socketCollection = ownerDoc.CreateElement( "SocketIdCollection" );
      processorElement.AppendChild( socketCollection );

      foreach( Socket socket in OutputSockets.Values )
      {
        XmlElement socketElement = ownerDoc.CreateElement( "SocketId" );
        socketElement.InnerText = socket.Id.ToString();
        socketCollection.AppendChild( socketElement );
      }
      
      return processorElement;
    }

    //-------------------------------------------------------------------------

    // Restore this instance from xml.

    public override XmlElement RestoreFromXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.RestoreFromXml( parent );

      // Processor element.
      XmlElement processorElement = parent[ "Processor" ];

      // Sockets.
      XmlElement socketCollection = parent[ "SocketIdCollection" ];

      foreach( XmlElement socketIdElement in socketCollection.SelectNodes( "SocketId" ) )
      {
        uint id = uint.Parse( socketIdElement.InnerText );
        Socket socket = (Socket)Context.Components[ id ];
        OutputSockets.Add( socket.Name, socket );
      }

      return processorElement;
    }

    //-------------------------------------------------------------------------
  }
}
