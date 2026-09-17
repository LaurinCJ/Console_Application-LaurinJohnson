USE [Video_Games]
GO

/****** Object: Table [dbo].[Franchises] Script Date: 9/17/2026 2:09:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Franchises] (
    [FranchiseID] INT           IDENTITY (1, 1) NOT NULL,
    [Franchise]   NVARCHAR (50) NOT NULL
);


