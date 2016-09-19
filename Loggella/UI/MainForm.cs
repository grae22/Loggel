using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using Loggel;
using Loggel.Nang;

namespace Loggella.UI
{
  public partial class MainForm : Form
  {
    //-------------------------------------------------------------------------

    private const int c_componentWidth = 150;
    private const int c_componentHeight = 50;

    private Thread Runner { get; set; }
    private bool IsAlive { get; set; } = true;
    private bool IsRunning { get; set; } = false;
    private NangCircuit Circuit { get; set; } = new NangCircuit();

    //-------------------------------------------------------------------------

    public MainForm()
    {
      InitializeComponent();

      Runner = new Thread( new ThreadStart( Run ) );
      Runner.Start();
    }

    //-------------------------------------------------------------------------

    private void Run()
    {
      UpdateCircuitValuesDelegate updateValuesDelegate =
        new UpdateCircuitValuesDelegate( UpdateCircuitValues );

      while( IsAlive )
      {
        try
        {
          if( IsRunning == false )
          {
            continue;
          }

          Circuit.RunCircuit();

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
      IsAlive = false;
      Runner.Join();
    }

    //-------------------------------------------------------------------------

    delegate void UpdateCircuitValuesDelegate();

    private void UpdateCircuitValues()
    {
      Invalidate( true );
    }

    //-------------------------------------------------------------------------

    private void UpdateStoryView()
    {
      List< IStory > stories;
      Circuit.GetStories( out stories );

      foreach( IStory s in stories )
      {
        // Try find a control for this story.
        bool foundStoryControl = false;

        foreach( Control c in uiStories.Controls )
        {
          StoryControl storyControl = (StoryControl)c;

          if( storyControl != null &&
              storyControl.Story == s )
          {
            foundStoryControl = true;
            break;
          }
        }

        // Add a new control if story doesn't have one yet.
        if( foundStoryControl == false )
        {
          StoryControl sc = new StoryControl( Circuit, s );
          sc.RefreshUi();
          sc.StoryChanged += OnStoryChanged;
          uiStories.Controls.Add( sc );
        }
      }
    }

    //-------------------------------------------------------------------------

    private void UpdateDetailedViewUI()
    {
      uiComponents.Controls.Clear();

      Point point = new Point( 0, 10 );

      List< Circuit > circuits;
      Circuit.GetCircuits( out circuits );

      foreach( Circuit circuit in circuits )
      {
        point.X = 0;

        PlaceDetailViewComponent( circuit, ref point );

        point.Y += 50;
      }
    }

    //-------------------------------------------------------------------------

    private void PlaceDetailViewComponent(
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

        PlaceDetailViewComponent(
          ( component as Circuit ).EntryProcessor,
          ref currentPoint );

        currentPoint.X -= c_componentWidth;
      }
      else if( component is Processor )
      {
        currentPoint.X += c_componentWidth;

        List< Processor > processors;
        ( component as Processor ).GetConnectedProcessors( out processors );

        int connectedProcessorCount = processors.Count;
        int i = 0;

        foreach( Processor processor in processors )
        {
          PlaceDetailViewComponent(
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

    private void uiAddStory_Click( object sender, EventArgs e )
    {
      // Make a name (followed by number if name already exists).
      List< IStory > stories;
      Circuit.GetStories( out stories );
      string name = "New Story";
      int i = 0;

      while( Circuit.GetStory( name ) != null )
      {
        i++;
        name = "New Story " + i.ToString();
      }

      // Create the story & update the UI.
      Circuit.CreateStory( name );

      UpdateStoryView();
      UpdateDetailedViewUI();
    }

    //-------------------------------------------------------------------------

    private void OnStoryChanged( object sender, EventArgs args )
    {
      UpdateDetailedViewUI();
    }

    //-------------------------------------------------------------------------
  }
}