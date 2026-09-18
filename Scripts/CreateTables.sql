USE [Video_Games]
GO

/****** Object: Table [dbo].[Franchises] Script Date: 9/18/2026 1:02:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Franchises] (
    [FranchiseID] INT           IDENTITY (1, 1) NOT NULL,
    [Franchise]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([FranchiseID] ASC)
);


USE [Video_Games] --Unsure if I need two, but I kept it to be safe
GO                --I copied and pasted from my created tables DDLs

/****** Object: Table [dbo].[Titles] Script Date: 9/18/2026 1:10:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Titles] (
    [GameID]      INT           IDENTITY (1, 1) NOT NULL,
    [GameTitle]   NVARCHAR (50) NOT NULL,
    [FranchiseID] INT           NOT NULL,
    [Console]     NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FranchiseID] FOREIGN KEY ([FranchiseID]) REFERENCES [dbo].[Franchises] ([FranchiseID])
);