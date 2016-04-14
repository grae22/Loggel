using System.Xml;

namespace Loggel
{
  public class Circuit : Component
  {
    //-------------------------------------------------------------------------

    // Inital value of this circuit.
    private dynamic InitialValue { get; set; }

    // This circuit's context struct.
    public CircuitContext Context { get; private set; }

    // The processor whose Process() method will be called to kick-off
    // processing of this circuit.
    public Processor EntryProcessor { get; set; } = null;

    //-------------------------------------------------------------------------

    public dynamic Value
    {
      get
      {
        return Context.Value;
      }
    }

    //-------------------------------------------------------------------------
    
    // Class constructor.

    public Circuit(
      string name,
      dynamic initialValue )
    {
      Name = name;
      InitialValue = initialValue;

      Context = new CircuitContext( this );
      Context.Value = initialValue;
    }

    //-------------------------------------------------------------------------

    public Circuit( XmlElement parent )
    {
      Context = new CircuitContext( this );

      RestoreFromXml( parent );
    }

    //-------------------------------------------------------------------------

    // Obtains and returns (from the processor factory) a new processor of
    // the specified type.

    public T CreateProcessor<T>(
      string name,
      string description,
      bool setAsEntryProcessor ) where T : Processor
    {
      return
        ProcessorFactory.CreateProcessor<T>(
          name,
          description,
          this,
          setAsEntryProcessor );
    }

    //-------------------------------------------------------------------------

    // We allow the circuit's 'entry' processor to perform logic, it
    // will return the next processor that requires processing and we will
    // let it process. We keep calling the returned processor's Process()
    // method until there is no next processor (i.e. null is returned).

    public void Process()
    {
      // Each processor will return the next processor to process, continue
      // until there isn't one.
      Processor processorToProcess = EntryProcessor;

      while( processorToProcess != null )
      {
        processorToProcess = processorToProcess.Process();
      }
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.GetAsXml( parent );

      // Circuit xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement circuitElement = ownerDoc.CreateElement( "Circuit" );
      parent.AppendChild( circuitElement );
      
      XmlElement initialValueElement = ownerDoc.CreateElement( "InitialValue" );
      initialValueElement.InnerText = InitialValue.ToString();
      circuitElement.AppendChild( initialValueElement );

      XmlElement entryProcessorNameElement = ownerDoc.CreateElement( "EntryProcessorName" );
      entryProcessorNameElement.InnerText = ( EntryProcessor == null ? "" : EntryProcessor.Name );
      circuitElement.AppendChild( entryProcessorNameElement );

      // Entry processor.
      if( EntryProcessor != null )
      {
        EntryProcessor.GetAsXml( circuitElement );
      }

      return circuitElement;
    }

    //-------------------------------------------------------------------------

    // Restore this instance from xml.

    public override XmlElement RestoreFromXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.RestoreFromXml( parent );

      // Circuit.
      XmlElement circuitElement = parent[ "Circuit" ];
      InitialValue = circuitElement[ "InitialValue" ].InnerText;
      Context.Value = InitialValue;

      //EntryProcessor

      return circuitElement;
    }

    //-------------------------------------------------------------------------
  }
}