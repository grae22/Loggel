using System.Xml;

namespace Loggel
{
  public class Circuit : Component
  {
    //-------------------------------------------------------------------------

    // Circuit context class, provides limited access to aspects of this class.

    public class CircuitContext
    {
      // The curent value of this circuit.
      public dynamic Value { get; set; }
    }

    //-------------------------------------------------------------------------

    // Inital value of this circuit.
    private dynamic InitialValue { get; set; }

    // This circuit's context struct.
    public CircuitContext Context { get; private set; } = new CircuitContext();

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
      Context.Value = initialValue;
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
      parent = base.GetAsXml( parent );

      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement circuitElement = ownerDoc.CreateElement( "Circuit" );
      parent.AppendChild( circuitElement );
      
      XmlElement initialValueElement = ownerDoc.CreateElement( "InitialValue" );
      initialValueElement.InnerText = InitialValue.ToString();
      circuitElement.AppendChild( initialValueElement );

      XmlElement entryProcessorNameElement = ownerDoc.CreateElement( "EntryProcessorName" );
      entryProcessorNameElement.InnerText = ( EntryProcessor == null ? "" : EntryProcessor.Name );
      circuitElement.AppendChild( entryProcessorNameElement );

      return circuitElement;
    }

    //-------------------------------------------------------------------------
  }
}