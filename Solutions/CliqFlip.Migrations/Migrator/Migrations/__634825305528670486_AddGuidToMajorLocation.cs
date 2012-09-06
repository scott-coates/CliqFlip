#region

using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634825305528670486)]
    public class __634825305528670486_AddGuidToMajorLocation : Migration
    {
        public override void Up()
        {
            Database.AddColumn("MajorLocations", "Guid", DbType.Guid, ColumnProperty.Null);
            Database.ExecuteNonQuery("UPDATE MajorLocations SET Guid = newid()");
            var column = Database.GetColumnByName("MajorLocations", "Guid");
            column.ColumnProperty = ColumnProperty.NotNull;
            Database.ChangeColumn("MajorLocations", column);
        }

        public override void Down()
        {
            Database.RemoveColumn("MajorLocations", "Guid");
        }
    }
}