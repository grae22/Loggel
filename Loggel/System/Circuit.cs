using System;

namespace Loggel
{
  public class Circuit<T> : BasicCircuitEntity
    where T : IComparable
  {
    //-------------------------------------------------------------------------
    // TYPES.

    // Circuit context class, provides limited access to aspects of this class.
    public class CircuitContext
    {
      // The curent value of this circuit.
      public T Value { get; set; }
    }

    //-------------------------------------------------------------------------
    // PROPERTIES.

    // This circuit's context struct.
    private CircuitContext Context { get; set; } = new CircuitContext();

    // The processor whose Process() method will be called to kick-off
    // processing of this circuit.
    public Processor<T> EntryProcessor { get; set; } = null;

    //-------------------------------------------------------------------------

    public T Value
    {
      get
      {
        return Context.Value;
      }
    }

    //-------------------------------------------------------------------------
    // METHODS.

    public Circuit( T initialValue )
    {
      Context.Value = initialValue;
    }

    //-------------------------------------------------------------------------

    public void Process()
    {
      // Each processor will return the next processor to process, continue
      // until there isn't one.
      Processor<T> processorToProcess = EntryProcessor;

      while( processorToProcess != null )
      {
        processorToProcess = processorToProcess.Process( Context );
      }
    }

    //-------------------------------------------------------------------------
  }
}