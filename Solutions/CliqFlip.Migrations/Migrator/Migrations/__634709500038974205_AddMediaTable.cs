#region

using System;
using System.Data;
using Migrator.Framework;
using ForeignKeyConstraint = Migrator.Framework.ForeignKeyConstraint;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634709500038974205)]
    public class __634709500038974205_AddMediaTable : Migration
    {
        private const String FK_Images_UsersInterests = "FK_Images_UsersInterests";
        private const String FK_Images_Media = "FK_Images_Media";
        private const String FK_Media_UsersInterests = "FK_Media_UsersInterests";
        private const String FK_Users_Images = "FK_Users_Images";

        public override void Up()
        {
            //create media table
            Database.AddTable(
                "Media", new[]
                {
                    new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                    new Column("UserInterestId", DbType.Int32, ColumnProperty.Null),
                    new Column("InterestMediumOrder", DbType.Int32, ColumnProperty.Null),
                    new Column("Description", DbType.String, 1024, ColumnProperty.Null),
                    new Column("TmpImageId", DbType.Int32, ColumnProperty.Null)
                });

            //make the old image pk not be identity

            #region changeIdentityColumn

            const string changeIdentityColumn =
                @"
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION

ALTER TABLE dbo.Images
	DROP CONSTRAINT FK_Images_UsersInterests

ALTER TABLE dbo.UserInterests SET (LOCK_ESCALATION = TABLE)

COMMIT
BEGIN TRANSACTION

CREATE TABLE dbo.Tmp_Images
	(
	Id int NOT NULL,
	OriginalFileName nvarchar(1024) NOT NULL,
	Description nvarchar(1024) NULL,
	ThumbFileName nvarchar(1024) NOT NULL,
	MediumFileName nvarchar(1024) NOT NULL,
	FullFileName nvarchar(1024) NOT NULL,
	UserInterestId int NULL,
	InterestImageOrder int NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.Tmp_Images SET (LOCK_ESCALATION = TABLE)

IF EXISTS(SELECT * FROM dbo.Images)
	 EXEC('INSERT INTO dbo.Tmp_Images (Id, OriginalFileName, Description, ThumbFileName, MediumFileName, FullFileName, UserInterestId, InterestImageOrder)
		SELECT Id, OriginalFileName, Description, ThumbFileName, MediumFileName, FullFileName, UserInterestId, InterestImageOrder FROM dbo.Images WITH (HOLDLOCK TABLOCKX)')

ALTER TABLE dbo.Users
	DROP CONSTRAINT FK_Users_Images

DROP TABLE dbo.Images

EXECUTE sp_rename N'dbo.Tmp_Images', N'Images', 'OBJECT' 

ALTER TABLE dbo.Images ADD CONSTRAINT
	PK__Images__3214EC0720C1E124 PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


ALTER TABLE dbo.Images ADD CONSTRAINT
	FK_Images_UsersInterests FOREIGN KEY
	(
	UserInterestId
	) REFERENCES dbo.UserInterests
	(
	Id
	) ON UPDATE  CASCADE 
	 ON DELETE  CASCADE 
	

COMMIT
BEGIN TRANSACTION

ALTER TABLE dbo.Users ADD CONSTRAINT
	FK_Users_Images FOREIGN KEY
	(
	ProfileImageId
	) REFERENCES dbo.Images
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)

COMMIT
";

            #endregion

            Database.ExecuteNonQuery(changeIdentityColumn);

            //add foreign key from image to meda
            Database.RenameColumn("Images", "Id", "MediumId");

            //Remove user image fk temporarily
            Database.RemoveForeignKey("Users", FK_Users_Images);

            //move image order to media order & move image description to media description

            #region moveImageToMedia

            const string moveImagesToMedia =
                @"
INSERT INTO [Media]
           ([InterestMediumOrder]
           ,[Description]
           ,[UserInterestId]
           ,[TmpImageId])
     SELECT InterestImageOrder, Description, UserInterestId, MediumId
     FROM Images
          
  Update i
  set i.MediumId = m.id 
  from Images i
  inner join Media m
  on m.TmpImageId = i.mediumid

  Update u
  set u.ProfileImageId = m.id
from Users u
inner join Media m
on m.TmpImageId = u.ProfileImageId
";

            #endregion

            Database.ExecuteNonQuery(moveImagesToMedia);
            Database.RemoveColumn("Media", "TmpImageId");

            //now that we've migrated images to media, create fk images -> media
            Database.AddForeignKey(FK_Images_Media, "Images", "MediumId", "Media", "Id", ForeignKeyConstraint.Cascade);

            //delete old foreign key
            Database.RemoveForeignKey("Images", FK_Images_UsersInterests);

            //Put User -> Image fk back
            Database.AddForeignKey(FK_Users_Images, "Users", "ProfileImageId", "Images", "MediumId", ForeignKeyConstraint.NoAction);

            //Remove old columns from Images
            Database.RemoveColumn("Images", "Description");
            Database.RemoveColumn("Images", "InterestImageOrder");
            Database.RemoveColumn("Images", "UserInterestId");

            //add media foreign key to userinterest
            Database.AddForeignKey(FK_Media_UsersInterests, "Media", "UserInterestId", "UserInterests", "Id");
        }

        public override void Down()
        {
            //remove add media foreign key to userinterest
            Database.RemoveForeignKey("Media", FK_Media_UsersInterests);

            //put identity back to on

            #region putIdentityBack

            const string putIdentityBack =
                @"
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION

ALTER TABLE dbo.Images
	DROP CONSTRAINT FK_Images_Media

ALTER TABLE dbo.Media SET (LOCK_ESCALATION = TABLE)

COMMIT
BEGIN TRANSACTION

CREATE TABLE dbo.Tmp_Images
	(
	MediumId int NOT NULL IDENTITY (1, 1),
	OriginalFileName nvarchar(1024) NOT NULL,
	ThumbFileName nvarchar(1024) NOT NULL,
	MediumFileName nvarchar(1024) NOT NULL,
	FullFileName nvarchar(1024) NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.Tmp_Images SET (LOCK_ESCALATION = TABLE)

SET IDENTITY_INSERT dbo.Tmp_Images ON

IF EXISTS(SELECT * FROM dbo.Images)
	 EXEC('INSERT INTO dbo.Tmp_Images (MediumId, OriginalFileName, ThumbFileName, MediumFileName, FullFileName)
		SELECT MediumId, OriginalFileName, ThumbFileName, MediumFileName, FullFileName FROM dbo.Images WITH (HOLDLOCK TABLOCKX)')

SET IDENTITY_INSERT dbo.Tmp_Images OFF

ALTER TABLE dbo.Users
	DROP CONSTRAINT FK_Users_Images

DROP TABLE dbo.Images

EXECUTE sp_rename N'dbo.Tmp_Images', N'Images', 'OBJECT' 

ALTER TABLE dbo.Images ADD CONSTRAINT
	PK__Images__3214EC0720C1E124 PRIMARY KEY CLUSTERED 
	(
	MediumId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


ALTER TABLE dbo.Images ADD CONSTRAINT
	FK_Images_Media FOREIGN KEY
	(
	MediumId
	) REFERENCES dbo.Media
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

COMMIT
BEGIN TRANSACTION

ALTER TABLE dbo.Users ADD CONSTRAINT
	FK_Users_Images FOREIGN KEY
	(
	ProfileImageId
	) REFERENCES dbo.Images
	(
	MediumId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)

COMMIT
";

            #endregion

            Database.ExecuteNonQuery(putIdentityBack);

            //put back old columns on images
            Database.AddColumn("Images", "Description", DbType.String, 1024, ColumnProperty.Null);
            Database.AddColumn("Images", "InterestImageOrder", DbType.Int32, ColumnProperty.Null);
            Database.AddColumn("Images", "UserInterestId", DbType.Int32, ColumnProperty.Null);

            //put image data back

            #region updateImageTable

            const string updateImageTable =
                @"
 Update i
  set i.Description  = m.description,
i.InterestImageOrder = m.InterestMediumOrder,
i.UserInterestId = m.UserInterestId
  from Images i
  inner join Media m
  on m.Id = i.mediumid
";

            #endregion

            Database.ExecuteNonQuery(updateImageTable);

            //Remove User -> Image fk
            Database.RemoveForeignKey("Users", FK_Users_Images);

            //delete fk images -> media
            Database.RemoveForeignKey("Images", FK_Images_Media);

            Database.RemoveTable("Media");

            //old image name
            Database.RenameColumn("Images", "MediumId", "Id");

            //add images -> user interests fk
            Database.AddForeignKey(FK_Images_UsersInterests, "Images", "UserInterestId", "UserInterests", "Id", ForeignKeyConstraint.Cascade);

            //add users -> image fk
            Database.AddForeignKey(FK_Users_Images, "Users", "ProfileImageId", "Images", "Id", ForeignKeyConstraint.NoAction);
        }
    }
}