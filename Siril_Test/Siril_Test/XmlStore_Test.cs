using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siril;

namespace Siril_Test
{
  [TestClass]
  public class XmlStore_Test
  {
    //-------------------------------------------------------------------------

    private class Node : SirilObject
    {
      public List<Node> Children { get; set; } = new List<Node>();
      public int ValueInt { get; private set; } = 123;
      public double ValueDouble { get; private set; } = 1.23;

      public Node( string name )
      :
        base( name )
      {
      }

      public override void PerformSnapshot( List<SirilObject> children )
      {
        SetSnapshotMember<int>( "int", ValueInt );
        SetSnapshotMember<double>( "double", ValueDouble );

        foreach( Node child in Children )
        {
          children.Add( child );
        }
      }

      public void AddChild( Node child )
      {
        Children.Add( child );
      }
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void General()
    {
      // Create some objects whose values we want to store as xml.
      Node n1_2_1 = new Node( "Node1_2_1" );
      Node n1_2 = new Node( "Node1_2" );
      n1_2.AddChild( n1_2_1 );
      Node n1_1 = new Node( "Node1_1" );
      Node n1 = new Node( "Node1" );
      n1.AddChild( n1_1 );
      n1.AddChild( n1_2 );

      List<SirilObject> rootObjects = new List<SirilObject>();
      rootObjects.Add( n1 );

      // Write the xml.
      XmlStore xml = new XmlStore();
      bool result = xml.WriteToFile( "XmlStore_Test.General.xml", rootObjects );
      Assert.AreEqual( true, result, "Failed to write the test file." );

      // Read the xml and check it's what we're expecting.
      string content = File.ReadAllText( "XmlStore_Test.General.xml" );
      while( content.Contains( Environment.NewLine ) )
      {
        content = content.Remove( content.IndexOf( Environment.NewLine ), Environment.NewLine.Length );
      }
      while( content.Contains( " " ) )
      {
        content = content.Remove( content.IndexOf( ' ' ), 1 );
      }
      Assert.AreEqual(
        "<Store><DataNode><Name>Node1</Name><MemberCollection><Member>" +
          "<Name>int</Name><Value>123</Value></Member><Member>" +
          "<Name>double</Name><Value>1.23</Value></Member></MemberCollection>" +
          "<DataNodeCollection><DataNode><Name>Node1_1</Name><MemberCollection>" +
          "<Member><Name>int</Name><Value>123</Value></Member><Member>" +
          "<Name>double</Name><Value>1.23</Value></Member></MemberCollection>" +
          "<DataNodeCollection/></DataNode><DataNode><Name>Node1_2</Name>" +
          "<MemberCollection><Member><Name>int</Name><Value>123</Value>" +
          "</Member><Member><Name>double</Name><Value>1.23</Value></Member>" +
          "</MemberCollection><DataNodeCollection><DataNode>" +
          "<Name>Node1_2_1</Name><MemberCollection><Member><Name>int</Name>" +
          "<Value>123</Value></Member><Member><Name>double</Name>" +
          "<Value>1.23</Value></Member></MemberCollection><DataNodeCollection/>" +
          "</DataNode></DataNodeCollection></DataNode></DataNodeCollection>" +
          "</DataNode></Store>",
        content );
    }

    //-------------------------------------------------------------------------
  }
}
