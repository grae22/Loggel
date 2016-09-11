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
      NangStory story =
        new NangStory( "", NangValue.NangValueType.DECIMAL );

      Assert.AreEqual( 0.0, story.Value.GetValue() );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangStory_SimpleStory()
    {
      NangStory refStory =
        new NangStory( "RefStory", NangValue.NangValueType.STATE );
      ((NangValueState)refStory.Value).SetStateNames(
        new string[] { "on", "off" } );
      refStory.Value.SetValue( 1 );
      refStory.BuildCircuit();

      NangStory story =
        new NangStory( "Simple", NangValue.NangValueType.DECIMAL );
      story.ReferenceStory = refStory;
      story.Condition.ComparisonValue = 0;
      story.BuildCircuit();

      Assert.AreEqual( 1, story.StoryCircuit.Context.Value );
    }
  }
}
