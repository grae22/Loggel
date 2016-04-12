using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siril;

namespace Siril_Test
{
  [TestClass]
  public class SirilObjectFactory_Test
  {
    //-------------------------------------------------------------------------

    private class TestObject : SirilObject
    {
      public string Name { get { return Data.Name; } }

      public TestObject( string name ) : base( name ) {}
      public override void PerformSnapshot( List<SirilObject> children ) {}
      public override void RestoreSnapshot() {}
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void CreateObject()
    {
      SirilObjectFactory.RegisterType( typeof( TestObject ).FullName, typeof( TestObject ) );

      TestObject ob =
        (TestObject)SirilObjectFactory.CreateObject(
          typeof( TestObject ).FullName,
          "Test" );

      Assert.IsNotNull( ob );
      Assert.AreEqual( "Test", ob.Name );
    }

    //-------------------------------------------------------------------------
  }
}
