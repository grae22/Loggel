using System.Collections.Generic;
using System.Xml;

namespace Loggel
{
  public abstract class Processor : Component
  {
    //-------------------------------------------------------------------------

    // Reference to the context of the circuit to which this processor belongs.
    protected Circuit.CircuitContext CircuitContext { get; set; }

    // List of this processor's output sockets.
    private Dictionary<string, Socket> OutputSockets { get; set; } = new Dictionary<string, Socket>();

    //-------------------------------------------------------------------------

    public Processor( string name,
                      Circuit.CircuitContext circuitContext )
    {
      Name = name;
      CircuitContext = circuitContext;
    }

    //-------------------------------------------------------------------------

    protected Socket GetNewOutputSocket(
      string name,
      string description )
    {
      Socket socket = new Socket( name );
      socket.Description = description;

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
      XmlElement socketCollection = ownerDoc.CreateElement( "SocketCollection" );
      processorElement.AppendChild( socketCollection );

      foreach( Socket socket in OutputSockets.Values )
      {
        socket.GetAsXml( socketCollection );
      }
      
      return processorElement;
    }

    //-------------------------------------------------------------------------
  }
}
