
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/20/2017 12:37:44
-- Generated from EDMX file: C:\Users\Nick\Documents\Visual Studio 2015\NEWAPP\GovHistoryWeb\GovHistoryRepository\GovHistoryModel.edmx
-- --------------------------------------------------

 QUOTED_IDENTIFIER OFF;
GO
USE [GovHistory];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [LastModified] datetime  NOT NULL,
    [Inactive] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [EmailConfirmed] nvarchar(max)  NOT NULL,
    [PasswordHash] nvarchar(max)  NOT NULL,
    [SecurityStamp] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [RoleDescription] nvarchar(max)  NOT NULL,
    [IsSysAdmin] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LNK_User_Role'
CREATE TABLE [dbo].[LNK_User_Role] (
    [UserId] int  NOT NULL,
    [RoleId] int  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Permissions'
CREATE TABLE [dbo].[Permissions] (
    [PermissionId] int IDENTITY(1,1) NOT NULL,
    [PermissionDescription] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Lnk_Role_Permission'
CREATE TABLE [dbo].[Lnk_Role_Permission] (
    [RoleId] int  NOT NULL,
    [PermissionId] int  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'UserLogins'
CREATE TABLE [dbo].[UserLogins] (
    [LoginProvider] nvarchar(max)  NOT NULL,
    [ProviderKey] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'UserClaims'
CREATE TABLE [dbo].[UserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClaimType] nvarchar(max)  NOT NULL,
    [ClaimValue] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [Id] in table 'LNK_User_Role'
ALTER TABLE [dbo].[LNK_User_Role]
ADD CONSTRAINT [PK_LNK_User_Role]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PermissionId] in table 'Permissions'
ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT [PK_Permissions]
    PRIMARY KEY CLUSTERED ([PermissionId] ASC);
GO

-- Creating primary key on [Id] in table 'Lnk_Role_Permission'
ALTER TABLE [dbo].[Lnk_Role_Permission]
ADD CONSTRAINT [PK_Lnk_Role_Permission]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ProviderKey] in table 'UserLogins'
ALTER TABLE [dbo].[UserLogins]
ADD CONSTRAINT [PK_UserLogins]
    PRIMARY KEY CLUSTERED ([ProviderKey] ASC);
GO

-- Creating primary key on [Id] in table 'UserClaims'
ALTER TABLE [dbo].[UserClaims]
ADD CONSTRAINT [PK_UserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'LNK_User_Role'
ALTER TABLE [dbo].[LNK_User_Role]
ADD CONSTRAINT [FK_UsersLNK_Users_Roles]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersLNK_Users_Roles'
CREATE INDEX [IX_FK_UsersLNK_Users_Roles]
ON [dbo].[LNK_User_Role]
    ([UserId]);
GO

-- Creating foreign key on [RoleId] in table 'LNK_User_Role'
ALTER TABLE [dbo].[LNK_User_Role]
ADD CONSTRAINT [FK_RolesLNK_Users_Roles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolesLNK_Users_Roles'
CREATE INDEX [IX_FK_RolesLNK_Users_Roles]
ON [dbo].[LNK_User_Role]
    ([RoleId]);
GO

-- Creating foreign key on [RoleId] in table 'Lnk_Role_Permission'
ALTER TABLE [dbo].[Lnk_Role_Permission]
ADD CONSTRAINT [FK_RolesLnk_Role_Permission]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolesLnk_Role_Permission'
CREATE INDEX [IX_FK_RolesLnk_Role_Permission]
ON [dbo].[Lnk_Role_Permission]
    ([RoleId]);
GO

-- Creating foreign key on [PermissionId] in table 'Lnk_Role_Permission'
ALTER TABLE [dbo].[Lnk_Role_Permission]
ADD CONSTRAINT [FK_PermissionsLnk_Role_Permission]
    FOREIGN KEY ([PermissionId])
    REFERENCES [dbo].[Permissions]
        ([PermissionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionsLnk_Role_Permission'
CREATE INDEX [IX_FK_PermissionsLnk_Role_Permission]
ON [dbo].[Lnk_Role_Permission]
    ([PermissionId]);
GO

-- Creating foreign key on [UserId] in table 'UserLogins'
ALTER TABLE [dbo].[UserLogins]
ADD CONSTRAINT [FK_UsersUserLogins]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersUserLogins'
CREATE INDEX [IX_FK_UsersUserLogins]
ON [dbo].[UserLogins]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserClaims'
ALTER TABLE [dbo].[UserClaims]
ADD CONSTRAINT [FK_UsersUserClaims]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersUserClaims'
CREATE INDEX [IX_FK_UsersUserClaims]
ON [dbo].[UserClaims]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------