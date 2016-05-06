using System.Xml;
using System.Collections.Generic;

namespace Loggel.Processors
{
  public class Router : Processor
  {
    //-------------------------------------------------------------------------

    public List<Comparer> Routes { get; private set; } = new List<Comparer>();

    //-------------------------------------------------------------------------

    public Router(
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
      Processor nextProcessor = null;

      foreach( Comparer route in Routes )
      {
        nextProcessor = route.Process();

        if( nextProcessor != null )
        {
          break;
        }
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Compile a map of connected processors.
      Dictionary<string, Processor> processors = new Dictionary<string, Processor>();

      foreach( Comparer route in Routes )
      {
        processors.Add( route.Name, route );
      }

      // Must call base method.
      parent = base.GetAsXml( parent, processors );

      // Router processor xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement routerElement = ownerDoc.CreateElement( "Router" );
      parent.AppendChild( routerElement );

      return routerElement;
    }

    //-------------------------------------------------------------------------

    // Restore this instance from xml.

    public override XmlElement RestoreFromXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.RestoreFromXml( parent );

      // Comparer processor.
      XmlElement routerElement = parent[ "Router" ];

      // Connected processors.
      foreach( string k in GetConnectedProcessorKeys() )
      {
        Routes.Add( (Comparer)GetConnectedProcessor( k ) );
      }

      return routerElement;
    }

    //-------------------------------------------------------------------------
  }
}
