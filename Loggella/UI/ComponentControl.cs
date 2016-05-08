using System.Drawing;
using System.Windows.Forms;
using Loggel;

namespace Loggella.UI
{
  public partial class ComponentControl : UserControl
  {
    //-------------------------------------------------------------------------

    private Component CircuitComponent { get; set; }

    //-------------------------------------------------------------------------

    public ComponentControl( Component component )
    {
      InitializeComponent();

      CircuitComponent = component;
    }

    //-------------------------------------------------------------------------

    private void ComponentControl_Paint( object sender, PaintEventArgs e )
    {
      uiName.Text = CircuitComponent.Name;
      uiType.Text = CircuitComponent.GetType().Name;

      BackColor = CircuitComponent.HasProcessed ? Color.Green : Color.White;
    }

    //-------------------------------------------------------------------------
  }
}
