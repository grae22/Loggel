using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using Loggel.Nang;

namespace Loggella.UI
{
  public partial class StoryControl : UserControl
  {
    //-------------------------------------------------------------------------

    public IStory Story { get; private set; }

    private NangCircuit Circuit { get; set; }

    //-------------------------------------------------------------------------

    List< EventHandler > StoryChangedHandlers = new List< EventHandler >();

    public event EventHandler StoryChanged
    {
      add
      {
        StoryChangedHandlers.Add( value );
      }

      remove
      {
        StoryChangedHandlers.Remove( value );
      }
    }

    private void OnStoryChanged()
    {
      foreach( EventHandler handler in StoryChangedHandlers )
      {
        handler( this, EventArgs.Empty );
      }
    }

    //-------------------------------------------------------------------------

    public StoryControl( NangCircuit circuit, IStory story )
    {
      Circuit = circuit;
      Story = story;

      InitializeComponent();

      List< ICondition > conditions;
      story.GetConditions( out conditions );
      foreach( ICondition c in conditions )
      {
        DependencyControl dep = new DependencyControl( c, null, Circuit, Story );
        dep.DependencyChanged += OnDependencyChanged;
        uiDependencies.Controls.Add( dep );
      }

      RefreshUi();
    }

    //-------------------------------------------------------------------------

    public void RefreshUi()
    {
      // Remove event handlers.
      uiName.TextChanged -= uiName_TextChanged;
      uiType.SelectedIndexChanged -= uiType_SelectedIndexChanged;

      // Apply values.
      uiName.Text = Story.GetName();
      uiType.Text = Story.GetValueType() == IValue.Type.DECIMAL ? "Value" : "State";
      uiInitialValue.Text = Story.GetValue().ToString();

      // Add back event handlers.
      uiName.TextChanged += uiName_TextChanged;
      uiType.SelectedIndexChanged += uiType_SelectedIndexChanged;
    }

    //-------------------------------------------------------------------------

    private void uiName_TextChanged( object sender, System.EventArgs e )
    {
      if( Story.GetName() == uiName.Text )
      {
        return;
      }

      if( Circuit.RenameStory( Story, uiName.Text ) )
      {
        uiName.ForeColor = Color.Black;
      }
      else
      {
        uiName.ForeColor = Color.Red;
      }
    }

    //-------------------------------------------------------------------------

    private void uiType_SelectedIndexChanged( object sender, System.EventArgs e )
    {
      if( uiType.Text == "Value" )
      {
        Circuit.ChangeStoryValueType( Story, IValue.Type.DECIMAL, 0 );
      }
      else
      {
        Circuit.ChangeStoryValueType( Story, IValue.Type.STATE, "" );
      }
    }

    //-------------------------------------------------------------------------

    private void uiAddDependency_Click( object sender, System.EventArgs e )
    {
      ICondition condition = Circuit.CreateStoryCondition( Story );

      DependencyControl dep = new DependencyControl( condition, null, Circuit, Story );
      dep.DependencyChanged += OnDependencyChanged;
      uiDependencies.Controls.Add( dep );
    }

    //-------------------------------------------------------------------------

    private void OnDependencyChanged( object sender, EventArgs args )
    {
      OnStoryChanged();
    }

    //-------------------------------------------------------------------------
  }
}
