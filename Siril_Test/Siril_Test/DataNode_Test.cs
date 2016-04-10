using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siril;

namespace Siril_Test
{
  [TestClass]
  public class DataNode_Test
  {
    //-------------------------------------------------------------------------

    DataNode m_node = null;

    //-------------------------------------------------------------------------

    [TestInitialize]
    public void Initialise()
    {
      m_node = new DataNode( "Test" );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void CreateDataNode()
    {
      Assert.AreEqual( "Test", m_node.Name );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void AddDataNodeMembers()
    {
      m_node.SetMember< bool >( "bool", true );
      Assert.AreEqual( true, m_node.GetMember<bool>( "bool" ) );

      m_node.SetMember< int >( "int", 123 );
      Assert.AreEqual( 123, m_node.GetMember<int>( "int" ) );

      m_node.SetMember< double >( "double", 123.456 );
      Assert.AreEqual( 123.456, m_node.GetMember<double>( "double" ) );

      m_node.SetMember< string >( "string", "123.456" );
      Assert.AreEqual( "123.456", m_node.GetMember<string>( "string" ) );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void ModifyMemberValue()
    {
      m_node.SetMember<int>( "int", 123 );
      Assert.AreEqual( 123, m_node.GetMember<int>( "int" ) );

      m_node.SetMember<int>( "int", 456 );
      Assert.AreEqual( 456, m_node.GetMember<int>( "int" ) );
    }

    //-------------------------------------------------------------------------
  }
}
