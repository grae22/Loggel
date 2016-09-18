using System.Windows.Forms;
using System.Drawing;
using Loggel.Nang;

namespace Loggella.UI
{
  public partial class StoryControl : UserControl
  {
    //-------------------------------------------------------------------------

    public IStory Story { get; private set; }

    private NangCircuit Circuit { get; set; }

    //-------------------------------------------------------------------------

    public StoryControl( NangCircuit circuit, IStory story )
    {
      Circuit = circuit;
      Story = story;

      InitializeComponent();

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
      DependencyControl dep = new DependencyControl( Circuit, Story );
      uiDependencies.Controls.Add( dep );
    }

    //-------------------------------------------------------------------------
  }
}
