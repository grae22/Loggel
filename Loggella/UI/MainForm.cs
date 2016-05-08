using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
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

      Point point = new Point( 0, 10 );
      foreach( Circuit circuit in Circuits )
      {
        PlaceComponent( circuit, point );

        //foreach( Component component in circuit.Context.Components.Values )
        //{
        //  ComponentControl c = new ComponentControl( component );
        //  uiComponentsLayout.Controls.Add( c );
        //}
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

      Invalidate( true );
    }

    //-------------------------------------------------------------------------

    private void PlaceComponent(
      Component component,
      Point currentPoint )
    {
      currentPoint.X += 200;

      if( component is Circuit == false )
      {
        ComponentControl c = new ComponentControl( component );
        c.Location = currentPoint;
        uiComponents.Controls.Add( c );
      }
      else
      {
        component = ( component as Circuit ).EntryProcessor;
      }

      if( component is Processor )
      {
        foreach( Processor processor in ( component as Processor ).ConnectedProcessors.Values )
        {
          PlaceComponent(
            processor,
            currentPoint );

          currentPoint.Y += 50;
        }
      }
    }

    //-------------------------------------------------------------------------
  }
}