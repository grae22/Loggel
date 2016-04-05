﻿using System;

namespace Loggel.Processors
{
  public class Maths<T> : Processor<T>
    where T : IComparable
  {
    //-------------------------------------------------------------------------
    // PROPERTIES.

    public char Operator { get; set; }
    public T Value2 { get; set; }

    //-------------------------------------------------------------------------
    // METHODS.

    public Maths()
    {

    }

    //-------------------------------------------------------------------------

    public override Processor<T> Process( Circuit<T>.CircuitContext context )
    {
      // int.
      if( context.Value is int )
      {
        switch( Operator )
        {
          case '+':
            PerformAddition_int( context );
            break;

          case '-':
            PerformSubtraction_int( context );
            break;

          case 'x':
            PerformMultiplication_int( context );
            break;

          case '/':
            PerformDivision_int( context );
            break;
        }
      }
      // double.
      else if( context.Value is double )
      {
        switch( Operator )
        {
          case '+':
            PerformAddition_double( context );
            break;

          case '-':
            PerformSubtraction_double( context );
            break;

          case 'x':
            PerformMultiplication_double( context );
            break;

          case '/':
            PerformDivision_double( context );
            break;
        }
      }

      return null;
    }

    //-------------------------------------------------------------------------
    // Addition.

    private void PerformAddition_int( Circuit<T>.CircuitContext context )
    {
      context.Value =
        (T)(object)( (int)(object)context.Value + (int)(object)Value2 );
    }

    //-------------------------------------------------------------------------

    private void PerformAddition_double( Circuit<T>.CircuitContext context )
    {
      context.Value =
        (T)(object)( (double)(object)context.Value + (double)(object)Value2 );
    }

    //-------------------------------------------------------------------------
    // Subtraction.

    private void PerformSubtraction_int( Circuit<T>.CircuitContext context )
    {
      context.Value =
        (T)(object)( (int)(object)context.Value - (int)(object)Value2 );
    }

    //-------------------------------------------------------------------------

    private void PerformSubtraction_double( Circuit<T>.CircuitContext context )
    {
      context.Value =
        (T)(object)( (double)(object)context.Value - (double)(object)Value2 );
    }

    //-------------------------------------------------------------------------
    // Multiplication.

    private void PerformMultiplication_int( Circuit<T>.CircuitContext context )
    {
      context.Value =
        (T)(object)( (int)(object)context.Value * (int)(object)Value2 );
    }

    //-------------------------------------------------------------------------

    private void PerformMultiplication_double( Circuit<T>.CircuitContext context )
    {
      context.Value =
        (T)(object)( (double)(object)context.Value * (double)(object)Value2 );
    }

    //-------------------------------------------------------------------------
    // Division.

    private void PerformDivision_int( Circuit<T>.CircuitContext context )
    {
      context.Value =
        (T)(object)( (int)(object)context.Value / (int)(object)Value2 );
    }

    //-------------------------------------------------------------------------

    private void PerformDivision_double( Circuit<T>.CircuitContext context )
    {
      context.Value =
        (T)(object)( (double)(object)context.Value / (double)(object)Value2 );
    }

    //-------------------------------------------------------------------------
  }
}