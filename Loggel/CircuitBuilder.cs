using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace Loggel
{
  public class CircuitBuilder
  {
    //-------------------------------------------------------------------------

    public static void Load(
      string absDirectory,
      out List<Circuit> circuits )
    {
      Dictionary<Component, XmlElement> componentXml = new Dictionary<Component, XmlElement>();

      circuits = new List<Circuit>();

      CircuitContext.AllContexts.Clear();

      RecursiveLoad(
        absDirectory,
        circuits,
        componentXml );

      foreach( KeyValuePair<Component, XmlElement> currentComponentXml in componentXml )
      {
        currentComponentXml.Key.RestoreFromXml( currentComponentXml.Value );
      }
    }

    //-------------------------------------------------------------------------

    private static void RecursiveLoad(
      string absDirectory,
      List<Circuit> circuits,
      Dictionary<Component, XmlElement> componentXml )
    {
      // Is there a 'Circuit.xml' file in this directory? Load the circuit.
      string absCircuitFilename = absDirectory + @"\Circuit.xml";

      if( File.Exists( absCircuitFilename ) )
      {
        // Load the circuit's xml.
        XmlDocument doc = new XmlDocument();
        doc.Load( absCircuitFilename );
        XmlElement rootElement = doc.FirstChild as XmlElement;

        uint componentId = uint.Parse( rootElement.Attributes[ "id" ].Value );

        Circuit circuit = ComponentFactory.CreateCircuit( componentId );

        circuits.Add( circuit );    // TODO: Remove?

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
        
          // Load the doc and get the component type.
          doc = new XmlDocument();
          doc.Load( filename );
          rootElement = doc.FirstChild as XmlElement;
          string typeName = rootElement.Attributes[ "type" ].Value;
          componentId = uint.Parse( rootElement.Attributes[ "id" ].Value );

          // Instantiate the component.
          Component component =
            circuit.Context.CreateComponent(
              typeName,
              componentId );

          componentXml.Add( component, rootElement );
        }
      }

      // Check for sub-directories with circuits.
      string[] subDirectories = Directory.GetDirectories( absDirectory );

      foreach( string subDir in subDirectories )
      {
        RecursiveLoad(
          subDir,
          circuits,
          componentXml );
      }
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

      XmlAttribute idAttrib = doc.CreateAttribute( "id" );
      idAttrib.Value = circuit.Id.ToString();
      rootElement.Attributes.Append( idAttrib );

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

        idAttrib = doc.CreateAttribute( "id" );
        idAttrib.Value = component.Id.ToString();
        rootElement.Attributes.Append( idAttrib );

        component.GetAsXml( rootElement );
        doc.Save( absFilename );
      }
    }

    //-------------------------------------------------------------------------
  }
}
