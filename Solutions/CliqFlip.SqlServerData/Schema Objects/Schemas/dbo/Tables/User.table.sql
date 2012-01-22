﻿CREATE TABLE [dbo].[User] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (255) NULL,
    [Email]    NVARCHAR (255) NULL,
    [Password] NVARCHAR (255) NULL,
    [Bio]      NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



