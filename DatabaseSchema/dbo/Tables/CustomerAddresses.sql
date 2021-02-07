CREATE TABLE [dbo].[CustomerAddresses] (
    [CustomerId] INT NOT NULL,
    [AddressId]  INT NOT NULL,
    CONSTRAINT [PK_CustomerAddress] PRIMARY KEY CLUSTERED ([CustomerId] ASC, [AddressId] ASC),
    CONSTRAINT [FK_CustomerAddress_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([AddressId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_CustomerAddress_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE ON UPDATE CASCADE
);

