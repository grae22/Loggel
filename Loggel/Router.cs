using System;
using System.Collections.Generic;

namespace Loggel
{
  public class Router<T> where T : IComparable
  {
    //-------------------------------------------------------------------------
    /*
     * The Router class determines which (if any) output socket will become live
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
    public SocketOut OutSocket_Equal { get; private set; }
    public SocketOut OutSocket_NotEqual { get; private set; }
    public SocketOut OutSocket_Greater { get; private set; }
    public SocketOut OutSocket_Lesser { get; private set; }
    public SocketOut OutSocket_InRange { get; private set; }
    public SocketOut OutSocket_NotInRange { get; private set; }

    //-- General.
    public T ComparisonValue { get; set; }
    public T RangeMin { get; set; }
    public T RangeMax { get; set; }

    //-- Private members.
    private Circuit<T> Circuit { get; set; }
    private List<SocketOut> OutSockets { get; set; }

    //-------------------------------------------------------------------------

    public Router( Circuit<T> circuit )
    {
      Circuit = circuit;

      // Create the output sockets.
      OutSocket_Equal = new SocketOut();
      OutSocket_NotEqual = new SocketOut();
      OutSocket_Greater = new SocketOut();
      OutSocket_Lesser = new SocketOut();
      OutSocket_InRange = new SocketOut();
      OutSocket_NotInRange = new SocketOut();

      OutSockets = new List<SocketOut>();
      OutSockets.Add( OutSocket_Equal );
      OutSockets.Add( OutSocket_NotEqual );
      OutSockets.Add( OutSocket_Greater );
      OutSockets.Add( OutSocket_Lesser );
      OutSockets.Add( OutSocket_InRange );
      OutSockets.Add( OutSocket_NotInRange );
    }

    //-------------------------------------------------------------------------

    public void Update()
    {
      UpdateComparisonValue();
      UpdateRangeMin();
      UpdateRangeMax();
      UpdateOutSockets();
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
    // Update each output socket's 'live' state by comparing the circuit value
    // with the comparison-value.
    // Only ONE output socket can be live at any one time.

    private void UpdateOutSockets()
    {
      //-- Reset all sockets' live states.
      SetOutSocketsLiveState( false );

      //-- Check if any of the conditions are met for an output socket to be live.
      //-- A socket can only be live if it is connected to a wire.
      
      // Order is important and range sockets take precendence.
      bool inRangeResult = false;
      int compareResult = Circuit.Value.CompareTo( ComparisonValue );

      if( OutSocket_InRange.Wire != null ||
          OutSocket_NotInRange.Wire != null )
      {
        // We're in-range if the value is equal to the range min or max OR
        // if the value is between the range min and max.
        int rangeResultMin = Circuit.Value.CompareTo( RangeMin );
        int rangeResultMax = Circuit.Value.CompareTo( RangeMax );

        inRangeResult =
          rangeResultMin == 0 ||
          rangeResultMax == 0 ||
          ( rangeResultMin > 0 && rangeResultMax < 0 );
      }

      // In-range.
      if( inRangeResult &&
          OutSocket_InRange.Wire != null )
      {
        OutSocket_InRange.IsLive = true;
      }
      // Not in-range.
      else if( inRangeResult == false &&
               OutSocket_NotInRange.Wire != null )
      {
        OutSocket_NotInRange.IsLive = true;
      }            
      // Equal.
      else if( compareResult == 0 &&
               OutSocket_Equal.Wire != null )
      {
        OutSocket_Equal.IsLive = true;
      }
      // Greater.
      else if( compareResult > 0 &&
               OutSocket_Greater.Wire != null )
      {
        OutSocket_Greater.IsLive = true;
      }
      // Lesser.
      else if( compareResult < 0 &&
               OutSocket_Lesser.Wire != null )
      {
        OutSocket_Lesser.IsLive = true;
      }
      // Not equal.
      else if( OutSocket_NotEqual.Wire != null )
      {
        OutSocket_NotEqual.IsLive = true;
      }
    }

    //-------------------------------------------------------------------------

    private void SetOutSocketsLiveState( bool state )
    {
      foreach( SocketOut socket in OutSockets )
      {
        socket.IsLive = state;
      }
    }

    //-------------------------------------------------------------------------
  }
}