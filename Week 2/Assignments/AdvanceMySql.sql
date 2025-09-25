CREATE DATABASE IF NOT EXISTS hoteldb;
USE hoteldb;

CREATE TABLE customers (
    customerid INT PRIMARY KEY AUTO_INCREMENT,
    firstname VARCHAR(50) NOT NULL,
    lastname VARCHAR(50),
    phonenumber VARCHAR(15) UNIQUE,
    joindate DATE DEFAULT (CURDATE())
);

CREATE TABLE menuitems (
    menuitemid INT PRIMARY KEY AUTO_INCREMENT,
    itemname VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    category ENUM('Farsan', 'Main Course', 'Sweet Dish', 'Beverage'),
    isavailable BOOLEAN DEFAULT TRUE
);

CREATE TABLE orders (
    orderid INT PRIMARY KEY AUTO_INCREMENT,
    customerid INT,
    ordertime DATETIME DEFAULT CURRENT_TIMESTAMP,
    tablenumber INT,
    totalamount DECIMAL(10, 2) DEFAULT 0.00,
    status ENUM('Pending', 'Completed', 'Cancelled') DEFAULT 'Pending',
    FOREIGN KEY (customerid) REFERENCES customers(customerid)
);

CREATE TABLE orderdetails (
    orderdetailid INT PRIMARY KEY AUTO_INCREMENT,
    orderid INT,
    menuitemid INT,
    quantity INT NOT NULL,
    itempriceatorder DECIMAL(10, 2),
    FOREIGN KEY (orderid) REFERENCES orders(orderid),
    FOREIGN KEY (menuitemid) REFERENCES menuitems(menuitemid)
);

CREATE TABLE staff (
    staffid INT PRIMARY KEY AUTO_INCREMENT,
    firstname VARCHAR(50) NOT NULL,
    lastname VARCHAR(50) NOT NULL,
    role ENUM('Manager', 'Maharaj', 'Waiter', 'Host')
);

INSERT INTO customers (firstname, lastname, phonenumber) VALUES
('Jignesh', 'Patel', '9876543210'),
('Meera', 'Shah', '9876543211'),
('Hitesh', 'Desai', '9876543212'),
('Priya', 'Mehta', '9876543213');

INSERT INTO staff (firstname, lastname, role) VALUES
('Ramesh', 'Joshi', 'Manager'),
('Sunil', 'Trivedi', 'Maharaj'),
('Gita', 'Pandya', 'Waiter'),
('Meera', 'Shah', 'Waiter');

INSERT INTO menuitems (itemname, description, price, category) VALUES
('Khandvi', 'Soft, rolled-up snacks made from gram flour.', 80.00, 'Farsan'),
('Dhokla', 'Spongy and savory steamed cake.', 70.00, 'Farsan'),
('Gujarati Thali', 'A complete platter with shaak, roti, dal, rice, and farsan.', 250.00, 'Main Course'),
('Undhiyu', 'A mixed vegetable casserole, a winter specialty.', 180.00, 'Main Course'),
('Sev Tameta nu Shaak', 'A sweet and sour tomato curry with crunchy sev.', 150.00, 'Main Course'),
('Shrikhand', 'A sweet dish made of strained yogurt, flavored with saffron and cardamom.', 100.00, 'Sweet Dish'),
('Jalebi', 'Crispy, deep-fried spirals soaked in sugar syrup.', 90.00, 'Sweet Dish'),
('Chaas', 'Refreshing buttermilk spiced with cumin and coriander.', 40.00, 'Beverage');

INSERT INTO orders (customerid, tablenumber, status) VALUES
(1, 5, 'Completed'),
(2, 3, 'Completed'),
(3, 5, 'Pending');

INSERT INTO orderdetails (orderid, menuitemid, quantity, itempriceatorder) VALUES
(1, 3, 2, 250.00),
(1, 8, 2, 40.00),
(2, 1, 1, 80.00),
(2, 5, 1, 150.00),
(3, 2, 2, 70.00),
(3, 7, 1, 90.00);

UPDATE orders SET totalamount = 580.00 WHERE orderid = 1;
UPDATE orders SET totalamount = 230.00 WHERE orderid = 2;
UPDATE orders SET totalamount = 230.00 WHERE orderid = 3;




-- 1. Find menu items more expensive than the average price
SELECT
    itemname,
    price
FROM
    menuitems
WHERE
    price > (SELECT AVG(price) FROM menuitems);
    
--  Find customers who have placed at least one order
SELECT
    firstname,
    lastname
FROM
    customers
WHERE
    customerid IN (SELECT customerid FROM orders);
    
-- 3. Find any menu item ordered in a quantity of 2 or more
SELECT
    itemname,
    price
FROM
    menuitems
WHERE
    menuitemid = ANY (SELECT menuitemid FROM orderdetails WHERE quantity >= 2);
    
-- 4. Find orders with a total amount greater than the customer's average order amount
SELECT
    orderid,
    customerid,
    totalamount
FROM
    orders o1
WHERE
    totalamount > (SELECT AVG(totalamount) FROM orders o2 WHERE o2.customerid = o1.customerid);
    

-- 5. Find customers who have ordered 'Gujarati Thali
SELECT
    firstname,
    lastname
FROM
    customers c
WHERE EXISTS (
    SELECT 1
    FROM orders o
    JOIN orderdetails od ON o.orderid = od.orderid
    JOIN menuitems mi ON od.menuitemid = mi.menuitemid
    WHERE mi.itemname = 'Gujarati Thali' AND o.customerid = c.customerid
);


-- 6. Display customer name with its most recent order date
SELECT
    firstname,
    lastname,
    (SELECT MAX(ordertime) FROM orders o WHERE o.customerid = c.customerid) AS last_order_date
FROM
    customers c;


-- 7. Subquery in FROM
SELECT
    c.firstname,
    c.lastname,
    sales_summary.total_spent
FROM
    customers c
JOIN
    (SELECT customerid, SUM(totalamount) AS total_spent
     FROM orders
     WHERE status = 'Completed'
     GROUP BY customerid) AS sales_summary
ON
    c.customerid = sales_summary.customerid
ORDER BY
    sales_summary.total_spent DESC;
    
-- 8. Find the second highest order amount
SELECT
    MAX(totalamount) AS second_highest_order
FROM
    orders
WHERE
    totalamount < (SELECT MAX(totalamount) FROM orders);
    
    
-- 9. UNION - A Single List of All People
SELECT firstname, lastname FROM customers
UNION
SELECT firstname, lastname FROM staff;

-- 10. UNION ALL - A Combined Log with Duplicates
SELECT firstname, lastname FROM customers
UNION ALL
SELECT firstname, lastname FROM staff;

-- 11. INTERSECT - Common First Names
SELECT firstname FROM customers
INTERSECT
SELECT firstname FROM staff;

-- 12. EXCEPT - Customers with Unique First Names
SELECT firstname FROM customers
EXCEPT
SELECT firstname FROM staff;

-- 13. Income Expence Report
SELECT
    'Income' AS transaction_type,
    SUM(totalamount) AS amount,
    CURDATE() AS date
FROM orders
WHERE status = 'Completed'
UNION ALL
SELECT
    'Expense' AS transaction_type,
    5000.00 AS amount,
    CURDATE() AS date;
    
    
    
-- Stored Procedures

-- Get Menu Items by Category
DELIMITER $$
CREATE PROCEDURE GetMenuItemsByCategory(IN category_name VARCHAR(50))
BEGIN
    SELECT
        itemname,
        description,
        price
    FROM
        menuitems
    WHERE
        category = category_name AND isavailable = TRUE;
END$$
DELIMITER ;

CALL GetMenuItemsByCategory('Farsan');


--  Add a New Customer
DELIMITER $$

CREATE PROCEDURE AddNewCustomer(
    IN p_firstname VARCHAR(50),
    IN p_lastname VARCHAR(50),
    IN p_phonenumber VARCHAR(15),
    OUT new_customer_id INT
)
BEGIN
    INSERT INTO customers (firstname, lastname, phonenumber)
    VALUES (p_firstname, p_lastname, p_phonenumber);

    -- Get the ID of the record just inserted
    SET new_customer_id = LAST_INSERT_ID();
END$$

DELIMITER ;

CALL AddNewCustomer('Adit', 'Vyas', '9876543218', @adit_id);
SELECT @adit_id; -- This will show the new ID for Adit Vyas

-- IF/ELSE Logic: Create a New Order

DELIMITER $$

CREATE PROCEDURE CreateNewOrder(
    IN p_customerid INT,
    IN p_tablenumber INT
)
BEGIN
    DECLARE customer_exists INT DEFAULT 0;

    SELECT COUNT(*) INTO customer_exists FROM customers WHERE customerid = p_customerid;

    IF customer_exists > 0 THEN
        
        INSERT INTO orders (customerid, tablenumber)
        VALUES (p_customerid, p_tablenumber);
        SELECT 'Order created successfully.' AS message;
    ELSE
        SELECT 'Error: Customer ID does not exist.' AS message;
    END IF;
END$$

DELIMITER ;
CALL CreateNewOrder(1, 10);
CALL CreateNewOrder(99, 12); 


--  Add Item to Order Safely

DELIMITER $$

CREATE PROCEDURE AddItemToOrderWithTransaction(
    IN p_orderid INT,
    IN p_menuitemid INT,
    IN p_quantity INT
)
BEGIN

    DECLARE item_price DECIMAL(10, 2);
    DECLARE item_is_available BOOLEAN;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'An error occurred. Transaction rolled back.' AS result;
    END;

    START TRANSACTION;
    

    SELECT price, isavailable INTO item_price, item_is_available
    FROM menuitems WHERE menuitemid = p_menuitemid;
    
    -- Check if the item is available
    IF item_is_available = TRUE THEN
     
        INSERT INTO orderdetails (orderid, menuitemid, quantity, itempriceatorder)
        VALUES (p_orderid, p_menuitemid, p_quantity, item_price);
        
     
        UPDATE orders
        SET totalamount = totalamount + (item_price * p_quantity)
        WHERE orderid = p_orderid;
        
        
        COMMIT;
        SELECT 'Item added successfully.' AS result;
    ELSE
	
        ROLLBACK;
        SELECT 'Item is not available. Transaction rolled back.' AS result;
    END IF;
END$$

DELIMITER ;

CALL AddItemToOrderWithTransaction(3, 1, 2); 

-- Use of curser for daily summary
DELIMITER $$
CREATE PROCEDURE CalculateTotalSales()
BEGIN
    DECLARE done INT DEFAULT FALSE;
    DECLARE current_amount DECIMAL(10, 2);
    DECLARE total_sales DECIMAL(12, 2) DEFAULT 0.00;

  
    DECLARE sales_cursor CURSOR FOR
        SELECT totalamount FROM orders WHERE status = 'Completed';

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

    OPEN sales_cursor;

    read_loop: LOOP
      
        FETCH sales_cursor INTO current_amount;

        IF done THEN
            LEAVE read_loop;
        END IF;

        SET total_sales = total_sales + current_amount;
    END LOOP;

 
    CLOSE sales_cursor;
    
    SELECT total_sales;
END$$
DELIMITER ;

CALL CalculateTotalSales();


-- Advance Error Handling with signal 
DELIMITER $$
CREATE PROCEDURE AddItemSafely(IN p_orderid INT, IN p_menuitemid INT)
BEGIN
    DECLARE item_count INT;

    SELECT COUNT(*) INTO item_count FROM menuitems WHERE menuitemid = p_menuitemid;

    IF item_count = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Menu item does not exist.';
    ELSE
        SELECT 'Item added' AS message;
    END IF;
END$$
DELIMITER ;

-- This call will fail with the custom error message:
CALL AddItemSafely(3, 999);


-- Stored Functions
-- Simple Function to Calculate Tax
DELIMITER $$

CREATE FUNCTION calculate_tax(amount DECIMAL(10,2))
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
    RETURN amount * 0.05;
END $$

DELIMITER ;
SELECT calculate_tax(100); 


-- Function to Get Customer Full Name
DELIMITER $$

CREATE FUNCTION get_customer_fullname(cust_id INT)
RETURNS VARCHAR(100)
DETERMINISTIC
BEGIN
    DECLARE full_name VARCHAR(100);

    SELECT CONCAT(firstname, ' ', lastname) INTO full_name
    FROM customers
    WHERE customerid = cust_id;

    RETURN full_name;
END $$

DELIMITER ;

SELECT get_customer_fullname(1);

-- Calculate Total Amount for an Order
DELIMITER $$

CREATE FUNCTION calculate_order_total(o_id INT)
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
    DECLARE total DECIMAL(10,2);

    SELECT SUM(quantity * itempriceatorder)
    INTO total
    FROM orderdetails
    WHERE orderid = o_id;

    IF total IS NULL THEN
        SET total = 0;
    END IF;

    RETURN total;
END $$

DELIMITER ;

SELECT calculate_order_total(2);

-- Check if a Menu Item Is Available
DELIMITER $$

CREATE FUNCTION is_item_available(menu_id INT)
RETURNS BOOLEAN
DETERMINISTIC
BEGIN
    DECLARE availability BOOLEAN;

    SELECT isavailable INTO availability
    FROM menuitems
    WHERE menuitemid = menu_id;

    IF availability IS NULL THEN
        RETURN FALSE;
    END IF;

    RETURN availability;
END $$

DELIMITER ;

SELECT is_item_available(1);

SET GLOBAL log_bin_trust_function_creators = 1; -- This tells MySQL to trust function creators and allow creation of non-deterministic functions without explicit characteristics.
-- This is less safe for replication environments because non-deterministic behavior may cause inconsistencies across master and slave servers.
-- Non- Deterministic Function
DELIMITER $$

CREATE FUNCTION get_current_time()
RETURNS DATETIME
NOT DETERMINISTIC
contains sql
BEGIN
    RETURN NOW();
END $$

DELIMITER ;



-- Triggers
-- Automatically set totalamount = 0 when a new order is inserted, if not provided.
DELIMITER $$

CREATE TRIGGER before_order_insert
BEFORE INSERT ON orders
FOR EACH ROW
BEGIN
    IF NEW.totalamount IS NULL THEN
        SET NEW.totalamount = 0;
    END IF;
END$$

DELIMITER ;

INSERT INTO orders (customerid, tablenumber, status) VALUES (1, 2, 'Pending');
SELECT * FROM orders;


-- AFTER INSERT Trigger for Audit Logging
CREATE TABLE customer_audit (
    auditid INT AUTO_INCREMENT PRIMARY KEY,
    customerid INT,
    action VARCHAR(20),
    actiontime DATETIME DEFAULT CURRENT_TIMESTAMP
);

DELIMITER $$

CREATE TRIGGER after_customer_insert
AFTER INSERT ON customers
FOR EACH ROW
BEGIN
    INSERT INTO customer_audit (customerid, action)
    VALUES (NEW.customerid, 'INSERT');
END$$

DELIMITER ;

INSERT INTO customers (firstname, lastname, phonenumber) VALUES
('Twinkle', 'Dave', '9876843210');

SELECT * FROM customer_audit;


-- BEFORE UPDATE Trigger for Data Validation
DELIMITER $$

CREATE TRIGGER before_menuitem_update
BEFORE UPDATE ON menuitems
FOR EACH ROW
BEGIN
    IF NEW.price < 0 THEN
        SET NEW.price = OLD.price;
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Price must be a positive number.';
    END IF;
END$$

DELIMITER ;


UPDATE menuitems
SET price = -80.00 
WHERE menuitemid=1;

SELECT * FROM menuitems;

-- Maintaining Order Total Automatically
DELIMITER $$

CREATE TRIGGER after_orderdetails_insert
AFTER INSERT ON orderdetails
FOR EACH ROW
BEGIN
    UPDATE orders
    SET totalamount = totalamount + (NEW.quantity * NEW.itempriceatorder)
    WHERE orderid = NEW.orderid;
END$$

DELIMITER ;

INSERT INTO orderdetails (orderid, menuitemid, quantity, itempriceatorder) VALUES
(4, 3, 2, 250.00);
SELECT * FROM orders;



CREATE INDEX idx_customerid ON orders(customerid);

EXPLAIN SELECT * FROM orders WHERE customerid = 1;
EXPLAIN FORMAT=JSON SELECT * FROM orders WHERE customerid = 1;