/*
  * This class determines which (if any) connected processor will become
  * live by comparing the circuit value against a comparison value (which
  * may be dynamic if a circuit is provided for this purpose).
  * 
  * NOTE:
  * x Range checks are performed as follows:
  *   x In-range: Inclusive.
  *   x Not in-range: Exclusive.
*/

using System.Xml;
using System.Collections.Generic;

namespace Loggel.Processors
{
  public class Comparer : Processor
  {
    //-------------------------------------------------------------------------
    // PROPERTIES.

    //-- Input circuits.

    // (OPTIONAL) Circuit context whose value we will compare with the comparison
    // value rather than the value of the circuit to which this processor belongs.
    public CircuitContext ExternalValueSource { private get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // comparison value (if a dynamic comparison value is desired).
    public CircuitContext ComparisonValueSource { private get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // range min value (if a dynamic range min value is desired).
    public CircuitContext RangeMinSource { private get; set; }

    // (OPTIONAL) Circuit that provides a value that will be used to update the
    // range max value (if a dynamic range max value is desired).
    public CircuitContext RangeMaxSource { private get; set; }

    //-- Connected processors.
    public Processor Processor_Equal { get; set; }
    public Processor Processor_NotEqual { get; set; }
    public Processor Processor_Greater { get; set; }
    public Processor Processor_Lesser { get; set; }
    public Processor Processor_InRange { get; set; }
    public Processor Processor_NotInRange { get; set; }

    //-- General.
    public dynamic ComparisonValue { private get; set; }
    public dynamic RangeMin { private get; set; }
    public dynamic RangeMax { private get; set; }

    //-------------------------------------------------------------------------

    public Comparer(
      uint id,
      string name,
      string description,
      CircuitContext circuitContext )
    :
      base( id, name, description, circuitContext )
    {

    }

    //-------------------------------------------------------------------------

    override public Component Process()
    {
      // Must call base class.
      base.Process();

      // Run update methods.
      UpdateComparisonValue();
      UpdateRangeMin();
      UpdateRangeMax();

      // Identify and return the next processor to run (if any).
      return IdentifyOutputSocketToProcess();
    }
    
    //-------------------------------------------------------------------------

    // If we have a circuit for dynamically updating the comparison-value,
    // use it to update now.

    private void UpdateComparisonValue()
    {
      if( ComparisonValueSource != null )
      {
        ComparisonValue = ComparisonValueSource.Value;
      }
    }

    //-------------------------------------------------------------------------

    // If we have a circuit for dynamically updating the range-min value,
    // use it to update now.

    private void UpdateRangeMin()
    {
      if( RangeMinSource != null )
      {
        RangeMin = RangeMinSource.Value;
      }
    }

    //-------------------------------------------------------------------------

    // If we have a circuit for dynamically updating the range-max value,
    // use it to update now.

    private void UpdateRangeMax()
    {
      if( RangeMaxSource != null )
      {
        RangeMax = RangeMaxSource.Value;
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
      CircuitContext context = ( ExternalValueSource ?? Context );

      //-- Check if any of the conditions are met for an output socket to be live.
      //-- A socket can only be live if it is connected to a wire.
      Processor nextProcessor = null;
      
      // Order is important and range sockets take precendence.
      bool inRangeResult = false;

      if( Processor_InRange != null ||
          Processor_NotInRange != null )
      {
        inRangeResult =
          RangeMin <= context.Value &&
          RangeMax >= context.Value;
      }

      // In-range.
      if( inRangeResult &&
          Processor_InRange != null )
      {
        nextProcessor = Processor_InRange;
      }
      // Not in-range.
      else if( inRangeResult == false &&
               Processor_NotInRange != null )
      {
        nextProcessor = Processor_NotInRange;
      }            
      // Equal.
      else if( context.Value == ComparisonValue &&
               Processor_Equal != null )
      {
        nextProcessor = Processor_Equal;
      }
      // Greater.
      else if( context.ValueIsNumeric &&
               context.Value > ComparisonValue &&
               Processor_Greater != null )
      {
        nextProcessor = Processor_Greater;
      }
      // Lesser.
      else if( context.ValueIsNumeric &&
               context.Value < ComparisonValue &&
               Processor_Lesser != null )
      {
        nextProcessor = Processor_Lesser;
      }
      // Not equal.
      else if( Processor_NotEqual != null )
      {
        nextProcessor = Processor_NotEqual;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Compile a map of connected processors.
      Dictionary<string, Processor> processors = new Dictionary<string, Processor>();
      processors.Add( "equal", Processor_Equal );
      processors.Add( "notEqual", Processor_NotEqual );
      processors.Add( "greater", Processor_Greater );
      processors.Add( "lesser", Processor_Lesser );
      processors.Add( "inRange", Processor_InRange );
      processors.Add( "notInRange", Processor_NotInRange );

      // Must call base method.
      parent = base.GetAsXml( parent, processors );

      // Comparer processor xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement comparerElement = ownerDoc.CreateElement( "Comparer" );
      parent.AppendChild( comparerElement );

      // External value source.
      XmlElement externalValueSourceIdElement = ownerDoc.CreateElement( "ExternalValueSourceId" );
      comparerElement.AppendChild( externalValueSourceIdElement );
      if( ExternalValueSource != null )
      {
        externalValueSourceIdElement.InnerText = ExternalValueSource.Id.ToString();
      }

      // Comparison value source.
      XmlElement comparisonValueSourceIdElement = ownerDoc.CreateElement( "ComparisonValueSourceId" );
      comparerElement.AppendChild( comparisonValueSourceIdElement );
      if( ComparisonValueSource != null )
      {
        comparisonValueSourceIdElement.InnerText = ComparisonValueSource.Id.ToString();
      }

      // Range-min source.
      XmlElement rangeMinSourceIdElement = ownerDoc.CreateElement( "RangeMinSourceId" );
      comparerElement.AppendChild( rangeMinSourceIdElement );
      if( RangeMinSource != null )
      {
        rangeMinSourceIdElement.InnerText = RangeMinSource.Id.ToString();
      }

      // Range-max source.
      XmlElement rangeMaxSourceIdElement = ownerDoc.CreateElement( "RangeMaxSourceId" );
      comparerElement.AppendChild( rangeMaxSourceIdElement );
      if( RangeMaxSource != null )
      {
        rangeMaxSourceIdElement.InnerText = RangeMaxSource.Id.ToString();
      }

      // Comparison value.
      XmlElement comparisonValueElement = ownerDoc.CreateElement( "ComparisonValue" );
      comparerElement.AppendChild( comparisonValueElement );
      if( ComparisonValue != null )
      {
        comparisonValueElement.InnerText = ComparisonValue.ToString();
      }

      // Range-min value.
      XmlElement rangeMinValueElement = ownerDoc.CreateElement( "RangeMin" );
      comparerElement.AppendChild( rangeMinValueElement );
      if( RangeMin != null )
      {
        rangeMinValueElement.InnerText = RangeMin.ToString();
      }

      // Range-max value.
      XmlElement rangeMaxValueElement = ownerDoc.CreateElement( "RangeMax" );
      comparerElement.AppendChild( rangeMaxValueElement );
      if( RangeMax != null )
      {
        rangeMaxValueElement.InnerText = RangeMax.ToString();
      }

      return comparerElement;
    }

    //-------------------------------------------------------------------------

    // Restore this instance from xml.

    public override XmlElement RestoreFromXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.RestoreFromXml( parent );

      // Comparer processor.
      XmlElement comparerElement = parent[ "Comparer" ];

      // External value source.
      XmlElement externalValueSourceIdElement = comparerElement[ "ExternalValueSourceId" ];
      if( externalValueSourceIdElement.InnerText.Length > 0 )
      {
        uint id = uint.Parse( externalValueSourceIdElement.InnerText );
        ExternalValueSource = CircuitContext.AllContexts[ id ];
      }

      // Comparison value source.
      XmlElement comparisonValueSourceIdElement = comparerElement[ "ComparisonValueSourceId" ];
      if( comparisonValueSourceIdElement.InnerText.Length > 0 )
      {
        uint id = uint.Parse( comparisonValueSourceIdElement.InnerText );
        ComparisonValueSource = CircuitContext.AllContexts[ id ];
      }

      // Range min value source.
      XmlElement rangeMinValueSourceIdElement = comparerElement[ "RangeMinSourceId" ];
      if( rangeMinValueSourceIdElement.InnerText.Length > 0 )
      {
        uint id = uint.Parse( rangeMinValueSourceIdElement.InnerText );
        RangeMinSource = CircuitContext.AllContexts[ id ];
      }

      // Range max value source.
      XmlElement rangeMaxValueSourceIdElement = comparerElement[ "RangeMaxSourceId" ];
      if( rangeMaxValueSourceIdElement.InnerText.Length > 0 )
      {
        uint id = uint.Parse( rangeMaxValueSourceIdElement.InnerText );
        RangeMaxSource = CircuitContext.AllContexts[ id ];
      }

      // Connected processors.
      Processor_Equal = GetConnectedProcessor( "equal" );
      Processor_NotEqual = GetConnectedProcessor( "notEqual" );
      Processor_Greater = GetConnectedProcessor( "greater" );
      Processor_Lesser = GetConnectedProcessor( "lesser" );
      Processor_InRange = GetConnectedProcessor( "inRange" );
      Processor_NotInRange = GetConnectedProcessor( "notInRange" );

      // Comparison value.
      // TODO: Restoring from xml currently sets all values as 'strings'... fix this.
      XmlElement comparisonValueElement = comparerElement[ "ComparisonValue" ];
      ComparisonValue = Context.ConvertToCircuitValueType( comparisonValueElement.InnerText );

      // Range min value.
      XmlElement rangeMinValueElement = comparerElement[ "RangeMin" ];
      RangeMin = Context.ConvertToCircuitValueType( rangeMinValueElement.InnerText );

      // Range max value.
      XmlElement rangeMaxValueElement = comparerElement[ "RangeMax" ];
      RangeMax = Context.ConvertToCircuitValueType( rangeMaxValueElement.InnerText );

      return comparerElement;
    }

    //-------------------------------------------------------------------------

    public void SetAllProcessors( Processor p )
    {
      Processor_Equal = p;
      Processor_NotEqual = p;
      Processor_Greater = p;
      Processor_Lesser = p;
      Processor_InRange = p;
      Processor_NotInRange = p;
    }

    //-------------------------------------------------------------------------
  }
}