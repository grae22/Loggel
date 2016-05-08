using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using Loggel;

namespace Loggella.UI
{
  public partial class MainForm : Form
  {
    //-------------------------------------------------------------------------

    private List<Circuit> Circuits { get; set; } = new List<Circuit>();

    private Thread _runner;

    //-------------------------------------------------------------------------

    public MainForm()
    {
      InitializeComponent();

      List<Circuit> circuits;
      CircuitBuilder.Load(
        "./test-circuit-accumulator/",
        out circuits );
      Circuits = circuits;

      foreach( Circuit circuit in Circuits )
      {
        foreach( Component component in circuit.Context.Components.Values )
        {
          ComponentControl c = new ComponentControl( component );
          uiComponentsLayout.Controls.Add( c );
        }
      }

      _runner = new Thread( new ThreadStart( Run ) );
      _runner.Start();
    }

    //-------------------------------------------------------------------------

    private void Run()
    {
      UpdateCircuitValuesDelegate updateValuesDelegate =
        new UpdateCircuitValuesDelegate( UpdateCircuitValues );

      while( _runner.IsAlive )
      {
        try
        {
          foreach( Circuit circuit in Circuits )
          {
            foreach( Component component in circuit.Context.Components.Values )
            {
              component.HasProcessed = false;
            }

            circuit.Process();
          }

          Invoke( updateValuesDelegate );

          Thread.Sleep( 250 );
        }
        catch
        {
          // Ignore.
        }
      }
    }

    //-------------------------------------------------------------------------

    private void MainForm_FormClosed( object sender, FormClosedEventArgs e )
    {
      _runner.Abort();
      _runner.Join();
    }

    //-------------------------------------------------------------------------

    delegate void UpdateCircuitValuesDelegate();

    private void UpdateCircuitValues()
    {
      uiCircuitValues.Text = "";

      foreach( Circuit circuit in Circuits )
      {
        uiCircuitValues.Text +=
          circuit.Name + ": " + circuit.Context.Value +
          Environment.NewLine;
      }

      //foreach( Control c in uiComponents.Controls )
      //{
      //  c.Invalidate();
      //}
      Invalidate( true );
    }

    //-------------------------------------------------------------------------

    private void MainForm_Paint( object sender, PaintEventArgs e )
    {

    }

    //-------------------------------------------------------------------------
  }
}