using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;

namespace Loggel_Test
{
  [TestClass]
  public class Router_Test
  {
    //-------------------------------------------------------------------------

    [TestMethod]
    public void TestMethod1()
    {
      Circuit<bool> signal = new Circuit<bool>( true );
      Router<bool> router = new Router<bool>( signal );

      router.ComparisonValue = true;
      router.Update();

      Assert.AreEqual( router.OutSocket_Equal.IsLive, true );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NumericEquals()
    {
      Circuit<int> signal = new Circuit<int>( 0 );
      Router<int> router = new Router<int>( signal );

      router.ComparisonValue = 0;
      router.Update();

      Assert.AreEqual( router.OutSocket_Equal.IsLive, true );
      Assert.AreEqual( router.OutSocket_NotEqual.IsLive, false );
      Assert.AreEqual( router.OutSocket_Greater.IsLive, false );
      Assert.AreEqual( router.OutSocket_Lesser.IsLive, false );
      Assert.AreEqual( router.OutSocket_InRange.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotInRange.IsLive, false );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NumericInRange()
    {
      Circuit<int> signal = new Circuit<int>( 0 );
      Router<int> router = new Router<int>( signal );

      router.ComparisonValue = 0;
      router.RangeMin = 0;
      router.RangeMin = 10;
      router.Update();

      Assert.AreEqual( router.OutSocket_Equal.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotEqual.IsLive, false );
      Assert.AreEqual( router.OutSocket_Greater.IsLive, false );
      Assert.AreEqual( router.OutSocket_Lesser.IsLive, false );
      Assert.AreEqual( router.OutSocket_InRange.IsLive, true );
      Assert.AreEqual( router.OutSocket_NotInRange.IsLive, false );
    }

    //-------------------------------------------------------------------------
  }
}
