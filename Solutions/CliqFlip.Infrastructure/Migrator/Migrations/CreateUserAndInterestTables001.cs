using Migrator.Framework;
using Migrator.Framework.SchemaBuilder;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(20120121161640)]
	public class CreateUserAndInterestTables001 : Migration
	{
		public override void Up()
		{
			#region sql stmt 

			string sql =
				@"    create table [Interest] (
        Id INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       Description NVARCHAR(255) null,
       Slug NVARCHAR(255) null,
       ParentInterestId INT null,
       primary key (Id)
    )

    create table [User] (
        Id INT IDENTITY NOT NULL,
       Username NVARCHAR(255) null,
       Email NVARCHAR(255) null,
       Password NVARCHAR(255) null,
       Bio NVARCHAR(max) null,
       primary key (Id)
    )

    create table [UserInterest] (
        Id INT IDENTITY NOT NULL,
       SocialityPoints INT null,
       InterestId INT null,
       UserId INT null,
       primary key (Id)
    )

    alter table [Interest] 
        add constraint FK798382342BA6EEA7 
        foreign key (ParentInterestId) 
        references [Interest]

    alter table [UserInterest] 
        add constraint FK6CE00CDFB06C4543 
        foreign key (InterestId) 
        references [Interest]

    alter table [UserInterest] 
        add constraint FK6CE00CDFEB96777D 
        foreign key (UserId) 
        references [User]";

			#endregion

			Database.ExecuteNonQuery(sql);
		}

		public override void Down()
		{
			Database.RemoveTable("UserInterest");
			Database.RemoveTable("Interest");
			Database.ExecuteNonQuery("DROP TABLE [USER]"); //user is a reserved keyword and makes this fail
		}
	}
}