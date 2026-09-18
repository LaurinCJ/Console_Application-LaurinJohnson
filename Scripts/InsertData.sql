USE [Video_Games];
GO

SET IDENTITY_INSERT [dbo].[Franchises] ON;
GO

INSERT INTO [dbo].[Franchises] ([FranchiseID], [Franchise])
VALUES
    (1, N'Super Mario'),
    (2, N'Pokemon'),
    (3, N'The Legend of Zelda'),
    (4, N'Kirby'),
    (5, N'Metroid'),
    (7, N'Sonic the Hedgehog'),
    (8, N'Mega Man');

SET IDENTITY_INSERT [dbo].[Franchises] OFF;
GO

SET IDENTITY_INSERT [dbo].[Titles] ON;
GO

INSERT INTO [dbo].[Titles] ([GameID], [GameTitle], [FranchiseID], [Console])
VALUES
    (1, N'Super Mario Bros.', 1, N'Nintendo Entertainment System'),
    (2, N'Pokemon Red', 2, N'Game Boy'),
    (3, N'Pokemon Blue', 2, N'Game Boy'),
    (4, N'Pokemon Yellow', 2, N'Game Boy'),
    (5, N'Super Mario Odyssey', 1, N'Nintendo Switch'),
    (6, N'Metroid', 5, N'Nintendo Entertainment System'),
    (7, N'Metroid: Zero Mission', 5, N'Game Boy Advance'),
    (8, N'Kirby''s Dream Land', 4, N'Game Boy'),
    (9, N'Kirby''s Return to Dream Land', 4, N'Nintendo Wii'),
    (10, N'Pokemon Scarlet', 2, N'Nintendo Switch'),
    (11, N'Pokemon Violet', 2, N'Nintendo Switch'),
    (12, N'The Legend of Zelda', 3, N'Nintendo Entertainment System'),
    (13, N'Zelda II: The Adventure of Link', 3, N'Nintendo Entertainment System'),
    (14, N'Metroid: Other M', 5, N'Nintendo Wii'),
    (15, N'The Legend of Zelda: Breath of the Wild', 3, N'Nintendo Switch'),
    (16, N'The Legend of Zelda: Tears of the Kingdom', 3, N'Nintendo Switch'),
    (17, N'Super Mario Bros. Wonder', 1, N'Nintendo Switch'),
    (18, N'Super Mario Sunshine', 1, N'Nintendo Gamecube'),
    (19, N'New Super Mario Bros.', 1, N'Nintendo DS'),
    (20, N'Sonic Rush', 7, N'Nintendo DS'),
    (21, N'Sonic Rush Adventures', 7, N'Nintendo DS'),
    (22, N'Sonic the Hedgehog', 7, N'Sega Genesis'),
    (23, N'Mega Man', 8, N'Nintendo Entertainment System'),
    (24, N'Mega Man 2', 8, N'Nintendo Entertainment System'),
    (25, N'Mega Man 3', 8, N'Nintendo Entertainment System'),
    (26, N'Mega Man 4', 8, N'Nintendo Entertainment System'),
    (27, N'Mega Man 5', 8, N'Nintendo Entertainment System');
GO

SET IDENTITY_INSERT [dbo].[Titles] OFF;
GO