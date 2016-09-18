/*
 * A router is, essentially, a collection of comparers - the relevent processor
 * from first comparer which has a met condition will be selected to run next.  
*/

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

    override public Component Process()
    {
      Component nextComponent = null;

      // Must call base class.
      base.Process();

      // Run each route's processor until one passes, then
      // we return the next processor which should be run.
      foreach( Comparer route in Routes )
      {
        nextComponent = route.Process();

        if( nextComponent != null )
        {
          break;
        }
      }

      return nextComponent;
    }

    //-------------------------------------------------------------------------

    public override void GetConnectedProcessors( out List< Processor > processors )
    {
      processors = new List< Processor >();

      foreach( Processor p in Routes )
      {
        processors.Add( p );
      }
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
