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

    // Circuit-context to which this component belongs.
    protected CircuitContext Context { get; private set; }

    //-------------------------------------------------------------------------

    public Component( string name,
                      string description,
                      CircuitContext circuitContext )
    {
      Name = name;
      Description = description;
      Context = circuitContext;
    }

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

    // Restores values from xml.
    // Implementors should return the XmlElement that they want children to
    // use as a parent.

    public virtual XmlElement RestoreFromXml( XmlElement parent )
    {
      XmlElement componentElement = parent[ "Component" ];
      Name = componentElement.Attributes[ "name" ].Value;

      XmlElement descriptionElement = componentElement[ "Description" ];
      Description = descriptionElement.InnerText;

      return componentElement;
    }

    //-------------------------------------------------------------------------
  }
}
