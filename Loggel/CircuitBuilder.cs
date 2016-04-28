using System;
using System.Xml;
using System.IO;

namespace Loggel
{
  public class CircuitBuilder
  {
    //-------------------------------------------------------------------------

    public static Circuit Load( string absDirectory )
    {
      // There must be a 'Circuit.xml' file in the directory.
      string absCircuitFilename = absDirectory + @"\Circuit.xml";

      if( File.Exists( absCircuitFilename ) == false )
      {
        return null;
      }

      // Load the circuit's xml.
      XmlDocument doc = new XmlDocument();
      doc.Load( absCircuitFilename );
      XmlElement rootElement = doc.FirstChild as XmlElement;

      Circuit circuit = new Circuit();

      // Load the rest of the circuit's components, each has its own xml file.
      string[] filenames =
        Directory.GetFiles(
          absDirectory,
          "*.xml",
          SearchOption.TopDirectoryOnly );

      foreach( string filename in filenames )
      {
        string name =
          Path.GetFileNameWithoutExtension( filename );

        // Skip the circuit's file.
        if( name.ToLower() == "circuit" )
        {
          continue;
        }
        
        // TODO: Continue here.
      }

      return circuit;
    }

    //-------------------------------------------------------------------------

    // Creates a directory in the parent directory and writes XML docs for
    // specified circuit and its components to this new directory.

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

      // Create XML docs for each component.
      foreach( Component component in circuit.Context.Components.Values )
      {
        string absFilename =
          circuitDir +
          "Component_" + component.GetType().Name + '_' + component.Name + ".xml";

        doc = new XmlDocument();
        rootElement = doc.CreateElement( "Root" );
        doc.AppendChild( rootElement );

        XmlAttribute typeAttrib = doc.CreateAttribute( "type" );
        typeAttrib.Value = component.GetType().FullName;
        rootElement.Attributes.Append( typeAttrib );

        component.GetAsXml( rootElement );
        doc.Save( absFilename );
      }
    }

    //-------------------------------------------------------------------------
  }
}
