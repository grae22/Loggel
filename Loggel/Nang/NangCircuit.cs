using System.Collections.Generic;

namespace Loggel.Nang
{
  internal class NangCircuit
  {
    //-------------------------------------------------------------------------

    // All the stories belonging to this circuit.
    private Dictionary<string, NangStory> Stories { get; set; } =
      new Dictionary<string, NangStory>();

    //-------------------------------------------------------------------------
  }
}
