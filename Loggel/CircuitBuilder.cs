using System.Xml;

namespace Loggel
{
  public class CircuitBuilder
  {
    //-------------------------------------------------------------------------

    public static Circuit BuildFromXmlFile( string absFilename )
    {
      // Load the xml.
      XmlDocument doc = new XmlDocument();
      doc.Load( absFilename );
      XmlElement rootElement = doc.FirstChild as XmlElement;

      Circuit circuit = new Circuit( rootElement );

      return circuit;
    }

    //-------------------------------------------------------------------------

    public static void SaveToXmlFile( string absFilename,
                                      Circuit circuit )
    {
      XmlDocument doc = new XmlDocument();
      XmlElement rootElement = doc.CreateElement( "Root" );
      doc.AppendChild( rootElement );

      foreach( Component component in circuit.Context.Components )
      {
        component.GetAsXml( rootElement );
      }

      doc.Save( absFilename );
    }

    //-------------------------------------------------------------------------
  }
}
