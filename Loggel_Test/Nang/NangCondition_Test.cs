using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Loggel.Nang;

namespace Loggel_Test
{
  [TestClass]
  public class NangCondition_Test
  {
    //-------------------------------------------------------------------------

    [TestInitialize]
    public void Initialise()
    {

    }

    //-------------------------------------------------------------------------

    [TestMethod]
    public void NangCondition_BuildCircuit()
    {
      bool threwException = false;

      // Should throw exception if no reference story has been specified.
      NangCondition condition = new NangCondition();

      try
      {
        threwException = false;
        condition.BuildCircuit( null );
      }
      catch( Exception )
      {
        threwException = true;
      }

      Assert.IsTrue( threwException );

      // Should throw exception if no comparison value has been specified.
      try
      {
        threwException = false;

        NangStory refStory =
          new NangStory( "", NangValue.NangValueType.DECIMAL );

        condition.ReferenceStory = refStory;
        condition.BuildCircuit( null );
      }
      catch( Exception )
      {
        threwException = true;
      }

      Assert.IsTrue( threwException );
    }

    //-------------------------------------------------------------------------

    // Test condition with no sub-conditions.

    //[TestMethod]
    //public void NangCondition_SimpleTest()
    //{
    //  NangCondition condition = new NangCondition();
    //  condition.ReferenceStory = new NangStory( "", NangValue.NangValueType.DECIMAL );
    //  condition.ComparisonValue = 1.23;
    //}

    //-------------------------------------------------------------------------
  }
}
