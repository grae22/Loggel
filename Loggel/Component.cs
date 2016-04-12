using System.Xml;

namespace Loggel
{
  interface Component
  {
    //-------------------------------------------------------------------------

    // Component name property.
    string Name { get; set; }

    // Component description property.
    string Description { get; set; }

    // Method for persisting component data as xml.
    void GetAsXml( XmlElement parent );

    //-------------------------------------------------------------------------
  }
}
