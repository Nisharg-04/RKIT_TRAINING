CREATE DATABASE invdb01;
USE invdb01;
CREATE TABLE usrtb01 (
    usrf01 INT AUTO_INCREMENT PRIMARY KEY,
    usrf02 VARCHAR(100) NOT NULL UNIQUE, -- Username
    usrf03 VARCHAR(255) NOT NULL,        -- Password
    usrf04 VARCHAR(50) NOT NULL,         -- Role
    usrf05 DATETIME DEFAULT CURRENT_TIMESTAMP
);
INSERT INTO usrtb01 (usrf02, usrf03, usrf04)
VALUES
('admin', 'admin123', 'Admin'),
('manager', 'manager123', 'Manager'),
('staff', 'staff123', 'Staff');
CREATE TABLE prdtb01 (
    prdf01 INT AUTO_INCREMENT PRIMARY KEY,
    prdf02 VARCHAR(150) NOT NULL, -- ProductName
    prdf03 DECIMAL(10,2) NOT NULL, -- Price
    prdf04 INT NOT NULL, -- Quantity
    prdf05 DATETIME DEFAULT CURRENT_TIMESTAMP
);
