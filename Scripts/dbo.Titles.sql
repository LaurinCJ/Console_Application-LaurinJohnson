USE [Video_Games]
GO

/****** Object: Table [dbo].[Titles] Script Date: 9/17/2026 2:09:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Titles] (
    [GameID]      INT           IDENTITY (1, 1) NOT NULL,
    [GameTitle]   NVARCHAR (50) NOT NULL,
    [FranchiseID] INT           NOT NULL,
    [Console]     NVARCHAR (50) NULL
);


