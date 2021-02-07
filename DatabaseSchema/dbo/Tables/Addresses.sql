CREATE TABLE [dbo].[Addresses] (
    [AddressId]    INT           IDENTITY (1, 1) NOT NULL,
    [AddressLine1] NVARCHAR (60) NOT NULL,
    [AddressLine2] NVARCHAR (60) NULL,
    [PostalCode]   NVARCHAR (15) NOT NULL,
    [City]         NVARCHAR (50) NOT NULL,
    [Phone]        NVARCHAR (50) NOT NULL,
    [Email]        NVARCHAR (50) NOT NULL,
    [Version]      ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([AddressId] ASC)
);

