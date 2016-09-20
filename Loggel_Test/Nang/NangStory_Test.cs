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
      NangStory story = new NangStory( "", IValue.Type.DECIMAL );

      Assert.AreEqual( 0.0, story.Value.GetValue() );
    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangStory_SimpleStory()
    {
      // Reference story with state 'off'.
      NangStory refStory = new NangStory( "RefStory", IValue.Type.STATE );
      ((NangValueState)refStory.Value).SetStateNames( new string[] { "on", "off" } );
      refStory.Value.SetValue( 1 );
      refStory.BuildCircuit();

      // Test story with initial value 0, value will be incremented
      // by 1.23 when ref story state is 'off'.
      NangStory story = new NangStory( "Simple", IValue.Type.DECIMAL );
      story.CreateCondition(
        refStory,
        NangCondition.ComparisonType.EQUAL,
        "off",
        NangCondition.ActionType.ADD,
        1.23 );
      story.BuildCircuit();

      story.StoryCircuit.Process();

      Assert.AreEqual( 1.23, story.StoryCircuit.Context.Value );
    }

    //-------------------------------------------------------------------------
  }
}
