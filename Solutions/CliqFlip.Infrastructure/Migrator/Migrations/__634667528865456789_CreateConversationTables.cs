using System;
using System.Data;
using Migrator.Framework;
using ForeignKeyConstraint = Migrator.Framework.ForeignKeyConstraint;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634667528865456789)]
    public class __634667528865456789_CreateConversationTables : Migration
	{
        public override void Up()
		{
            Database.AddTable("Conversations", new[]
            {
                new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity)
            });

            Database.AddTable("Messages", new[]
            {
                new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Text", DbType.String, MigrationConstants.NVarCharMax, ColumnProperty.NotNull),
                new Column("SendDate", DbType.DateTime, ColumnProperty.NotNull),
                new Column("SenderId", DbType.Int32, ColumnProperty.ForeignKey),
                new Column("ConversationId", DbType.Int32, ColumnProperty.ForeignKey)
            });

            Database.AddTable("Participants", new Column[] {
                new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("HasUnreadMessages", DbType.Boolean, ColumnProperty.NotNull, false),
                new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true),
                new Column("UserId", DbType.Int32, ColumnProperty.ForeignKey),
                new Column("ConversationId", DbType.Int32, ColumnProperty.ForeignKey)
            });

            Database.AddForeignKey("FK_Messages_Users", "Messages", "SenderId", "Users", "Id");
            Database.AddForeignKey("FK_Messages_Conversations", "Messages", "ConversationId", "Conversations", "Id");
            Database.AddForeignKey("FK_Participants_Users", "Participants", "UserId", "Users", "Id");
            Database.AddForeignKey("FK_Participants_Conversations", "Participants", "ConversationId", "Conversations", "Id");
		}

		public override void Down()
		{
            Database.RemoveForeignKey("Participants", "FK_Participants_Conversations");
            Database.RemoveForeignKey("Participants", "FK_Participants_Users");
            Database.RemoveForeignKey("Messages", "FK_Messages_Conversations");
            Database.RemoveForeignKey("Messages", "FK_Messages_Users");
            Database.RemoveTable("Participants");
            Database.RemoveTable("Messages");
            Database.RemoveTable("Conversations");
		}
	}
}