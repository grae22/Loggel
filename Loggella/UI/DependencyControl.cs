using System.Windows.Forms;
using System.Collections.Generic;
using Loggel.Nang;

namespace Loggella.UI
{
  public partial class DependencyControl : UserControl
  {
    //-------------------------------------------------------------------------

    public ICondition Condition { get; private set; }

    private NangCircuit Circuit { get; set; }
    private IStory Story { get; set; }

    //-------------------------------------------------------------------------

    public DependencyControl( 
      ICondition condition,
      NangCircuit circuit,
      IStory story )
    {
      Condition = condition;
      Circuit = circuit;
      Story = story;

      InitializeComponent();

      // Remove handlers before setting control values.
      uiStories.DropDown -= uiStories_DropDown;
      uiCondition.SelectedIndexChanged -= uiCondition_SelectedIndexChanged;

      // Set values.
      uiStories.Text = Condition.GetReferenceStory()?.GetName();

      // Add back handlers.
      uiStories.DropDown += uiStories_DropDown;
      uiCondition.SelectedIndexChanged += uiCondition_SelectedIndexChanged;
    }

    //-------------------------------------------------------------------------

    private void uiStories_DropDown( object sender, System.EventArgs e )
    {
      uiStories.Items.Clear();

      List< IStory > stories;
      Circuit.GetStories( out stories );

      foreach( IStory s in stories )
      {
        if( s != Story )
        {
          uiStories.Items.Add( s.GetName() );
        }
      }
    }

    //-------------------------------------------------------------------------

    private void uiStories_SelectedIndexChanged( object sender, System.EventArgs e )
    {
      ApplyDependencyToStory();
    }

    //-------------------------------------------------------------------------

    private void uiCondition_SelectedIndexChanged( object sender, System.EventArgs e )
    {
      ApplyDependencyToStory();
    }

    //-------------------------------------------------------------------------

    private void ApplyDependencyToStory()
    {
      // Reference story.
      IStory refStory = Circuit.GetStory( uiStories.Text );

      if( refStory == null )
      {
        return;
      }

      // Comparison type.
      ICondition.ComparisonType comparisonType = ICondition.ComparisonType.NOTHING;

      if( uiCondition.Text == "=" )
      {
        comparisonType = ICondition.ComparisonType.EQUAL;
      }
      else if( uiCondition.Text == "!=" )
      {
        comparisonType = ICondition.ComparisonType.NOT_EQUAL;
      }
      else if( uiCondition.Text == ">" )
      {
        comparisonType = ICondition.ComparisonType.GREATER;
      }
      else if( uiCondition.Text == ">=" )
      {
        comparisonType = ICondition.ComparisonType.GREATER_OR_EQUAL;
      }
      else if( uiCondition.Text == "<" )
      {
        comparisonType = ICondition.ComparisonType.LESSER;
      }
      else if( uiCondition.Text == "<=" )
      {
        comparisonType = ICondition.ComparisonType.LESSER_OR_EQUAL;
      }
      else
      {
        return;
      }

      // Comparison value.
      float comparisonValue;

      if( float.TryParse( uiCompisonValue.Text, out comparisonValue ) == false )
      {
        return;
      }

      // Action when true.
      ICondition.ActionType actionWhenTrue = ICondition.ActionType.NONE;

      if( uiAction.Text == "be" )
      {
        actionWhenTrue = ICondition.ActionType.SET;
      }
      else if( uiAction.Text == "increase by" )
      {
        actionWhenTrue = ICondition.ActionType.ADD;
      }
      else if( uiAction.Text == "decrease by" )
      {
        actionWhenTrue = ICondition.ActionType.SUBTRACT;
      }
      else
      {
        return;
      }

      float actionValueWhenTrue;

      if( float.TryParse( uiActionValue.Text, out actionValueWhenTrue ) == false )
      {
        return;
      }

      // Apply.
      Circuit.SetConditionValues(
        Condition,
        Story,
        refStory,
        comparisonType,
        comparisonValue,
        actionWhenTrue,
        actionValueWhenTrue,
        ICondition.ActionType.NONE,
        null );
    }

    //-------------------------------------------------------------------------
  }
}
