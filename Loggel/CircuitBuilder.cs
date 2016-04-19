using System;
using System.Xml;
using System.IO;

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

    public static void Save( string absParentDirectory,
                             Circuit circuit )
    {
      string circuitDir = absParentDirectory + '\\' + circuit.Name + '\\';

      // Create a directory for this circuit if it doesn't already exist.
      if( Directory.Exists( circuitDir ) == false )
      {
        try
        {
          Directory.CreateDirectory( circuitDir );
        }
        catch( Exception ex )
        {
          // TODO: Handle this.
          throw ex;
        }
      }

      // Create a XML doc for this circuit.
      XmlDocument doc = new XmlDocument();
      XmlElement rootElement = doc.CreateElement( "Root" );
      doc.AppendChild( rootElement );
      circuit.GetAsXml( rootElement );
      doc.Save( circuitDir + "Circuit.xml" );

      foreach( Component component in circuit.Context.Components.Values )
      {
        doc = new XmlDocument();
        rootElement = doc.CreateElement( "Root" );
        doc.AppendChild( rootElement );
        component.GetAsXml( rootElement );
        doc.Save( circuitDir + component.Name + ".xml" );
      }
    }

    //-------------------------------------------------------------------------
  }
}
