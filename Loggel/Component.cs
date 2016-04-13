using System.Xml;

namespace Loggel
{
  public abstract class Component
  {
    //-------------------------------------------------------------------------

    // Component name property.
    public string Name { get; set; }

    // Component description property.
    public string Description { get; set; }

    //-------------------------------------------------------------------------

    // Method for persisting component data as xml.
    // Implementors should return the XmlElement that they want children to
    // use as a parent.

    public virtual XmlElement GetAsXml( XmlElement parent )
    {
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement componentElement = ownerDoc.CreateElement( "Component" );
      parent.AppendChild( componentElement );
      
      XmlAttribute nameAttrib = ownerDoc.CreateAttribute( "name" );
      nameAttrib.Value = Name;
      componentElement.Attributes.Append( nameAttrib );

      XmlElement descriptionElement = ownerDoc.CreateElement( "Description" );
      descriptionElement.InnerText = Description;
      componentElement.AppendChild( descriptionElement );

      return componentElement;
    }

    //-------------------------------------------------------------------------
  }
}
