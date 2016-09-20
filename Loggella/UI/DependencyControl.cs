using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Loggel.Nang;

namespace Loggella.UI
{
  public partial class DependencyControl : UserControl
  {
    //-------------------------------------------------------------------------

    public ICondition Condition { get; private set; }
    public ICondition ParentCondition { get; private set; }

    private NangCircuit Circuit { get; set; }
    private IStory Story { get; set; }
    private DependencyControl SubDependency { get; set; }

    //-------------------------------------------------------------------------

    List< EventHandler > DepedencyChangedHandlers = new List<EventHandler>();

    public event EventHandler DependencyChanged
    {
      add
      {
        DepedencyChangedHandlers.Add( value );
      }

      remove
      {
        DepedencyChangedHandlers.Remove( value );
      }
    }

    private void OnDependencyChanged()
    {
      foreach( EventHandler handler in DepedencyChangedHandlers )
      {
        handler( this, EventArgs.Empty );
      }
    }

    //-------------------------------------------------------------------------

    public DependencyControl( 
      ICondition condition,
      ICondition parentCondition,
      NangCircuit circuit,
      IStory story )
    {
      Condition = condition;
      ParentCondition = parentCondition;
      Circuit = circuit;
      Story = story;

      InitializeComponent();

      uiAddSubDependency.Hide();

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
        uiStories.Items.Add( s.GetName() );
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


    private void uiCompisonValue_TextUpdate( object sender, System.EventArgs e )
    {
      ApplyDependencyToStory();
    }

    //-------------------------------------------------------------------------


    private void uiAction_SelectedIndexChanged( object sender, System.EventArgs e )
    {
      if( uiAction.Text == "depends on" &&
          uiDependencies.Controls.Count == 0 )
      {
        uiAddSubDependency.Show();
        uiAddSubDependency_Click( null, null );
      }

      ApplyDependencyToStory();
    }

    //-------------------------------------------------------------------------


    private void uiActionValue_TextUpdate( object sender, System.EventArgs e )
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
      ICondition.ActionType action = ICondition.ActionType.NONE;

      if( uiAction.Text == "be" )
      {
        action = ICondition.ActionType.SET;
      }
      else if( uiAction.Text == "increase by" )
      {
        action = ICondition.ActionType.ADD;
      }
      else if( uiAction.Text == "decrease by" )
      {
        action = ICondition.ActionType.SUBTRACT;
      }
      else
      {
        //return;
      }

      float actionValue;

      if( float.TryParse( uiActionValue.Text, out actionValue ) == false )
      {
        //return;
      }

      // Apply.
      Circuit.SetStoryConditionValues(
        Condition,
        Story,
        refStory,
        comparisonType,
        comparisonValue,
        action,
        actionValue );

      // Raise event.
      OnDependencyChanged();
    }

    //-------------------------------------------------------------------------

    private void OnSubDependencyChanged( object sender, EventArgs args )
    {
      OnDependencyChanged();
    }

    //-------------------------------------------------------------------------

    private void uiAddSubDependency_Click( object sender, EventArgs e )
    {
      ICondition condition = Circuit.CreateSubCondition( Condition );

      DependencyControl dep = new DependencyControl( condition, Condition, Circuit, Story );
      dep.DependencyChanged += OnSubDependencyChanged;
      uiDependencies.Controls.Add( dep );
    }

    //-------------------------------------------------------------------------
  }
}
