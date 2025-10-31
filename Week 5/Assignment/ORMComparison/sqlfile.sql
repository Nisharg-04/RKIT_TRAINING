-- ---------------------------------
-- 1. CREATE TABLES
-- ---------------------------------
CREATE DATABASE IF NOT EXISTS OrmDemo;
USE OrmDemo;

-- (Drop tables if they exist to make this script re-runnable)
DROP TABLE IF EXISTS OrderItems;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Customers;

CREATE TABLE Customers (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100)
);

CREATE TABLE Products (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL
);

CREATE TABLE Orders (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CustomerId INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

CREATE TABLE OrderItems (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- ---------------------------------
-- 2. INSERT SAMPLE DATA
-- ---------------------------------

-- Customers
INSERT INTO Customers (Name, Email) VALUES
('Alice Smith', 'alice@example.com'),   -- Top spender
('Bob Johnson', 'bob@example.com'),
('Charlie Brown', 'charlie@example.com'),
('David Lee', 'david@example.com'),
('Eve Davis', 'eve@example.com'),
('Frank White', 'frank@example.com'),
('Grace Hall', 'grace@example.com');     -- Won't make the top 5

-- Products
INSERT INTO Products (Name, Category) VALUES
('4K Monitor', 'Electronics'),          -- Product 1
('Mechanical Keyboard', 'Electronics'), -- Product 2
('Wireless Mouse', 'Electronics'),      -- Product 3
('The SQL Book', 'Books'),              -- Product 4
('C# Headset', 'Electronics');         -- Product 5 (so 'Electronics' is a mix)

-- Orders & OrderItems (Data is for Q4 2025: Oct 1 - Dec 31)

-- Alice (Spender 1)
INSERT INTO Orders (CustomerId, OrderDate) VALUES (1, '2025-10-15 10:00:00'); -- Order 1
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (1, 1, 1, 300.00); -- 300
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (1, 2, 1, 150.00); -- 150
INSERT INTO Orders (CustomerId, OrderDate) VALUES (1, '2025-11-05 11:00:00'); -- Order 2
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (2, 3, 2, 75.00);  -- 150. Total: 600

-- Bob (Spender 2)
INSERT INTO Orders (CustomerId, OrderDate) VALUES (2, '2025-10-20 12:00:00'); -- Order 3
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (3, 1, 1, 300.00); -- 300
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (3, 5, 1, 80.00);  -- 80. Total: 380

-- Charlie (Spender 3)
INSERT INTO Orders (CustomerId, OrderDate) VALUES (3, '2025-11-10 14:00:00'); -- Order 4
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (4, 2, 2, 150.00); -- 300. Total: 300

-- David (Spends on Books, NOT Electronics)
INSERT INTO Orders (CustomerId, OrderDate) VALUES (4, '2025-10-25 15:00:00'); -- Order 5
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (5, 4, 10, 25.00); -- 250 (Books). Total: 0 in Electronics

-- Eve (Spender 4)
INSERT INTO Orders (CustomerId, OrderDate) VALUES (5, '2025-12-01 16:00:00'); -- Order 6
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (6, 3, 1, 75.00);  -- 75. Total: 75

-- Frank (Spender 5)
INSERT INTO Orders (CustomerId, OrderDate) VALUES (6, '2025-12-10 17:00:00'); -- Order 7
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (7, 5, 1, 80.00);  -- 80. Total: 80

-- Grace (Spends, but NOT in Q4 2025)
INSERT INTO Orders (CustomerId, OrderDate) VALUES (7, '2025-09-15 09:00:00'); -- Order 8 (Q3)
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (8, 1, 1, 300.00); -- 300. Total: 0 in Q4

-- Add one more for Eve to beat Frank
INSERT INTO Orders (CustomerId, OrderDate) VALUES (5, '2025-12-11 10:00:00'); -- Order 9
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES (9, 2, 1, 150.00); -- 150. Eve Total: 75 + 150 = 225

/*
Expected Results:
1. Alice: 600.00
2. Bob: 380.00
3. Charlie: 300.00
4. Eve: 225.00
5. Frank: 80.00
*/