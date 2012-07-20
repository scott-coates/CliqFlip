#region

using System;
using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634709972901148238)]
    public class __634709972901148238_AddVideoTable : Migration
    {
        private const String FK_Videos_Media = "FK_Videos_Media";
        private const String FK_Videos_Images = "FK_Videos_Images";

        public override void Up()
        {
            Database.AddTable(
                "Videos", new[]
                {
                    new Column("MediumId", DbType.Int32, ColumnProperty.PrimaryKey),
                    new Column("VideoUrl", DbType.String, 2048, ColumnProperty.NotNull),
                    new Column("Title", DbType.String, 1024, ColumnProperty.Null),
                    new Column("ImageId", DbType.Int32, ColumnProperty.Null)
                });

            Database.AddForeignKey(FK_Videos_Media, "Videos", "MediumId", "Media", "Id");
            Database.AddForeignKey(FK_Videos_Images, "Videos", "ImageId", "Images", "MediumId");
        }

        public override void Down()
        {
            Database.RemoveForeignKey("Videos", FK_Videos_Images);
            Database.RemoveForeignKey("Videos", FK_Videos_Media);
            Database.RemoveTable("Videos");
        }
    }
}