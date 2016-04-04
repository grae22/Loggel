using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel;

namespace Loggel_Test
{
  [TestClass]
  public class Circuit_Test
  {
    //-------------------------------------------------------------------------
    // Test if setting a signal's value works.

    [TestMethod]
    public void ValueSet()
    {
      Circuit<bool> signal = new Circuit<bool>( false );
      signal.Value = true;

      Assert.AreEqual( signal.Value, true );
    }

    //-------------------------------------------------------------------------
  }
}
