CREATE TABLE [dbo].[Util]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATETIME NULL, 
    [Name] NCHAR(10) NULL, 
    [Item] NCHAR(10) NULL, 
    [Amount] FLOAT NULL, 
    [Kome] BIT NULL, 
    [Mark] BIT NULL, 
    [Alex] BIT NULL, 
    [Hans] BIT NULL, 
    [Munya] BIT NULL
)
