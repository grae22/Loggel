namespace Loggel
{
  public class CircuitContext
  {
    //-------------------------------------------------------------------------

    private Circuit Circuit { get; set; }

    public dynamic Value { get; set; }

    //-------------------------------------------------------------------------

    public string Name
    {
      get
      {
        return Circuit.Name;
      }
    }

    //-------------------------------------------------------------------------

    public CircuitContext( Circuit circuit )
    {
      Circuit = circuit;
    }

    //-------------------------------------------------------------------------
  }
}
