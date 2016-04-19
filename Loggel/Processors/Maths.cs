using System.Collections.Generic;
using System.Xml;

namespace Loggel.Processors
{
  public class Maths : Processor
  {
    //-------------------------------------------------------------------------

    public char Operator { get; set; }
    public dynamic Value2 { get; set; }
    public Processor ConnectedProcessor { get; set; }

    //-------------------------------------------------------------------------

    public Maths(
      uint id,
      string name,
      string description,
      CircuitContext circuitContext )
    :
      base( id, name, description, circuitContext )
    {

    }

    //-------------------------------------------------------------------------

    public override Processor Process()
    {
      // Perform operation on circuit value.
      switch( Operator )
      {
        case '=':
          Context.Value = Value2;
          break;

        case '+':
          Context.Value += Value2;
          break;

        case '-':
          Context.Value -= Value2;
          break;

        case 'x':
          Context.Value *= Value2;
          break;

        case '/':
          Context.Value /= Value2;
          break;

        default:
          // TODO
          break;
      }

      // Return the next processor (may be null).
      return ConnectedProcessor;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Compile a map of connected processors.
      Dictionary<string, Processor> processors = new Dictionary<string, Processor>();
      processors.Add( "default", ConnectedProcessor );

      // Must call base method.
      parent = base.GetAsXml( parent, processors );

      // Maths processor xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement mathsElement = ownerDoc.CreateElement( "Maths" );
      parent.AppendChild( mathsElement );

      // Operator.
      XmlElement operatorElement = ownerDoc.CreateElement( "Operator" );
      operatorElement.InnerText = Operator.ToString() ?? "";
      mathsElement.AppendChild( operatorElement );

      // Value2.
      XmlElement value2Element = ownerDoc.CreateElement( "Value2" );
      value2Element.InnerText = Value2.ToString();
      mathsElement.AppendChild( value2Element );

      return mathsElement;
    }

    //-------------------------------------------------------------------------

    // Restore this instance from xml.

    public override XmlElement RestoreFromXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.RestoreFromXml( parent );

      // Maths processor.
      XmlElement mathsElement = parent[ "Maths" ];

      // Connected processor.
      ConnectedProcessor = GetConnectedProcessor( "default" );

      // Operator.
      XmlElement operatorElement = mathsElement[ "Operator" ];
      if( operatorElement.InnerText.Length > 0 )
      {
        Operator = operatorElement.InnerText[ 0 ];
      }

      // Value2.
      XmlElement value2Element = mathsElement[ "Value2" ];
      if( value2Element.InnerText.Length > 0 )
      {
        Value2 = value2Element.InnerText;
      }

      return mathsElement;
    }  
  }
}