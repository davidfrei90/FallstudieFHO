CREATE TABLE [dbo].[Products] (
    [ProductId]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50) NOT NULL,
    [ProductNumber]   NVARCHAR (25) CONSTRAINT [DF_Products_ProductNumber] DEFAULT ((-1)) NOT NULL,
    [Category]        NVARCHAR (50) NOT NULL,
    [QuantityPerUnit] FLOAT (53)    NOT NULL,
    [ListUnitPrice]   MONEY         NOT NULL,
    [UnitsOnStock]    INT           NOT NULL,
    [Version]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

