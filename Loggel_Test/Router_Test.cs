using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;

namespace Loggel_Test
{
  [TestClass]
  public class Router_Test
  {
    //-------------------------------------------------------------------------

    [TestMethod]
    public void NumericEqual()
    {
      Circuit<int> signal = new Circuit<int>( 0 );
      Router<int> router = new Router<int>( signal );
      Wire wire = new Wire();

      router.OutSocket_Equal.Wire = wire;
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
    public void NumericNotEqual()
    {
      Circuit<int> signal = new Circuit<int>( 0 );
      Router<int> router = new Router<int>( signal );
      Wire wire = new Wire();

      router.OutSocket_NotEqual.Wire = wire;
      router.ComparisonValue = 1;
      router.Update();

      Assert.AreEqual( router.OutSocket_Equal.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotEqual.IsLive, true );
      Assert.AreEqual( router.OutSocket_Greater.IsLive, false );
      Assert.AreEqual( router.OutSocket_Lesser.IsLive, false );
      Assert.AreEqual( router.OutSocket_InRange.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotInRange.IsLive, false );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NumericGreater()
    {
      Circuit<int> signal = new Circuit<int>( 1 );
      Router<int> router = new Router<int>( signal );
      Wire wire = new Wire();

      router.OutSocket_Greater.Wire = wire;
      router.ComparisonValue = 0;
      router.Update();

      Assert.AreEqual( router.OutSocket_Equal.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotEqual.IsLive, false );
      Assert.AreEqual( router.OutSocket_Greater.IsLive, true );
      Assert.AreEqual( router.OutSocket_Lesser.IsLive, false );
      Assert.AreEqual( router.OutSocket_InRange.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotInRange.IsLive, false );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NumericLesser()
    {
      Circuit<int> signal = new Circuit<int>( 0 );
      Router<int> router = new Router<int>( signal );
      Wire wire = new Wire();

      router.OutSocket_Lesser.Wire = wire;
      router.ComparisonValue = 1;
      router.Update();

      Assert.AreEqual( router.OutSocket_Equal.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotEqual.IsLive, false );
      Assert.AreEqual( router.OutSocket_Greater.IsLive, false );
      Assert.AreEqual( router.OutSocket_Lesser.IsLive, true );
      Assert.AreEqual( router.OutSocket_InRange.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotInRange.IsLive, false );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NumericInRange()
    {
      Circuit<int> signal = new Circuit<int>( 0 );
      Router<int> router = new Router<int>( signal );
      Wire wire = new Wire();

      router.OutSocket_InRange.Wire = wire;
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

    [TestMethod]
    public void NumericNotInRange()
    {
      Circuit<int> signal = new Circuit<int>( 11 );
      Router<int> router = new Router<int>( signal );
      Wire wire = new Wire();

      router.OutSocket_NotInRange.Wire = wire;
      router.ComparisonValue = 0;
      router.RangeMin = 0;
      router.RangeMin = 10;
      router.Update();

      Assert.AreEqual( router.OutSocket_Equal.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotEqual.IsLive, false );
      Assert.AreEqual( router.OutSocket_Greater.IsLive, false );
      Assert.AreEqual( router.OutSocket_Lesser.IsLive, false );
      Assert.AreEqual( router.OutSocket_InRange.IsLive, false );
      Assert.AreEqual( router.OutSocket_NotInRange.IsLive, true );
    }

    //-------------------------------------------------------------------------
  }
}
