
-- create database ecommercedb;

-- CREATE TABLE dbo.Product (
--     ProductId INT IDENTITY(1,1) PRIMARY KEY,
--     ProductCode NVARCHAR(20) NOT NULL UNIQUE,
--     Name NVARCHAR(100) NOT NULL,
--     Price DECIMAL(18,2) NOT NULL,
--     Stock INT NOT NULL DEFAULT 0,
--     Category NVARCHAR(50) NOT NULL,
--     IsActive BIT NOT NULL DEFAULT 1,

--     CONSTRAINT CK_Products_Price
--         CHECK(Price >= 0),
--     CONSTRAINT CK_Product_Stock
--         CHECK(Stock >= 0)
-- );
-- Select name From sys.databases;
-- SELECT DB_NAME() AS CurrentDatabase;
-- GO

-- SELECT name FROM sys.tables;
-- SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
-- INSERT INTO dbo.Product (ProductCode, Name, Price, Stock, Category) 
-- VALUES 
-- ('PR001', 'Keyboard', 28000, 10, 'Elektronik'),
-- ('PR002', 'Mouse', 10000, 48, 'Elektrik');
-- UPDATE dbo.Product SET Price = 5000.00, Stock = 3 WHERE ProductCode = 'PR001';
-- DELETE FROM dbo.Product WHERE ProductId = 2;
-- SELECT * FROM dbo.Product;
-- GO

USE ecommercedb;
-- DROP TABLE dbo.Customer;
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.Tables
Where TABLE_TYPE = 'BASE TABLE';
GO

select * FROM __EFMigrationsHistory;

select * FROM Product;
select * FROM Orders;
select * FROM OrderItems;
