using System.Data;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634696078341536810)]
    public class __634696078341536810_AddIsMainCategoryColumn : Migration
	{
		public override void Up()
		{
			Database.AddColumn("Interests", "IsMainCategory", DbType.Boolean, 1, ColumnProperty.NotNull, false);
		}

		public override void Down()
		{
            Database.RemoveColumn("Interests", "IsMainCategory");
		}
	}
}