using System.Xml;

namespace Loggel
{
  public abstract class Component
  {
    //-------------------------------------------------------------------------

    // Component's unique ID.
    public uint Id { get; private set; }

    // Component name property.
    public string Name { get; set; }

    // Component description property.
    public string Description { get; set; }

    // Circuit-context to which this component belongs.
    protected CircuitContext Context { get; private set; }

    //-------------------------------------------------------------------------

    public Component( uint id,
                      string name,
                      string description,
                      CircuitContext circuitContext )
    {
      Id = id;
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
      
      XmlElement nameElement = ownerDoc.CreateElement( "Name" );
      nameElement.InnerText = Name;
      componentElement.AppendChild( nameElement );

      XmlElement descriptionElement = ownerDoc.CreateElement( "Description" );
      descriptionElement.InnerText = Description;
      componentElement.AppendChild( descriptionElement );

      return componentElement;
    }

    //-------------------------------------------------------------------------

    // Restores values from xml.
    // Implementors should return the element any child classes should use
    // as the parent element.

    public virtual XmlElement RestoreFromXml( XmlElement parent )
    {
      XmlElement componentElement = parent[ "Component" ];

      XmlElement nameElement = componentElement[ "Name" ];
      Name = nameElement.InnerText;

      XmlElement descriptionElement = componentElement[ "Description" ];
      Description = descriptionElement.InnerText;

      return componentElement;
    }

    //-------------------------------------------------------------------------
  }
}
