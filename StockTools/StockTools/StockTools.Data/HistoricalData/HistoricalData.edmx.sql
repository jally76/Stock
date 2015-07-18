
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/18/2015 16:15:39
-- Generated from EDMX file: U:\Kod\Github\Stock\StockTools\StockTools\StockTools.Data\HistoricalData\HistoricalData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Stock];
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

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Prices'
CREATE TABLE [dbo].[Prices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyId] int  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Close] float  NOT NULL,
    [Volume] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Prices'
ALTER TABLE [dbo].[Prices]
ADD CONSTRAINT [PK_Prices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CompanyId] in table 'Prices'
ALTER TABLE [dbo].[Prices]
ADD CONSTRAINT [FK_PriceCompany]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PriceCompany'
CREATE INDEX [IX_FK_PriceCompany]
ON [dbo].[Prices]
    ([CompanyId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------