using System.Collections.Generic;
using System.Xml;

namespace Loggel
{
  public abstract class Processor : Component
  {
    //-------------------------------------------------------------------------

    // Processors connected to this processor, mapped id to processor.
    // This is only used when loading from xml.
    public Dictionary<string, Processor> ConnectedProcessors { get; private set; } =
      new Dictionary<string, Processor>();

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

    protected Processor GetConnectedProcessor( string name )
    {
      Processor processor = null;

      if( ConnectedProcessors.ContainsKey( name ) )
      {
        processor = ConnectedProcessors[ name ];
      }

      return processor;
    }

    //-------------------------------------------------------------------------

    protected string[] GetConnectedProcessorKeys()
    {
      string[] keys = new string[ ConnectedProcessors.Count ];

      int i = 0;
      foreach( string k in ConnectedProcessors.Keys )
      {
        keys[ i++ ] = k;
      }

      return keys;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public XmlElement GetAsXml(
      XmlElement parent,
      Dictionary<string, Processor> processors )
    {
      // Must call base method.
      parent = base.GetAsXml( parent );

      // Processor xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement processorElement = ownerDoc.CreateElement( "Processor" );
      parent.AppendChild( processorElement );

      // Connected processors.
      XmlElement connectedProcessorIdCollection =
        ownerDoc.CreateElement( "ConnectedProcessorIdCollection" );
      processorElement.AppendChild( connectedProcessorIdCollection );

      foreach( string name in processors.Keys )
      {
        Processor processor = processors[ name ];

        if( processor == null )
        {
          continue;
        }

        XmlElement processorIdElement = ownerDoc.CreateElement( "ProcessorId" );
        connectedProcessorIdCollection.AppendChild( processorIdElement );
        processorIdElement.InnerText = processor.Id.ToString();
        XmlAttribute internalNameAttrib = ownerDoc.CreateAttribute( "internalName" );
        processorIdElement.Attributes.Append( internalNameAttrib );
        internalNameAttrib.Value = name;
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

      // Connected processors.
      XmlElement connectedProcessorIdCollection = processorElement[ "ConnectedProcessorIdCollection" ];

      foreach( XmlElement processorIdElement in connectedProcessorIdCollection.SelectNodes( "ProcessorId" ) )
      {
        string internalName = processorIdElement.Attributes[ "internalName" ].Value;

        uint id = uint.Parse( processorIdElement.InnerText );
        Processor processors = (Processor)Context.Components[ id ];
        ConnectedProcessors.Add( internalName, processors );
      }

      return processorElement;
    }

    //-------------------------------------------------------------------------
  }
}
