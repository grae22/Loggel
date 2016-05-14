using System.Xml;

namespace Loggel.Nang
{
  internal class NangValueDecimal : NangValue
  {
    //-------------------------------------------------------------------------

    private float Value { get; set; }

    //-------------------------------------------------------------------------

    public NangValueDecimal()
    {

    }

    //-------------------------------------------------------------------------

    public NangValueDecimal( XmlElement xml )
    :
    base( xml )
    {
      // Value.
      Value = float.Parse( xml[ "Value" ].InnerText );
    }

    //-------------------------------------------------------------------------

    override public dynamic GetValue()
    {
      return Value;
    }

    //-------------------------------------------------------------------------

    override public void SetValue( dynamic value )
    {
      Value = value;
    }

    //-------------------------------------------------------------------------

    public override void GetAsXml( out XmlElement xml )
    {
      // Must call base.
      base.GetAsXml( out xml );
    }

    //-------------------------------------------------------------------------
  }
}
