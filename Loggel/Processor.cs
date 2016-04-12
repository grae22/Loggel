﻿using System.Collections.Generic;
using System.Xml;

namespace Loggel
{
  public abstract class Processor : Component
  {
    //-------------------------------------------------------------------------

    // BasicCircuitEntity.
    public string Name { get; set; } = "Unnamed";
    public string Description { get; set; } = "";

    // Reference to the context of the circuit to which this processor belongs.
    protected Circuit.CircuitContext CircuitContext { get; set; }

    // List of this processor's output sockets.
    public List<Socket> OutputSockets { get; set; } = new List<Socket>();

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

      OutputSockets.Add( socket );

      return socket;
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

    public virtual void GetAsXml( XmlElement parent )
    {
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement processorElement = ownerDoc.CreateElement( "Processor" );
      parent.AppendChild( processorElement );
      
      XmlAttribute nameAttrib = ownerDoc.CreateAttribute( "name" );
      nameAttrib.Value = Name;
      processorElement.Attributes.Append( nameAttrib );

      XmlElement descriptionElement = ownerDoc.CreateElement( "Description" );
      descriptionElement.InnerText = Description;
      processorElement.AppendChild( descriptionElement );
    }

    //-------------------------------------------------------------------------
  }
}
