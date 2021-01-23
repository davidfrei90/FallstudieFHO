CREATE TABLE [dbo].[Roles] (
    [RoleId]   INT           IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (50) NOT NULL,
    [Version]  ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

