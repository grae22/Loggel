using System.Xml;
using System.Collections.Generic;

namespace Loggel.Nang
{
  internal class NangValueState : NangValue
  {
    //-------------------------------------------------------------------------

    public List<string> StateNames { get; private set; } = new List<string>();

    //-------------------------------------------------------------------------

    public NangValueState()
    {

    }

    //-------------------------------------------------------------------------

    public NangValueState( XmlElement xml )
    :
    base( xml )
    {
      // States.
      foreach(
        XmlElement stateXml in xml[ "StateCollection" ].SelectNodes( "State" ) )
      {
        StateNames.Add( stateXml.InnerText );
      }
    }

    //-------------------------------------------------------------------------

    public override void GetAsXml( out XmlElement xml )
    {
      // Must call base.
      base.GetAsXml( out xml );

      // State collection.
      XmlElement stateCollectionXml = xml.OwnerDocument.CreateElement( "StateCollection" );
      xml.AppendChild( stateCollectionXml );

      foreach( string stateName in StateNames )
      {
        XmlElement stateXml = xml.OwnerDocument.CreateElement( "State" );
        stateCollectionXml.AppendChild( stateXml );
        stateXml.InnerText = stateName;
      }
    }

    //-------------------------------------------------------------------------
  }
}
