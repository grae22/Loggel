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
      uiName.Text = Story.GetName();
      uiType.Text = Story.GetValueType() == IValue.Type.DECIMAL ? "Value" : "State";
      uiInitialValue.Text = Story.GetValue().ToString();
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
  }
}
