using System;
using System.Collections.Generic;
using Siril;

namespace Loggel
{
  public abstract class BasicCircuitEntity : SirilObject
  {
    //-------------------------------------------------------------------------

    public string Name { get; set; } = "Unnamed";
    public string Description { get; set; } = "";

    //-------------------------------------------------------------------------

    // Constructor for use when creating a new entity.

    public BasicCircuitEntity( string name )
    :
      base( name )
    {
      Name = name;
    }

    //-------------------------------------------------------------------------

    // Constructor to use when restoring a saved entity.

    public BasicCircuitEntity( SirilObject ob )
    :
      base( ob )
    {
      
    }

    //-------------------------------------------------------------------------

    public override void PerformSnapshot( List<SirilObject> children )
    {
      SnapshotMember<string>( "description", Description );
    }

    //-------------------------------------------------------------------------

    public override void RestoreSnapshot()
    {
      Name = RestoreMember<string>( "name", "Unnamed" );
      Description = RestoreMember<string>( "description", "" );
    }

    //-------------------------------------------------------------------------
  }
}
