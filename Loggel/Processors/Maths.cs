using System;
using Loggel;

namespace Loggel.Processors
{
  class Maths<T> : Processor<T>
    where T : IComparable
  {
    //-------------------------------------------------------------------------

    //public enum

    //-------------------------------------------------------------------------

    public override Processor<T> Process( Circuit<T>.CircuitContext context )
    {
      if( context.Value is int )
      {
        
      }


      return null;
    }

    //-------------------------------------------------------------------------
  }
}
