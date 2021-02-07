CREATE TABLE [dbo].[Users] (
    [UserId]     INT           IDENTITY (1, 1) NOT NULL,
    [Username]   NVARCHAR (50) NOT NULL,
    [Password]   NVARCHAR (50) NULL,
    [CustomerId] INT           NULL,
    [Version]    ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_Users_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE ON UPDATE CASCADE
);

