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
      // Reference story with state 'off'.
      NangStory refStory =
        new NangStory( "RefStory", NangValue.NangValueType.STATE );
      ((NangValueState)refStory.Value).SetStateNames(
        new string[] { "on", "off" } );
      refStory.Value.SetValue( 1 );
      refStory.BuildCircuit();

      // Test story with initial value 0, value will be incremented
      // by 1.23 when ref story state is 'off'.
      NangStory story =
        new NangStory( "Simple", NangValue.NangValueType.DECIMAL );
      story.ReferenceStory = refStory;
      story.Condition.Comparison = NangCondition.ComparisonType.EQUAL;
      story.Condition.ComparisonValue = "off";
      story.Condition.ActionWhenTrue = NangCondition.ActionType.ADD;
      story.Condition.ActionValueWhenTrue = 1.23;
      story.BuildCircuit();

      story.StoryCircuit.Process();

      Assert.AreEqual( 1.23, story.StoryCircuit.Context.Value );
    }
  }
}
