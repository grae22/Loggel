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
  }
}
