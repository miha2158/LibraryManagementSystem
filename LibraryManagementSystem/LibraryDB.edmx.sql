
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/27/2018 23:02:02
-- Generated from EDMX file: D:\Projects\LibraryManagementSystem\LibraryManagementSystem\LibraryDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LibraryDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DbPublicationDbAuthor_DbPublication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DbPublicationDbAuthor] DROP CONSTRAINT [FK_DbPublicationDbAuthor_DbPublication];
GO
IF OBJECT_ID(N'[dbo].[FK_DbPublicationDbAuthor_DbAuthor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DbPublicationDbAuthor] DROP CONSTRAINT [FK_DbPublicationDbAuthor_DbAuthor];
GO
IF OBJECT_ID(N'[dbo].[FK_DbPublicationDbBookLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DbBookLocationSet] DROP CONSTRAINT [FK_DbPublicationDbBookLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_DbBookLocationDbReader]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DbBookLocationSet] DROP CONSTRAINT [FK_DbBookLocationDbReader];
GO
IF OBJECT_ID(N'[dbo].[FK_DbPublicationDbCourse_DbPublication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DbPublicationDbCourse] DROP CONSTRAINT [FK_DbPublicationDbCourse_DbPublication];
GO
IF OBJECT_ID(N'[dbo].[FK_DbPublicationDbCourse_DbCourse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DbPublicationDbCourse] DROP CONSTRAINT [FK_DbPublicationDbCourse_DbCourse];
GO
IF OBJECT_ID(N'[dbo].[FK_DisciplineDbPublication_Discipline]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DisciplineDbPublication] DROP CONSTRAINT [FK_DisciplineDbPublication_Discipline];
GO
IF OBJECT_ID(N'[dbo].[FK_DisciplineDbPublication_DbPublication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DisciplineDbPublication] DROP CONSTRAINT [FK_DisciplineDbPublication_DbPublication];
GO
IF OBJECT_ID(N'[dbo].[FK_DbPublicationDbStats]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DbStatsSet] DROP CONSTRAINT [FK_DbPublicationDbStats];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DbPublicationSet1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DbPublicationSet1];
GO
IF OBJECT_ID(N'[dbo].[DbAuthorSet1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DbAuthorSet1];
GO
IF OBJECT_ID(N'[dbo].[DbBookLocationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DbBookLocationSet];
GO
IF OBJECT_ID(N'[dbo].[DbReaderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DbReaderSet];
GO
IF OBJECT_ID(N'[dbo].[DbStatsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DbStatsSet];
GO
IF OBJECT_ID(N'[dbo].[DbCourseSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DbCourseSet];
GO
IF OBJECT_ID(N'[dbo].[DbDisciplineSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DbDisciplineSet];
GO
IF OBJECT_ID(N'[dbo].[DbPublicationDbAuthor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DbPublicationDbAuthor];
GO
IF OBJECT_ID(N'[dbo].[DbPublicationDbCourse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DbPublicationDbCourse];
GO
IF OBJECT_ID(N'[dbo].[DisciplineDbPublication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DisciplineDbPublication];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DbPublicationSet1'
CREATE TABLE [dbo].[DbPublicationSet1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(70)  NOT NULL,
    [DatePublished] datetime  NOT NULL,
    [PublicationType] tinyint  NOT NULL,
    [Publisher] nvarchar(25)  NULL,
    [InternetLocation] nvarchar(max)  NULL,
    [BookPublication] tinyint  NOT NULL
);
GO

-- Creating table 'DbAuthorSet1'
CREATE TABLE [dbo].[DbAuthorSet1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [First] nvarchar(15)  NOT NULL,
    [Last] nvarchar(15)  NOT NULL,
    [Patronimic] nvarchar(15)  NOT NULL,
    [WriterType] tinyint  NOT NULL
);
GO

-- Creating table 'DbBookLocationSet'
CREATE TABLE [dbo].[DbBookLocationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Room] int  NOT NULL,
    [Place] nvarchar(70)  NOT NULL,
    [IsTaken] bit  NOT NULL,
    [Publication_Id] int  NOT NULL,
    [Reader_Id] int  NULL
);
GO

-- Creating table 'DbReaderSet'
CREATE TABLE [dbo].[DbReaderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [First] nvarchar(15)  NOT NULL,
    [Last] nvarchar(15)  NOT NULL,
    [Patronimic] nvarchar(15)  NOT NULL,
    [AccessLevel] tinyint  NOT NULL,
    [Group] nvarchar(9)  NOT NULL
);
GO

-- Creating table 'DbStatsSet'
CREATE TABLE [dbo].[DbStatsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTaken] datetime  NOT NULL,
    [Publication_Id] int  NOT NULL
);
GO

-- Creating table 'DbCourseSet'
CREATE TABLE [dbo].[DbCourseSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Course] tinyint  NOT NULL
);
GO

-- Creating table 'DbDisciplineSet'
CREATE TABLE [dbo].[DbDisciplineSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(25)  NOT NULL
);
GO

-- Creating table 'DbPublicationDbAuthor'
CREATE TABLE [dbo].[DbPublicationDbAuthor] (
    [Publications_Id] int  NOT NULL,
    [Authors_Id] int  NOT NULL
);
GO

-- Creating table 'DbPublicationDbCourse'
CREATE TABLE [dbo].[DbPublicationDbCourse] (
    [Publication_Id] int  NOT NULL,
    [Course_Id] int  NOT NULL
);
GO

-- Creating table 'DisciplineDbPublication'
CREATE TABLE [dbo].[DisciplineDbPublication] (
    [Discipline_Id] int  NOT NULL,
    [Publication_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DbPublicationSet1'
ALTER TABLE [dbo].[DbPublicationSet1]
ADD CONSTRAINT [PK_DbPublicationSet1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DbAuthorSet1'
ALTER TABLE [dbo].[DbAuthorSet1]
ADD CONSTRAINT [PK_DbAuthorSet1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DbBookLocationSet'
ALTER TABLE [dbo].[DbBookLocationSet]
ADD CONSTRAINT [PK_DbBookLocationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DbReaderSet'
ALTER TABLE [dbo].[DbReaderSet]
ADD CONSTRAINT [PK_DbReaderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DbStatsSet'
ALTER TABLE [dbo].[DbStatsSet]
ADD CONSTRAINT [PK_DbStatsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DbCourseSet'
ALTER TABLE [dbo].[DbCourseSet]
ADD CONSTRAINT [PK_DbCourseSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DbDisciplineSet'
ALTER TABLE [dbo].[DbDisciplineSet]
ADD CONSTRAINT [PK_DbDisciplineSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Publications_Id], [Authors_Id] in table 'DbPublicationDbAuthor'
ALTER TABLE [dbo].[DbPublicationDbAuthor]
ADD CONSTRAINT [PK_DbPublicationDbAuthor]
    PRIMARY KEY CLUSTERED ([Publications_Id], [Authors_Id] ASC);
GO

-- Creating primary key on [Publication_Id], [Course_Id] in table 'DbPublicationDbCourse'
ALTER TABLE [dbo].[DbPublicationDbCourse]
ADD CONSTRAINT [PK_DbPublicationDbCourse]
    PRIMARY KEY CLUSTERED ([Publication_Id], [Course_Id] ASC);
GO

-- Creating primary key on [Discipline_Id], [Publication_Id] in table 'DisciplineDbPublication'
ALTER TABLE [dbo].[DisciplineDbPublication]
ADD CONSTRAINT [PK_DisciplineDbPublication]
    PRIMARY KEY CLUSTERED ([Discipline_Id], [Publication_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Publications_Id] in table 'DbPublicationDbAuthor'
ALTER TABLE [dbo].[DbPublicationDbAuthor]
ADD CONSTRAINT [FK_DbPublicationDbAuthor_DbPublication]
    FOREIGN KEY ([Publications_Id])
    REFERENCES [dbo].[DbPublicationSet1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Authors_Id] in table 'DbPublicationDbAuthor'
ALTER TABLE [dbo].[DbPublicationDbAuthor]
ADD CONSTRAINT [FK_DbPublicationDbAuthor_DbAuthor]
    FOREIGN KEY ([Authors_Id])
    REFERENCES [dbo].[DbAuthorSet1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DbPublicationDbAuthor_DbAuthor'
CREATE INDEX [IX_FK_DbPublicationDbAuthor_DbAuthor]
ON [dbo].[DbPublicationDbAuthor]
    ([Authors_Id]);
GO

-- Creating foreign key on [Publication_Id] in table 'DbBookLocationSet'
ALTER TABLE [dbo].[DbBookLocationSet]
ADD CONSTRAINT [FK_DbPublicationDbBookLocation]
    FOREIGN KEY ([Publication_Id])
    REFERENCES [dbo].[DbPublicationSet1]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DbPublicationDbBookLocation'
CREATE INDEX [IX_FK_DbPublicationDbBookLocation]
ON [dbo].[DbBookLocationSet]
    ([Publication_Id]);
GO

-- Creating foreign key on [Reader_Id] in table 'DbBookLocationSet'
ALTER TABLE [dbo].[DbBookLocationSet]
ADD CONSTRAINT [FK_DbBookLocationDbReader]
    FOREIGN KEY ([Reader_Id])
    REFERENCES [dbo].[DbReaderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DbBookLocationDbReader'
CREATE INDEX [IX_FK_DbBookLocationDbReader]
ON [dbo].[DbBookLocationSet]
    ([Reader_Id]);
GO

-- Creating foreign key on [Publication_Id] in table 'DbPublicationDbCourse'
ALTER TABLE [dbo].[DbPublicationDbCourse]
ADD CONSTRAINT [FK_DbPublicationDbCourse_DbPublication]
    FOREIGN KEY ([Publication_Id])
    REFERENCES [dbo].[DbPublicationSet1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Course_Id] in table 'DbPublicationDbCourse'
ALTER TABLE [dbo].[DbPublicationDbCourse]
ADD CONSTRAINT [FK_DbPublicationDbCourse_DbCourse]
    FOREIGN KEY ([Course_Id])
    REFERENCES [dbo].[DbCourseSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DbPublicationDbCourse_DbCourse'
CREATE INDEX [IX_FK_DbPublicationDbCourse_DbCourse]
ON [dbo].[DbPublicationDbCourse]
    ([Course_Id]);
GO

-- Creating foreign key on [Discipline_Id] in table 'DisciplineDbPublication'
ALTER TABLE [dbo].[DisciplineDbPublication]
ADD CONSTRAINT [FK_DisciplineDbPublication_Discipline]
    FOREIGN KEY ([Discipline_Id])
    REFERENCES [dbo].[DbDisciplineSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Publication_Id] in table 'DisciplineDbPublication'
ALTER TABLE [dbo].[DisciplineDbPublication]
ADD CONSTRAINT [FK_DisciplineDbPublication_DbPublication]
    FOREIGN KEY ([Publication_Id])
    REFERENCES [dbo].[DbPublicationSet1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DisciplineDbPublication_DbPublication'
CREATE INDEX [IX_FK_DisciplineDbPublication_DbPublication]
ON [dbo].[DisciplineDbPublication]
    ([Publication_Id]);
GO

-- Creating foreign key on [Publication_Id] in table 'DbStatsSet'
ALTER TABLE [dbo].[DbStatsSet]
ADD CONSTRAINT [FK_DbPublicationDbStats]
    FOREIGN KEY ([Publication_Id])
    REFERENCES [dbo].[DbPublicationSet1]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DbPublicationDbStats'
CREATE INDEX [IX_FK_DbPublicationDbStats]
ON [dbo].[DbStatsSet]
    ([Publication_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------