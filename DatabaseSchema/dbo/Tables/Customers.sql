CREATE TABLE [dbo].[Customers] (
    [CustomerId] INT           IDENTITY (1, 1) NOT NULL,
    [Salutation] NVARCHAR (15) NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [FirstName]  NVARCHAR (15) NOT NULL,
    [Version]    ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);

