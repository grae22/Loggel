using System.Xml;
using System.Collections.Generic;
using System;

namespace Loggel.Nang
{
  internal class NangValueState : NangValue
  {
    //-------------------------------------------------------------------------

    public List<string> StateNames { get; private set; } = new List<string>();

    private int ActiveStateIndex { get; set; } = 0;

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
      foreach( XmlElement stateXml in xml[ "StateCollection" ].SelectNodes( "State" ) )
      {
        StateNames.Add( stateXml.InnerText );
      }

      // Value.
      ActiveStateIndex = -1;
      string valueString = xml[ "Value" ].InnerText;

      for( int i = 0; i < StateNames.Count; i++ )
      {
        if( StateNames[ i ] == valueString )
        {
          ActiveStateIndex = i;
          break;
        }
      }

      if( ActiveStateIndex < 0 )
      {
        ActiveStateIndex = 0;
      }
    }

    //-------------------------------------------------------------------------

    override public dynamic GetValue()
    {
      return StateNames[ ActiveStateIndex ];
    }

    //-------------------------------------------------------------------------

    override public void SetValue( dynamic value )
    {
      if( value.GetType() == typeof( string ) )
      {
        for( int i = 0; i < StateNames.Count; i++ )
        {
          if( StateNames[ i ] == value )
          {
            ActiveStateIndex = i;
            break;
          }
        }
      }
      else if( value.GetType() == typeof( int ) )
      {
        ActiveStateIndex = value;
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
