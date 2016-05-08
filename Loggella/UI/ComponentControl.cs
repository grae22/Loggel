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

      uiValueInput.Visible = ( CircuitComponent is Circuit );
    }

    //-------------------------------------------------------------------------

    private void ComponentControl_Paint( object sender, PaintEventArgs e )
    {
      uiType.Text = CircuitComponent.GetType().Name;

      if( CircuitComponent is Circuit )
      {
        uiName.Text =
          CircuitComponent.Name +
          " = " +
          ( CircuitComponent as Circuit ).Context.Value.ToString();

        BackColor = Color.LightSkyBlue;
      }
      else
      {
        uiName.Text = CircuitComponent.Name;
        BackColor = CircuitComponent.HasProcessed ? Color.Green : Color.White;
      }
    }

    //-------------------------------------------------------------------------

    private void uiValueInput_KeyPress( object sender, KeyPressEventArgs e )
    {
      if( e.KeyChar == 13 )
      {
        ( CircuitComponent as Circuit ).Context.Value =
          ( CircuitComponent as Circuit ).Context.ConvertToCircuitValueType(
            uiValueInput.Text );

        uiValueInput.SelectAll();
      }
    }

    //-------------------------------------------------------------------------
  }
}
