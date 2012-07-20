#region

using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634697008349413565)]
    public class __634697008349413565_AddMainCategoryInterests : Migration
    {
        public override void Up()
        {
            string script =
                @"
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Automotive', N'automotive', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Animals', N'animals', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Arts And Humanities', N'arts-and-humanities', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Beauty', N'beauty', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Business', N'business', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Computers', N'computers', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Entertainment', N'entertainment', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Local', N'local', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'News', N'news', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Photo And Video', N'photo-and-video', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Real Estate', N'real-estate', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Recreation', N'recreation', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Reference', N'reference', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Science', N'science', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Finance', N'finance', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Food And Drink', N'food-and-drink', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Games', N'games', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Home And Garden', N'home-and-garden', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Industries', N'industries', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Internet', N'internet', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Lifestyles', N'lifestyles', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Shopping', N'shopping', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Social Networks', N'social-networks', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Society', N'society', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Sports', N'sports', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Telecommunications', N'telecommunications', 1)
                    INSERT [Interests] ([Name], [Slug], [IsMainCategory]) VALUES (N'Travel', N'travel', 1)";

            Database.ExecuteNonQuery(script);
        }

        public override void Down()
        {
            Database.ExecuteNonQuery(@"UPDATE [Interests] SET ParentInterestId = NULL
									WHERE ParentInterestId IN
									(
										SELECT Id FROM [Interests] WHERE [IsMainCategory] = 1
									)");

            Database.ExecuteNonQuery("DELETE FROM [Interests] WHERE [IsMainCategory] = 1");
        }
    }
}