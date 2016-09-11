using System;
using System.Xml;

namespace Loggel.Nang
{
  internal abstract class NangValue
  {
    //-------------------------------------------------------------------------

    public static NangValue Create( NangValueType type )
    {
      switch( type )
      {
        case NangValueType.STATE:
          return new NangValueState();

        case NangValueType.DECIMAL:
          return new NangValueDecimal();

        default:
          // TODO
          System.Diagnostics.Debug.Assert( false );
          return null;
      }
    }

    //=========================================================================

    public enum NangValueType
    {
      STATE = 0,
      DECIMAL
    }

    public string Name { get; set; } = "";
    public NangValueType ValueType { get; set; } = NangValueType.STATE;
    public string Unit { get; set; } = "";

    //-------------------------------------------------------------------------

    public NangValue()
    {

    }

    //-------------------------------------------------------------------------

    public NangValue( XmlElement xml )
    {
      Name = xml[ "Name" ].InnerText;
      ValueType = GetValueTypeFromString( xml[ "ValueType" ].InnerText );
      Unit = xml[ "Unit" ].InnerText;
    }

    //-------------------------------------------------------------------------

    public abstract dynamic GetValue();
    public abstract void SetValue( dynamic value );

    //-------------------------------------------------------------------------

    public virtual void GetAsXml( out XmlElement xml )
    {
      XmlDocument doc = new XmlDocument();
      xml = doc.CreateElement( "NangValue" );
      doc.AppendChild( xml );

      XmlElement nameXml = doc.CreateElement( "Name" );
      xml.AppendChild( nameXml );
      nameXml.InnerText = Name;

      XmlElement valueTypeXml = doc.CreateElement( "ValueType" );
      xml.AppendChild( valueTypeXml );
      valueTypeXml.InnerText = GetValueTypeAsString( ValueType );

      XmlElement unitXml = doc.CreateElement( "Unit" );
      xml.AppendChild( unitXml );
      unitXml.InnerText = Unit;

      XmlElement valueXml = doc.CreateElement( "Value" );
      xml.AppendChild( valueXml );
      valueXml.InnerText = GetValue().ToString();
    }

    //-------------------------------------------------------------------------

    public static string GetValueTypeAsString( NangValueType valueType )
    {
      switch( valueType )
      {
        case NangValueType.STATE:
          return "state";

        case NangValueType.DECIMAL:
          return "decimal";

        default:
          System.Diagnostics.Debug.Assert( false );
          return null;
      }
    }

    //-------------------------------------------------------------------------

    public static NangValueType GetValueTypeFromString( string s )
    {
      s = s.ToLower();

      if( s == "state" )
      {
        return NangValueType.STATE;
      }
      else if( s == "decimal" )
      {
        return NangValueType.DECIMAL;
      }

      throw new ArgumentException( "Unknown Nang value type '" + s + "'." );
    }

    //-------------------------------------------------------------------------
  }
}