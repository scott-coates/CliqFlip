using System;
using System.Data;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634710315997405844)]
	public class __634710315997405844_AddWebPageTable : Migration
	{
		private const String FK_WebPages_Media = "FK_WebPages_Media";
		private const String FK_WebPages_Images = "FK_WebPages_Images";

		public override void Up()
		{
			Database.AddTable("WebPages", new[]
			{
				new Column("MediumId", DbType.Int32, ColumnProperty.PrimaryKey),
				new Column("LinkUrl", DbType.String,2048, ColumnProperty.NotNull),
				new Column("Title", DbType.String,1024, ColumnProperty.Null),
				new Column("WebPageDomainName", DbType.String,1024, ColumnProperty.NotNull),
				new Column("ImageId", DbType.Int32, ColumnProperty.Null)
			});

			Database.AddForeignKey(FK_WebPages_Media, "WebPages", "MediumId", "Media", "Id");
			Database.AddForeignKey(FK_WebPages_Images, "WebPages", "ImageId", "Images", "MediumId");
		}

		public override void Down()
		{
			Database.RemoveForeignKey("WebPages", FK_WebPages_Images);
			Database.RemoveForeignKey("WebPages", FK_WebPages_Media);
			Database.RemoveTable("WebPages");
		}
	}
}