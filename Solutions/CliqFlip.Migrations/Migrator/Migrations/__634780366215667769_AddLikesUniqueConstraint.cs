#region

using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634780366215667769)]
    public class __634780366215667769_AddLikesUniqueConstraint : Migration
    {
        public override void Up()
        {
            Database.AddUniqueConstraint("IX_Unique_Like", "Likes", "PostId", "UserId");
        }

        public override void Down()
        {
            Database.RemoveConstraint("Likes", "IX_Unique_Like");
        }
    }
}