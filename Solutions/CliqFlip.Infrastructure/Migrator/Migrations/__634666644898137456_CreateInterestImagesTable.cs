using System;
using System.Data;
using Migrator.Framework;
using ForeignKeyConstraint = Migrator.Framework.ForeignKeyConstraint;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634666644898137456)]
	public class __634666644898137456_CreateInterestImagesTable : Migration
	{
		private const String FK_Images_UsersInterests = "FK_Images_UsersInterests";
		private const String FK_Images_Users = "FK_Images_Users";

		public override void Up()
		{
			Database.AddTable("Images", new[]
			{
				new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
				new Column("OriginalFileName", DbType.String, 1024, ColumnProperty.NotNull),
				new Column("Description", DbType.String, 1024, ColumnProperty.Null),
				new Column("ThumbFileName", DbType.String, 1024, ColumnProperty.NotNull),
				new Column("MediumFileName", DbType.String, 1024, ColumnProperty.NotNull),
				new Column("FullFileName", DbType.String, 1024, ColumnProperty.NotNull),
				new Column("UserInterestId", DbType.Int32, ColumnProperty.Null),
				new Column("UserId", DbType.Int32, ColumnProperty.Null),
				new Column("InterestImageOrder", DbType.Int32, ColumnProperty.Null)
			});

			Database.AddForeignKey(FK_Images_UsersInterests, "Images", "UserInterestId", "UserInterests", "Id", ForeignKeyConstraint.Cascade);
			Database.AddForeignKey(FK_Images_Users, "Images", "UserId", "Users", "Id", ForeignKeyConstraint.Cascade);
		}

		public override void Down()
		{
			Database.RemoveForeignKey("Images", FK_Images_UsersInterests);
			Database.RemoveForeignKey("Images", FK_Images_Users);
			Database.RemoveTable("Images");
		}
	}
}