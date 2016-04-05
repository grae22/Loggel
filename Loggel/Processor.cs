using System;
using System.Collections.Generic;

namespace Loggel
{
  public abstract class Processor<T> : BasicCircuitEntity
    where T : IComparable
  {
    //-------------------------------------------------------------------------
    // Properties.

    // List of this processor's output sockets.
    public List<Socket<T>> OutputSockets { get; set; } = new List<Socket<T>>();

    //-------------------------------------------------------------------------
    // Methods.

    public Processor()
    {

    }

    //-------------------------------------------------------------------------

    protected Socket<T> AddOutputSocket(
      string name,
      string description )
    {
      Socket<T> socket = new Socket<T>();
      socket.Name = name;
      socket.Description = description;

      OutputSockets.Add( socket );

      return socket;
    }

    //-------------------------------------------------------------------------
    // Abstract methods.

    // When a processor's input-socket is live, this will be called to allow
    // the processor to perform logic.
    // The return value should be the next processor whose logic must be
    // performed. A return value of null indicates processing is complete for
    // this circuit pass.
    public abstract Processor<T> Process( Circuit<T>.CircuitContext context );

    //-------------------------------------------------------------------------
  }
}
