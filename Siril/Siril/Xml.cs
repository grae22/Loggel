using System;
using System.Xml;

namespace Siril
{
  public class Xml : Document<XmlNode>
  {
    //-------------------------------------------------------------------------

    public override bool WriteToFile(
      string absFilename,
      DataNode rootNode )
    {
      XmlDocument xmlDoc = new XmlDocument();
      XmlElement docElement = xmlDoc.CreateElement( "Document" );
      xmlDoc.AppendChild( docElement );
      Generate( docElement, rootNode );
      xmlDoc.Save( absFilename );
      return true;
    }

    //-------------------------------------------------------------------------

    protected override XmlNode GenerateDataNodeContent(
      DataNode node,
      XmlNode parent,
      out XmlNode dataNodeElement )
    {
      // Data node.
      XmlDocument doc = parent.OwnerDocument;//new XmlDocument();
      dataNodeElement = doc.CreateElement( "DataNode" );
      parent.AppendChild( dataNodeElement );

      // Data node name.
      XmlElement name = doc.CreateElement( "Name" );
      name.InnerText = node.Name;
      dataNodeElement.AppendChild( name );

      // Member collection.
      XmlElement memberCollection = doc.CreateElement( "MemberCollection" );
      dataNodeElement.AppendChild( memberCollection );

      foreach( string memberName in node.Members.Keys )
      {
        XmlElement member = doc.CreateElement( "Member" );
        memberCollection.AppendChild( member );

        XmlElement memberNameElement = doc.CreateElement( "Name" );
        memberNameElement.InnerText = memberName;
        member.AppendChild( memberNameElement );

        XmlElement memberValue = doc.CreateElement( "Value" );
        memberValue.InnerText = node.Members[ memberName ];
        member.AppendChild( memberValue );
      }

      // Data node collection.
      XmlElement dataNodeCollection = doc.CreateElement( "DataNodeCollection" );
      dataNodeElement.AppendChild( dataNodeCollection );

      return dataNodeCollection;
    }

    //-------------------------------------------------------------------------
  }
}
