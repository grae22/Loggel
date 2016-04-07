using Loggel;

namespace Loggel.Processors
{
  public class Comparer : Processor
  {
    //-------------------------------------------------------------------------
    /*
     * This class determines which (if any) output socket will become live
     * by comparing the circuit value against a comparison value (which may
     * be dynamic if a circuit is provided for this purpose).
     * 
     * NOTE:
     * x Range checks are performed as follows:
     *   x In-range: Inclusive.
     *   x Not in-range: Exclusive.
    */
    //-------------------------------------------------------------------------

    //-- Input circuits.

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // comparison value (if a dynamic comparison value is desired).
    public Circuit Circuit_ComparisonValue { get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // range min value (if a dynamic range min value is desired).
    public Circuit Circuit_RangeMin { get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // range max value (if a dynamic range max value is desired).
    public Circuit Circuit_RangeMax { get; set; }

    //-- Output sockets.
    public Socket OutputSocket_Equal { get; private set; }
    public Socket OutputSocket_NotEqual { get; private set; }
    public Socket OutputSocket_Greater { get; private set; }
    public Socket OutputSocket_Lesser { get; private set; }
    public Socket OutputSocket_InRange { get; private set; }
    public Socket OutputSocket_NotInRange { get; private set; }

    //-- General.
    public dynamic ComparisonValue { get; set; }
    public dynamic RangeMin { get; set; }
    public dynamic RangeMax { get; set; }

    //-------------------------------------------------------------------------

    public Comparer()
    {
      // Create the output sockets.
      OutputSocket_Equal = AddOutputSocket( "Equal", "Circuit and Comparison values are equal." );
      OutputSocket_NotEqual = AddOutputSocket( "NotEqual", "Circuit and Comparison values are not equal." );
      OutputSocket_Greater = AddOutputSocket( "Greater", "Circuit value is greater than Comparison value." );
      OutputSocket_Lesser = AddOutputSocket( "Lesser", "Circuit value is smaller than Comparison value." );
      OutputSocket_InRange = AddOutputSocket( "InRange", "Circuit value falls within (inclusive) specified range." );
      OutputSocket_NotInRange = AddOutputSocket( "NotInRange", "Circuit value falls outside (exclusive) specified range." );
    }

    //-------------------------------------------------------------------------

    public override Processor Process( Circuit.CircuitContext context )
    {
      UpdateComparisonValue();
      UpdateRangeMin();
      UpdateRangeMax();

      return IdentifyOutputSocketToProcess( context );
    }
    
    //-------------------------------------------------------------------------
    // If we have a circuit for dynamically updating the comparison-value,
    // use it to update now.

    private void UpdateComparisonValue()
    {
      if( Circuit_ComparisonValue != null )
      {
        ComparisonValue = Circuit_ComparisonValue.Value;
      }
    }

    //-------------------------------------------------------------------------
    // If we have a circuit for dynamically updating the range-min value,
    // use it to update now.

    private void UpdateRangeMin()
    {
      if( Circuit_RangeMin != null )
      {
        RangeMin = Circuit_RangeMin.Value;
      }
    }

    //-------------------------------------------------------------------------
    // If we have a circuit for dynamically updating the range-max value,
    // use it to update now.

    private void UpdateRangeMax()
    {
      if( Circuit_RangeMax != null )
      {
        RangeMax = Circuit_RangeMax.Value;
      }
    }

    //-------------------------------------------------------------------------
    // Evaluates conditions and passes processing onto the relevant connected
    // Only ONE output socket can be live at any one time.

    private Processor IdentifyOutputSocketToProcess( Circuit.CircuitContext context )
    {
      //-- Check if any of the conditions are met for an output socket to be live.
      //-- A socket can only be live if it is connected to a wire.
      Processor nextProcessor = null;
      
      // Order is important and range sockets take precendence.
      bool inRangeResult = false;

      if( OutputSocket_InRange.IsConnected ||
          OutputSocket_NotInRange.IsConnected )
      {
        inRangeResult =
          RangeMin <= context.Value &&
          RangeMax >= context.Value;
      }

      // In-range.
      if( inRangeResult &&
          OutputSocket_InRange.IsConnected )
      {
        nextProcessor = OutputSocket_InRange.ConnectedProcessor;
      }
      // Not in-range.
      else if( inRangeResult == false &&
               OutputSocket_NotInRange.IsConnected )
      {
        nextProcessor = OutputSocket_NotInRange.ConnectedProcessor;
      }            
      // Equal.
      else if( context.Value == ComparisonValue &&
               OutputSocket_Equal.IsConnected )
      {
        nextProcessor = OutputSocket_Equal.ConnectedProcessor;
      }
      // Greater.
      else if( context.Value > ComparisonValue &&
               OutputSocket_Greater.IsConnected )
      {
        nextProcessor = OutputSocket_Greater.ConnectedProcessor;
      }
      // Lesser.
      else if( context.Value < ComparisonValue &&
               OutputSocket_Lesser.IsConnected )
      {
        nextProcessor = OutputSocket_Lesser.ConnectedProcessor;
      }
      // Not equal.
      else if( OutputSocket_NotEqual.IsConnected )
      {
        nextProcessor = OutputSocket_NotEqual.ConnectedProcessor;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------
  }
}