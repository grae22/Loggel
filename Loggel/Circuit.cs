using System;
using System.Xml;

namespace Loggel
{
  public class Circuit : Component
  {
    //-------------------------------------------------------------------------

    // Inital value of this circuit.
    private dynamic InitialValue { get; set; }

    // This circuit's context struct.
    public new CircuitContext Context { get; private set; }

    // The processor whose Process() method will be called to kick-off
    // processing of this circuit.
    public Processor EntryProcessor { get; set; } = null;

    //-------------------------------------------------------------------------

    // Class constructor.

    public Circuit(
      uint componentId,
      string name,
      string description,
      dynamic initialValue )
    :
      base( componentId, name, description, null )
    {
      InitialValue = initialValue;
      Context = CircuitContext.CreateContext( this );
      Context.Value = initialValue;
    }

    //-------------------------------------------------------------------------

    // Class constructor for restoring from xml.

    public Circuit( uint componentId )
    :
      base( componentId, "", "", null )
    {
      Context = CircuitContext.CreateContext( this );
    }

    //-------------------------------------------------------------------------

    // We allow the circuit's 'entry' processor to perform logic, it
    // will return the next processor that requires processing and we will
    // let it process. We keep calling the returned processor's Process()
    // method until there is no next processor (i.e. null is returned).

    override public Component Process()
    {
      // Must call base class.
      base.Process();

      // Each processor will return the next processor to process, continue
      // until there isn't one.
      Component componentToProcess = EntryProcessor;

      while( componentToProcess != null )
      {
        componentToProcess = componentToProcess.Process();
      }

      // Circuits don't return a next component to proces..
      return null;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    override public XmlElement GetAsXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.GetAsXml( parent );

      // Circuit xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement circuitElement = ownerDoc.CreateElement( "Circuit" );
      parent.AppendChild( circuitElement );
      
      XmlElement valueTypeElement = ownerDoc.CreateElement( "ValueType" );
      valueTypeElement.InnerText = Context.Value.GetType().FullName;
      circuitElement.AppendChild( valueTypeElement );

      XmlElement initialValueElement = ownerDoc.CreateElement( "InitialValue" );
      initialValueElement.InnerText = InitialValue.ToString();
      circuitElement.AppendChild( initialValueElement );

      XmlElement entryProcessorIdElement = ownerDoc.CreateElement( "EntryProcessorId" );
      entryProcessorIdElement.InnerText = ( EntryProcessor == null ? "" : EntryProcessor.Id.ToString() );
      circuitElement.AppendChild( entryProcessorIdElement );

      return circuitElement;
    }

    //-------------------------------------------------------------------------

    // Restore this instance from xml.

    override public XmlElement RestoreFromXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.RestoreFromXml( parent );

      // Circuit.
      XmlElement circuitElement = parent[ "Circuit" ];

      // Value type.
      string valueTypeName = circuitElement[ "ValueType" ].InnerText;
      Type valueType = Type.GetType( valueTypeName );

      Context.Value = Convert.ChangeType( "0", valueType );

      // Initial value.
      string initialValueAsString = circuitElement[ "InitialValue" ].InnerText;
      InitialValue = Context.ConvertToCircuitValueType( initialValueAsString );
      Context.Value = InitialValue;

      // Entry processor.
      XmlElement entryProcessorIdElement = circuitElement[ "EntryProcessorId" ];
      if( entryProcessorIdElement.InnerText.Length > 0 )
      {
        uint id = uint.Parse( entryProcessorIdElement.InnerText );
        EntryProcessor = (Processor)Context.Components[ id ];
      }

      return circuitElement;
    }

    //-------------------------------------------------------------------------

    // TODO: This is necessary for unit-tests that generate xml, without this
    //       we end up with varying component IDs which cause the tests to
    //       fail. This is a giant hack.

    public static void Reset()
    {
      CircuitContext.AllContexts.Clear();
      ComponentFactory.ResetNextComponentId();
    }

    //-------------------------------------------------------------------------
  }
}