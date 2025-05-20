CREATE DATABASE QuanLyKho2

-- Table to store product information
CREATE TABLE [dbo].[Products]
(
	[ProductID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ProductName] VARCHAR(50) NULL, 
    [Price] INT NULL, 
    [Quantity] INT NULL, 
    [Unit] INT NULL, 
    [CategoryID] INT NULL
);
GO

-- Table to store category information
CREATE TABLE [dbo].[Categories]
(
	[CategoryID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CategoryItem] VARCHAR(50) NULL,
);
GO

-- Table to track the history of stock updates
CREATE TABLE [dbo].[Histories]
(
	[HistoryID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ProductID] INT NULL, 
    [AddedStocks] INT NULL, 
    [Date] DATETIME NULL,
);
GO

-- Table to store items in the shopping cart
CREATE TABLE [dbo].[Carts]
(
	[CartID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] VARCHAR(50) NULL, 
    [Price] INT NULL, 
    [Quantity] INT NULL,
    [ProductID] INT NULL,
    [UserID] INT NULL
);

GO

-- Table to store transaction information
CREATE TABLE [dbo].[Transactions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Date] DATETIME NULL, 
    [Subtotal] VARCHAR(50) NULL, 
    [Cash] VARCHAR(50) NULL, 
    [DiscountPercent] VARCHAR(50) NULL, 
    [DiscountAmount] VARCHAR(50) NULL, 
    [Total] VARCHAR(50) NULL, 
    [Change] VARCHAR(50) NULL, 
    [TransactionId] VARCHAR(MAX) NULL,
    --[UserID] INT NULL
);
GO

-- Table to store ordered items in a transaction
CREATE TABLE [dbo].[Orders]
(
	[OrderID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [TransactionId] VARCHAR(MAX) NULL, 
    [Name] VARCHAR(50) NULL, 
    [Price] VARCHAR(50) NULL, 
    [Quantity] INT NULL
);
GO

-- Creates a table to store user information
CREATE TABLE [dbo].[Users]
(
	[UserID] INT NOT NULL PRIMARY KEY IDENTITY (1000, 1), 
    [Username] VARCHAR(50) NULL, 
    [Password] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NULL,
);

CREATE TABLE [dbo].[Discounts]
(
    [DiscountID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Description] NVARCHAR(100) NOT NULL,
    [Value] DECIMAL(5, 2) NOT NULL
);
INSERT INTO Discounts (Description, Value)
VALUES 
    (N'No discount', 0),
    (N'10%', 10),
    (N'15%', 15),
    (N'30%', 30),
    (N'50%', 50);
