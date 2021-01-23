CREATE TABLE [dbo].[Orders] (
    [OrderId]     INT        IDENTITY (1, 1) NOT NULL,
    [CustomerId]  INT        NOT NULL,
    [OrderDate]   DATETIME   NULL,
    [ShippedDate] DATETIME   NULL,
    [OrderStatus] INT        NOT NULL,
    [Version]     ROWVERSION NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE ON UPDATE CASCADE
);

