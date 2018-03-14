
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/14/2018 21:17:22
-- Generated from EDMX file: D:\Projects\LibraryManagementSystem\LibraryManagementSystem\DatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LibraryManagementSystemDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DBReaderDBPublication_DBReader]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DBReaderDBPublication] DROP CONSTRAINT [FK_DBReaderDBPublication_DBReader];
GO
IF OBJECT_ID(N'[dbo].[FK_DBReaderDBPublication_DBPublication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DBReaderDBPublication] DROP CONSTRAINT [FK_DBReaderDBPublication_DBPublication];
GO
IF OBJECT_ID(N'[dbo].[FK_DBAuthorDBPublication_DBAuthor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DBAuthorDBPublication] DROP CONSTRAINT [FK_DBAuthorDBPublication_DBAuthor];
GO
IF OBJECT_ID(N'[dbo].[FK_DBAuthorDBPublication_DBPublication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DBAuthorDBPublication] DROP CONSTRAINT [FK_DBAuthorDBPublication_DBPublication];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DBReaderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DBReaderSet];
GO
IF OBJECT_ID(N'[dbo].[DBPublicationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DBPublicationSet];
GO
IF OBJECT_ID(N'[dbo].[DBAuthorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DBAuthorSet];
GO
IF OBJECT_ID(N'[dbo].[DBReaderDBPublication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DBReaderDBPublication];
GO
IF OBJECT_ID(N'[dbo].[DBAuthorDBPublication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DBAuthorDBPublication];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DBReaderSet'
CREATE TABLE [dbo].[DBReaderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [First] nvarchar(max)  NOT NULL,
    [Last] nvarchar(max)  NOT NULL,
    [Patronimic] nvarchar(max)  NOT NULL,
    [AccessLevel] tinyint  NOT NULL
);
GO

-- Creating table 'DBPublicationSet'
CREATE TABLE [dbo].[DBPublicationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DatePublished] datetime  NOT NULL,
    [Publisher] nvarchar(max)  NOT NULL,
    [Type] tinyint  NOT NULL,
    [PhysicalLocation] nvarchar(max)  NOT NULL,
    [InternetLocation] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DBAuthorSet'
CREATE TABLE [dbo].[DBAuthorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [First] nvarchar(max)  NOT NULL,
    [Last] nvarchar(max)  NOT NULL,
    [Patronimic] nvarchar(max)  NOT NULL,
    [WriterType] tinyint  NOT NULL
);
GO

-- Creating table 'DBReaderDBPublication'
CREATE TABLE [dbo].[DBReaderDBPublication] (
    [Reader_Id] int  NOT NULL,
    [TakenPublications_Id] int  NOT NULL
);
GO

-- Creating table 'DBAuthorDBPublication'
CREATE TABLE [dbo].[DBAuthorDBPublication] (
    [Writer_Id] int  NOT NULL,
    [WrittenPublications_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DBReaderSet'
ALTER TABLE [dbo].[DBReaderSet]
ADD CONSTRAINT [PK_DBReaderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DBPublicationSet'
ALTER TABLE [dbo].[DBPublicationSet]
ADD CONSTRAINT [PK_DBPublicationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DBAuthorSet'
ALTER TABLE [dbo].[DBAuthorSet]
ADD CONSTRAINT [PK_DBAuthorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Reader_Id], [TakenPublications_Id] in table 'DBReaderDBPublication'
ALTER TABLE [dbo].[DBReaderDBPublication]
ADD CONSTRAINT [PK_DBReaderDBPublication]
    PRIMARY KEY CLUSTERED ([Reader_Id], [TakenPublications_Id] ASC);
GO

-- Creating primary key on [Writer_Id], [WrittenPublications_Id] in table 'DBAuthorDBPublication'
ALTER TABLE [dbo].[DBAuthorDBPublication]
ADD CONSTRAINT [PK_DBAuthorDBPublication]
    PRIMARY KEY CLUSTERED ([Writer_Id], [WrittenPublications_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Reader_Id] in table 'DBReaderDBPublication'
ALTER TABLE [dbo].[DBReaderDBPublication]
ADD CONSTRAINT [FK_DBReaderDBPublication_DBReader]
    FOREIGN KEY ([Reader_Id])
    REFERENCES [dbo].[DBReaderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TakenPublications_Id] in table 'DBReaderDBPublication'
ALTER TABLE [dbo].[DBReaderDBPublication]
ADD CONSTRAINT [FK_DBReaderDBPublication_DBPublication]
    FOREIGN KEY ([TakenPublications_Id])
    REFERENCES [dbo].[DBPublicationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DBReaderDBPublication_DBPublication'
CREATE INDEX [IX_FK_DBReaderDBPublication_DBPublication]
ON [dbo].[DBReaderDBPublication]
    ([TakenPublications_Id]);
GO

-- Creating foreign key on [Writer_Id] in table 'DBAuthorDBPublication'
ALTER TABLE [dbo].[DBAuthorDBPublication]
ADD CONSTRAINT [FK_DBAuthorDBPublication_DBAuthor]
    FOREIGN KEY ([Writer_Id])
    REFERENCES [dbo].[DBAuthorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [WrittenPublications_Id] in table 'DBAuthorDBPublication'
ALTER TABLE [dbo].[DBAuthorDBPublication]
ADD CONSTRAINT [FK_DBAuthorDBPublication_DBPublication]
    FOREIGN KEY ([WrittenPublications_Id])
    REFERENCES [dbo].[DBPublicationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DBAuthorDBPublication_DBPublication'
CREATE INDEX [IX_FK_DBAuthorDBPublication_DBPublication]
ON [dbo].[DBAuthorDBPublication]
    ([WrittenPublications_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------