using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loggel.Nang;

namespace Loggel_Test
{
  [TestClass]
  public class NangStory_Test
  {
    //-------------------------------------------------------------------------

    [TestInitialize]
    public void Initialise()
    {

    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangStory_Instantiate()
    {
      NangStory story = new NangStory( "", NangValue.NangValueType.DECIMAL );

      Assert.AreEqual( 0.0, story.Value.GetValue() );
    }

    //-------------------------------------------------------------------------
  }
}
