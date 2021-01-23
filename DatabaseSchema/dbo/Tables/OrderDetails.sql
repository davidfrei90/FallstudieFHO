CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailId]   INT        IDENTITY (1, 1) NOT NULL,
    [OrderId]         INT        NOT NULL,
    [ProductId]       INT        NOT NULL,
    [UnitPrice]       MONEY      NOT NULL,
    [QuantityInUnits] INT        NOT NULL,
    [Version]         ROWVERSION NOT NULL,
    CONSTRAINT [PK_OrderDetails_1] PRIMARY KEY CLUSTERED ([OrderDetailId] ASC),
    CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId])
);


GO
CREATE NONCLUSTERED INDEX [FK_OrderDetails_Products]
    ON [dbo].[OrderDetails]([ProductId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_OrderDetails_Orders]
    ON [dbo].[OrderDetails]([OrderId] ASC);

