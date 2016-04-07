namespace Loggel
{
  public class Circuit : BasicCircuitEntity
  {
    //-------------------------------------------------------------------------
    // TYPES.

    // Circuit context class, provides limited access to aspects of this class.
    public class CircuitContext
    {
      // The curent value of this circuit.
      public dynamic Value { get; set; }
    }

    //-------------------------------------------------------------------------
    // PROPERTIES.

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
    // METHODS.

    public Circuit( dynamic initialValue )
    {
      Context.Value = initialValue;
    }

    //-------------------------------------------------------------------------
    // TODO: Remove this method, when we have a processor factory it should
    //       no longer be necessary.

    public void RegisterProcessor(
      Processor processor,
      bool setAsEntryProcessor )
    {
      processor.CircuitContext = Context;

      if( setAsEntryProcessor )
      {
        EntryProcessor = processor;
      }
    }

    //-------------------------------------------------------------------------

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
  }
}