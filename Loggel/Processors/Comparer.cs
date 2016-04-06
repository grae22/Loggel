using System;

namespace Loggel.Processors
{
  public class Comparer<T> : Processor<T>
    where T : IComparable
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
    public Circuit<T> Circuit_ComparisonValue { get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // range min value (if a dynamic range min value is desired).
    public Circuit<T> Circuit_RangeMin { get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // range max value (if a dynamic range max value is desired).
    public Circuit<T> Circuit_RangeMax { get; set; }

    //-- Output sockets.
    public Socket<T> OutputSocket_Equal { get; private set; }
    public Socket<T> OutputSocket_NotEqual { get; private set; }
    public Socket<T> OutputSocket_Greater { get; private set; }
    public Socket<T> OutputSocket_Lesser { get; private set; }
    public Socket<T> OutputSocket_InRange { get; private set; }
    public Socket<T> OutputSocket_NotInRange { get; private set; }

    //-- General.
    public T ComparisonValue { get; set; }
    public T RangeMin { get; set; }
    public T RangeMax { get; set; }

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

    public override Processor<T> Process( Circuit<T>.CircuitContext context )
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

    private Processor<T> IdentifyOutputSocketToProcess( Circuit<T>.CircuitContext context )
    {
      //-- Check if any of the conditions are met for an output socket to be live.
      //-- A socket can only be live if it is connected to a wire.
      Processor<T> nextProcessor = null;
      
      // Order is important and range sockets take precendence.
      bool inRangeResult = false;
      int compareResult = context.Value.CompareTo( ComparisonValue );

      if( OutputSocket_InRange.IsConnected ||
          OutputSocket_NotInRange.IsConnected )
      {
        // We're in-range if the value is equal to the range min or max OR
        // if the value is between the range min and max.
        int rangeResultMin = context.Value.CompareTo( RangeMin );
        int rangeResultMax = context.Value.CompareTo( RangeMax );

        inRangeResult =
          rangeResultMin == 0 ||
          rangeResultMax == 0 ||
          ( rangeResultMin > 0 && rangeResultMax < 0 );
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
      else if( compareResult == 0 &&
               OutputSocket_Equal.IsConnected )
      {
        nextProcessor = OutputSocket_Equal.ConnectedProcessor;
      }
      // Greater.
      else if( compareResult > 0 &&
               OutputSocket_Greater.IsConnected )
      {
        nextProcessor = OutputSocket_Greater.ConnectedProcessor;
      }
      // Lesser.
      else if( compareResult < 0 &&
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