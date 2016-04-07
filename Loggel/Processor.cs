using System.Collections.Generic;

namespace Loggel
{
  public abstract class Processor : BasicCircuitEntity
  {
    //-------------------------------------------------------------------------
    // PROPERTIES.

    // Reference to the context of the circuit to which this processor belongs.
    // TODO: Make protected when processor factory is in place.
    public Circuit.CircuitContext CircuitContext { get; set; }

    // List of this processor's output sockets.
    public List<Socket> OutputSockets { get; set; } = new List<Socket>();

    //-------------------------------------------------------------------------
    // METHODS.

    public Processor( Circuit.CircuitContext circuitContext )
    {
      CircuitContext = circuitContext;
    }

    //-------------------------------------------------------------------------

    protected Socket GetNewOutputSocket(
      string name,
      string description )
    {
      Socket socket = new Socket();
      socket.Name = name;
      socket.Description = description;

      OutputSockets.Add( socket );

      return socket;
    }

    //-------------------------------------------------------------------------
    // ABSTRACT METHODS.

    // When a processor's input-socket is live, this will be called to allow
    // the processor to perform logic.
    // The return value should be the next processor whose logic must be
    // performed. A return value of null indicates processing is complete for
    // this circuit pass.
    public abstract Processor Process();

    //-------------------------------------------------------------------------
  }
}
