using System;

namespace Loggel
{
  public class ProcessorFactory
  {
    //-------------------------------------------------------------------------
    // Instantiates a processor of the required type and initialises it as
    // part of the specified circuit.
    //
    // name: Processor instance's name.
    // description: Description of processor instance's function.
    // circuit: The circuit to which the new processor will belong.
    // setAsCircuitEntryProcessor: Processor will be the ciruit's entry point for processing.

    public static T CreateProcessor<T>(
      string name,
      string description,
      Circuit circuit,
      bool setAsCircuitEntryProcessor ) where T : Processor
    {
      // Processor constructors take the circuit's context as a parameter.
      object[] contextArg = { circuit.Context };

      // Instantiate the processor.
      T processor = (T)Activator.CreateInstance( typeof( T ), contextArg );

      // Set processor's properties.
      // TODO: Handle null case.
      if( processor != null )
      {
        processor.Name = name;
        processor.Description = description;
      }

      // Set processor as circuit's entry processor, if required.
      if( setAsCircuitEntryProcessor )
      {
        circuit.EntryProcessor = processor;
      }

      return processor;
    }

    //-------------------------------------------------------------------------
  }
}
