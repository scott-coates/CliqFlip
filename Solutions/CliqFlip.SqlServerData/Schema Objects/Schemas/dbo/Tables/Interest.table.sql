CREATE TABLE [dbo].[Interest] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (255) NULL,
    [Description]      NVARCHAR (255) NULL,
    [Slug]             NVARCHAR (255) NULL,
    [ParentInterestId] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



