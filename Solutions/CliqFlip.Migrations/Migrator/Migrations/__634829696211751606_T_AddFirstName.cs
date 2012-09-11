#region

using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634829696211751606)]
    public class __634829696211751606_T_AddFirstName : ConditionalMigration
    {
        protected override void ConditionalUp()
        {
            Database.ExecuteNonQuery("UPDATE Users SET FirstName = Username");
        }

        protected override void ConditionalDown()
        {
            Database.ExecuteNonQuery("UPDATE Users SET FirstName = NULL WHERE FirstName = Username");
        }
    }
}