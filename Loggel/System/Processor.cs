using System;
using System.Collections.Generic;

namespace Loggel.System
{
  public abstract class Processor : BasicCircuitEntity
  {
    //-------------------------------------------------------------------------
    // PROPERTIES.

    // List of this processor's output sockets.
    public List<Socket> OutputSockets { get; set; } = new List<Socket>();

    //-------------------------------------------------------------------------
    // METHODS.

    protected Socket AddOutputSocket(
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
    public abstract Processor Process( Circuit.CircuitContext context );

    //-------------------------------------------------------------------------
  }
}
