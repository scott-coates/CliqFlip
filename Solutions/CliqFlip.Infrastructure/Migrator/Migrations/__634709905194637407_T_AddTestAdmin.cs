using System;
using System.Diagnostics;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634709905194637407)]
	public class __634709905194637407_T_AddTestAdmin : ConditionalMigration
	{
		protected override void ConditionalUp()
		{
			Database.ExecuteNonQuery(
				"INSERT [dbo].[Locations] ([CountryCode], [CountryName], [RegionCode], [RegionName], [MetroCode], [County], [Street], [City], [ZipCode], [AreaCode], [Latitude], [Longitude], [MajorLocationId]) VALUES (N'US', N'United States', N'CA', N'California', NULL, N'Orange County', N'', N'Newport Coast', N'92657', NULL, 33.59875, -117.831306, 114)");

			int locationId = Convert.ToInt32(Database.ExecuteScalar("SELECT SCOPE_IDENTITY()"));

			//username:CliqAdmin
			//password:CliqAdmin
			Database.ExecuteNonQuery(
				"INSERT [dbo].[Users] ([Username], [Email], [Password], [Salt], [Bio], [Headline], [TwitterUsername], [YouTubeUsername], [OriginalFileName], [ThumbFileName], [MediumFileName], [FullFileName], [SiteUrl], [FeedUrl], [CreateDate], [LastActivity], [FacebookUsername], [ProfileImageId], [LocationId], [Role]) VALUES (N'CliqAdmin', N'CliqAdmin@CliqFlip.com', N'YdbZlaM5abxJPm9WTeWGCFxS5+nRxAa9RTImunONRWo=', N'vDipmFC8/RUx3klUyUMmPw==', N'I am a cliqflip admin', N'CliqFlip Admin.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '" +
				DateTime.Now.ToShortDateString() + "' ,'" + DateTime.Now.ToShortDateString() + "', Null, NULL, " + locationId + ", N'Administrator')"
				);
		}

		protected override void ConditionalDown()
		{
			int userId = Convert.ToInt32(Database.ExecuteScalar("SELECT Id FROM USERS where username = 'cliqadmin'"));

			object convoString = Database.ExecuteScalar("SELECT ConversationId FROM Messages where SenderId = " + userId);
			if (convoString != null)
			{
				Database.Delete("Messages", "ConversationId", convoString.ToString());
				Database.Delete("UserConversations", "ConversationId", convoString.ToString());
				Database.Delete("Conversations", "Id", convoString.ToString());
			}

			Database.Delete("Users", "Username", "cliqadmin");
			int locationId = Convert.ToInt32(Database.ExecuteScalar("SELECT LocationId FROM USERS where username = 'cliqadmin'"));
			Database.Delete("Locations", "Id", locationId.ToString());
		}
	}
}