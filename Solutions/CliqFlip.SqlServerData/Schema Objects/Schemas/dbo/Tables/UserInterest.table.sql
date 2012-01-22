CREATE TABLE [dbo].[UserInterest] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [SocialityPoints] INT NULL,
    [InterestId]      INT NULL,
    [UserId]          INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



