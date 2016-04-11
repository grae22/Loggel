using System;
using System.Collections.Generic;
using System.Xml;

namespace Siril
{
  public class XmlStore : SirilStore<XmlNode>
  {
    //-------------------------------------------------------------------------

    public override void WriteToFile(
      string absFilename,
      List<SirilObject> rootObjects )
    {
      XmlDocument xmlDoc = new XmlDocument();
      XmlElement docElement = xmlDoc.CreateElement( "Store" );
      xmlDoc.AppendChild( docElement );
      PerformSnapshot( rootObjects, docElement );
      xmlDoc.Save( absFilename );
    }

    //-------------------------------------------------------------------------

    public override void ReadFromFile(
      string absFilename,
      out List<SirilObject> rootObjects )
    {
      rootObjects = new List<SirilObject>();

      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.Load( absFilename );
    }

    //-------------------------------------------------------------------------

    protected override XmlNode GenerateContent(
      DataNode node,
      XmlNode parent,
      out XmlNode dataNodeElement )
    {
      // Data node.
      XmlDocument doc = parent.OwnerDocument;
      dataNodeElement = doc.CreateElement( "DataNode" );
      parent.AppendChild( dataNodeElement );

      // Data node name.
      XmlAttribute name = doc.CreateAttribute( "name" );
      name.Value = node.Name;
      dataNodeElement.Attributes.Append( name );

      // Member collection.
      XmlElement memberCollection = doc.CreateElement( "MemberCollection" );
      dataNodeElement.AppendChild( memberCollection );

      foreach( string memberName in node.Members.Keys )
      {
        XmlElement member = doc.CreateElement( "Member" );
        memberCollection.AppendChild( member );
        member.InnerText = node.Members[ memberName ];

        XmlAttribute memberNameAttrib = doc.CreateAttribute( "name" );
        memberNameAttrib.Value = memberName;
        member.Attributes.Append( memberNameAttrib );
      }

      // Data node collection.
      XmlElement dataNodeCollection = doc.CreateElement( "DataNodeCollection" );
      dataNodeElement.AppendChild( dataNodeCollection );

      return dataNodeCollection;
    }

    //-------------------------------------------------------------------------
  }
}
