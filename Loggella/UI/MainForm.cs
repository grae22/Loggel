﻿using System;
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

    private const int c_componentWidth = 150;
    private const int c_componentHeight = 50;

    private List<Circuit> Circuits { get; set; } = new List<Circuit>();
    private Thread Runner { get; set; }
    private bool IsRunning { get; set; } = false;

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
        point.X = 0;

        PlaceComponent( circuit, ref point );

        point.Y += 50;
      }

      Runner = new Thread( new ThreadStart( Run ) );
      Runner.Start();
    }

    //-------------------------------------------------------------------------

    private void Run()
    {
      UpdateCircuitValuesDelegate updateValuesDelegate =
        new UpdateCircuitValuesDelegate( UpdateCircuitValues );

      while( Runner.IsAlive )
      {
        try
        {
          if( IsRunning == false )
          {
            continue;
          }

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
      Runner.Abort();
      Runner.Join();
    }

    //-------------------------------------------------------------------------

    delegate void UpdateCircuitValuesDelegate();

    private void UpdateCircuitValues()
    {
      Invalidate( true );
    }

    //-------------------------------------------------------------------------

    private void PlaceComponent(
      Component component,
      ref Point currentPoint )
    {
      ComponentControl c = new ComponentControl( component );
      c.Location = currentPoint;
      uiComponents.Controls.Add( c );

      if( component is Circuit &&
          ( component as Circuit ).EntryProcessor != null )
      {
        currentPoint.X += c_componentWidth;

        PlaceComponent(
          ( component as Circuit ).EntryProcessor,
          ref currentPoint );

        currentPoint.X -= c_componentWidth;
      }
      else if( component is Processor )
      {
        currentPoint.X += c_componentWidth;

        int connectedProcessorCount = ( component as Processor ).ConnectedProcessors.Count;
        int i = 0;

        foreach( Processor processor in ( component as Processor ).ConnectedProcessors.Values )
        {
          PlaceComponent(
            processor,
            ref currentPoint );

          if( i++ < connectedProcessorCount - 1 )
          {
            currentPoint.Y += c_componentHeight;
          }
        }
      }

      currentPoint.X -= c_componentWidth;
    }

    //-------------------------------------------------------------------------

    private void uiPlayPause_Click( object sender, EventArgs e )
    {
      IsRunning = !IsRunning;

      if( IsRunning )
      {
        uiPlayPause.Text = "Pause";
      }
      else
      {
        uiPlayPause.Text = "Play";
      }
    }

    //-------------------------------------------------------------------------
  }
}