DBCC CHECKIDENT ('', RESEED, 0);



CREATE DATABASE WarehouseProDB;
GO
USE WarehouseProDB;
GO

-- Bảng Users (Người dùng)
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL
);

-- Bảng Categories (Danh mục sản phẩm)
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50) NOT NULL
);

-- Bảng Products (Sản phẩm)
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CategoryID INT NOT NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- Bảng Sales (Bán hàng)
CREATE TABLE Sales (
    SaleID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    QuantitySold INT NOT NULL,
    SaleDate DATETIME NOT NULL DEFAULT GETDATE(),
    UserID INT NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Bảng History (Lịch sử giao dịch)
CREATE TABLE History (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    Action NVARCHAR(50) NOT NULL, -- 'AddStock' hoặc 'Sale'
    Quantity INT NOT NULL,
    ActionDate DATETIME NOT NULL DEFAULT GETDATE(),
    UserID INT NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Stored Procedure để kiểm tra tồn kho theo danh mục
CREATE PROCEDURE CheckInventoryByCategory
    @CategoryID INT
AS
BEGIN
    SELECT p.ProductID, p.ProductName, p.Quantity, p.Price, c.CategoryName
    FROM Products p
    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
    WHERE p.CategoryID = @CategoryID;
END;

-- Thêm dữ liệu mẫu
INSERT INTO Users (Username, Password) VALUES ('admin', 'admin123');
INSERT INTO Categories (CategoryName) VALUES ('Electronics'), ('Clothing'), ('Food');
INSERT INTO Products (ProductName, Quantity, Price, CategoryID) 
VALUES 
    ('Laptop', 10, 1299, 1),
    ('T-Shirt', 50, 29, 2),
    ('Chocolate', 100, 3, 3);


DELETE FROM Products
DELETE FROM Sales
DELETE FROM History

INSERT INTO Sales (ProductID, QuantitySold, UserID) VALUES (1, 2, 1);
INSERT INTO History (ProductID, Action, Quantity, UserID) 
VALUES 
    (1, 'AddStock', 10, 1),
    (1, 'Sale', 2, 1);
