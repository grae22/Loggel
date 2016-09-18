using System;
using System.Xml;

namespace Loggel.Nang
{
  public abstract class IValue
  {
    public enum Type
    {
      STATE = 0,
      DECIMAL
    }
  }

  //===========================================================================

  internal abstract class NangValue : IValue
  {
    //-------------------------------------------------------------------------

    public static NangValue Create( Type type )
    {
      switch( type )
      {
        case Type.STATE:
          return new NangValueState();

        case Type.DECIMAL:
          return new NangValueDecimal();

        default:
          // TODO
          System.Diagnostics.Debug.Assert( false );
          return null;
      }
    }

    //=========================================================================

    public string Name { get; set; } = "";
    public Type ValueType { get; set; } = Type.STATE;
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

    public static string GetValueTypeAsString( Type valueType )
    {
      switch( valueType )
      {
        case Type.STATE:
          return "state";

        case Type.DECIMAL:
          return "decimal";

        default:
          System.Diagnostics.Debug.Assert( false );
          return null;
      }
    }

    //-------------------------------------------------------------------------

    public static Type GetValueTypeFromString( string s )
    {
      s = s.ToLower();

      if( s == "state" )
      {
        return Type.STATE;
      }
      else if( s == "decimal" )
      {
        return Type.DECIMAL;
      }

      throw new ArgumentException( "Unknown Nang value type '" + s + "'." );
    }

    //-------------------------------------------------------------------------
  }
}