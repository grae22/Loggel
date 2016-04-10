using Siril;

namespace Loggel
{
  public abstract class BasicCircuitEntity : SirilObject
  {
    //-------------------------------------------------------------------------

    public string Name { get; set; } = "Unnamed";

    private string m_description = "";

    //-------------------------------------------------------------------------

    public string Description
    {
      get
      {
        return m_description;
      }

      set
      {
        m_description = value;

        SetSnapshotMember<string>( "description", m_description );
      }
    }

    //-------------------------------------------------------------------------

    public BasicCircuitEntity( string name )
    :
      base( name )
    {
      Name = name;
    }

    //-------------------------------------------------------------------------
  }
}
