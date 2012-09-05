#region

using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634824517287256366)]
    public class __634824517287256366_DeleteEventStore : Migration
    {
        public override void Up()
        {
            
        }

        public override void Down()
        {
            if(Database.TableExists("Commits"))
            {
                Database.RemoveTable("Commits");
            }
            if (Database.TableExists("Snapshots"))
            {
                Database.RemoveTable("Snapshots");
            }
        }
    }
}