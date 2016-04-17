using System.Xml;

namespace Loggel.Processors
{
  public class Maths : Processor
  {
    //-------------------------------------------------------------------------

    public char Operator { get; set; }
    public dynamic Value2 { get; set; }
    public Socket OutputSocket { get; set; }

    //-------------------------------------------------------------------------

    public Maths(
      uint id,
      string name,
      string description,
      CircuitContext circuitContext )
    :
      base( id, name, description, circuitContext )
    {
      OutputSocket =
        CreateOutputSocket( "Result", "Result of mathematical operation." );
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

      // Return the next processor if we have one.
      Processor nextProcessor = null;

      if( OutputSocket.ConnectedProcessor != null )
      {
        nextProcessor = OutputSocket.ConnectedProcessor;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.GetAsXml( parent );

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