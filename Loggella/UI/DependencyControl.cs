using System.Windows.Forms;
using System.Collections.Generic;
using Loggel.Nang;

namespace Loggella.UI
{
  public partial class DependencyControl : UserControl
  {
    //-------------------------------------------------------------------------

    public IStory Story { get; private set; }

    private NangCircuit Circuit { get; set; }

    //-------------------------------------------------------------------------

    public DependencyControl( NangCircuit circuit, IStory story )
    {
      Circuit = circuit;
      Story = story;

      InitializeComponent();
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
  }
}
