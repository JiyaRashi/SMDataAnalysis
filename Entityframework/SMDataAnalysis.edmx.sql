
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/17/2023 20:58:42
-- Generated from EDMX file: C:\Jiya -HP\Dot Net\SMData\SMData\Entityframework\SMDataAnalysis.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SME_DATA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[NSEStock]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NSEStock];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'NSEStocks'
CREATE TABLE [dbo].[NSEStocks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ISIN] varchar(255)  NULL,
    [TckrSymb] varchar(255)  NULL,
    [SctySrs] varchar(255)  NULL,
    [OpenPrice] decimal(18,2)  NULL,
    [HighPrice] decimal(18,2)  NULL,
    [LowPrice] decimal(18,2)  NULL,
    [ClosePrice] decimal(18,2)  NULL,
    [OpnPrice] decimal(18,0)  NULL,
    [LastPrice] decimal(18,2)  NULL,
    [PrviousClosePrice] decimal(18,2)  NULL,
    [TtlTradgVol] int  NULL,
    [TradDt] varchar(255)  NULL,
    [TtlNbOfTxsExctd] varchar(255)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'NSEStocks'
ALTER TABLE [dbo].[NSEStocks]
ADD CONSTRAINT [PK_NSEStocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------