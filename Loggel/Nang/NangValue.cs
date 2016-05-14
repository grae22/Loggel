using System;
using System.Xml;

namespace Loggel.Nang
{
  internal abstract class NangValue
  {
    //-------------------------------------------------------------------------

    public enum NangValueType
    {
      STATE = 0,
      VALUE
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
    }

    //-------------------------------------------------------------------------

    public static string GetValueTypeAsString( NangValueType valueType )
    {
      switch( valueType )
      {
        case NangValueType.STATE:
          return "state";

        case NangValueType.VALUE:
          return "value";

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
      else if( s == "value" )
      {
        return NangValueType.VALUE;
      }

      throw new ArgumentException( "Unknown Nang value type '" + s + "'." );
    }

    //-------------------------------------------------------------------------
  }
}