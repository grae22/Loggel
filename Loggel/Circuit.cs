using System;

namespace Loggel
{
  public class Circuit<T> where T : IComparable
  {
    //-------------------------------------------------------------------------

    public string Name { get; set; }
    public T Value { get; set; }

    //-------------------------------------------------------------------------

    public Circuit( T initialValue )
    {
      Name = "Unnamed";
      Value = initialValue;
    }

    //-------------------------------------------------------------------------
  }
}