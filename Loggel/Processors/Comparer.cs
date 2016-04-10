﻿using System.Collections.Generic;
using Siril;

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
    // PROPERTIES.

    //-- Input circuits.

    // (OPTIONAL) Circuit context whose value we will compare with the comparison
    // value rather than the value of the circuit to which this processor belongs.
    public Circuit.CircuitContext ExternalCircuitContext { private get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // comparison value (if a dynamic comparison value is desired).
    public Circuit Circuit_ComparisonValue { private get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // range min value (if a dynamic range min value is desired).
    public Circuit Circuit_RangeMin { private get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // range max value (if a dynamic range max value is desired).
    public Circuit Circuit_RangeMax { private get; set; }

    //-- Output sockets.
    public Socket OutputSocket_Equal { get; private set; }
    public Socket OutputSocket_NotEqual { get; private set; }
    public Socket OutputSocket_Greater { get; private set; }
    public Socket OutputSocket_Lesser { get; private set; }
    public Socket OutputSocket_InRange { get; private set; }
    public Socket OutputSocket_NotInRange { get; private set; }

    //-- General.
    public dynamic ComparisonValue { private get; set; }
    public dynamic RangeMin { private get; set; }
    public dynamic RangeMax { private get; set; }

    //-------------------------------------------------------------------------

    public Comparer(
      string name,
      Circuit.CircuitContext circuitContext )
    :
      base( name, circuitContext )
    {
      // Create the output sockets.
      OutputSocket_Equal = GetNewOutputSocket( "Equal", "Circuit and Comparison values are equal." );
      OutputSocket_NotEqual = GetNewOutputSocket( "NotEqual", "Circuit and Comparison values are not equal." );
      OutputSocket_Greater = GetNewOutputSocket( "Greater", "Circuit value is greater than Comparison value." );
      OutputSocket_Lesser = GetNewOutputSocket( "Lesser", "Circuit value is smaller than Comparison value." );
      OutputSocket_InRange = GetNewOutputSocket( "InRange", "Circuit value falls within (inclusive) specified range." );
      OutputSocket_NotInRange = GetNewOutputSocket( "NotInRange", "Circuit value falls outside (exclusive) specified range." );
    }

    //-------------------------------------------------------------------------

    public override Processor Process()
    {
      UpdateComparisonValue();
      UpdateRangeMin();
      UpdateRangeMax();

      return IdentifyOutputSocketToProcess();
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

    private Processor IdentifyOutputSocketToProcess()
    {
      //-- If we have an external circuit context we use that for the value to
      //-- compare against the comparison value. Otherwise we simply use the
      //-- context of the circuit to which this processor belongs.
      Circuit.CircuitContext context = ( ExternalCircuitContext ?? CircuitContext );

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

    public override void PerformSnapshot( List<SirilObject> children )
    {
      base.PerformSnapshot( children );

      children.Add( ComparisonValue );
      children.Add( RangeMin );
      children.Add( RangeMax );
      children.Add( OutputSocket_Equal );
      children.Add( OutputSocket_NotEqual );
      children.Add( OutputSocket_Greater );
      children.Add( OutputSocket_Lesser );
      children.Add( OutputSocket_InRange );
      children.Add( OutputSocket_NotInRange );

      SetSnapshotMember<dynamic>( "comparisonValue", ComparisonValue );
      SetSnapshotMember<dynamic>( "rangeMin", RangeMin );
      SetSnapshotMember<dynamic>( "rangeMax", RangeMax );
    }

    //-------------------------------------------------------------------------
  }
}