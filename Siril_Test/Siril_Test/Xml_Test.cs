using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siril;

namespace Siril_Test
{
  [TestClass]
  public class Xml_Test
  {
    //-------------------------------------------------------------------------

    [TestMethod]
    public void General()
    {
      DataNode n1 = new DataNode( "Node1" );
      n1.SetMember<int>( "M1", 1 );
      n1.SetMember<double>( "M2", 1.23 );

      DataNode n1_1 = n1.AddDataNode( "Node1_1" );
      n1_1.SetMember<int>( "M1", 1 );
      n1_1.SetMember<double>( "M2", 1.23 );

      DataNode n1_2 = n1.AddDataNode( "Node1_2" );
      n1_2.SetMember<int>( "M1", 1 );
      n1_2.SetMember<double>( "M2", 1.23 );

      DataNode n1_2_1 = n1_2.AddDataNode( "Node1_2_1" );
      n1_2_1.SetMember<int>( "M1", 1 );
      n1_2_1.SetMember<double>( "M2", 1.23 );

      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.AppendChild( xmlDoc.CreateElement( "Root" ) );

      Xml xml = new Xml();
      xml.Generate( xmlDoc.FirstChild, n1 );
      Assert.AreEqual(
        "<Root><DataNode><Name>Node1</Name><MemberCollection><Member>" +
          "<Name>M1</Name><Value>1</Value></Member><Member><Name>M2</Name>" +
          "<Value>1.23</Value></Member></MemberCollection><DataNodeCollection>" + 
          "<DataNode><Name>Node1_1</Name><MemberCollection><Member>" +
          "<Name>M1</Name><Value>1</Value></Member><Member><Name>M2</Name>" +
          "<Value>1.23</Value></Member></MemberCollection><DataNodeCollection />" +
          "</DataNode><DataNode><Name>Node1_2</Name><MemberCollection><Member>" +
          "<Name>M1</Name><Value>1</Value></Member><Member><Name>M2</Name>" +
          "<Value>1.23</Value></Member></MemberCollection><DataNodeCollection>" +
          "<DataNode><Name>Node1_2_1</Name><MemberCollection><Member>" +
          "<Name>M1</Name><Value>1</Value></Member><Member><Name>M2</Name>" +
          "<Value>1.23</Value></Member></MemberCollection><DataNodeCollection />" +
          "</DataNode></DataNodeCollection></DataNode></DataNodeCollection></DataNode></Root>",
        xmlDoc.OuterXml );
    }

    //-------------------------------------------------------------------------
  }
}
