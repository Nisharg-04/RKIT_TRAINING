SHOW VARIABLES LIKE 'log_bin';
-- This command creates our reliable starting point.
-- mysqldump -u root -p --single-transaction --routines --triggers hoteldb > /backups/hotel_2am.sql
SHOW MASTER STATUS;
USE hoteldb;
INSERT INTO customers (firstname, lastname, phonenumber) 
VALUES ('Arjun', 'Pandya', '9876543215');

INSERT INTO orders (customerid, tablenumber, status) 
VALUES (4, 8, 'Pending');

INSERT INTO orderdetails (orderid, menuitemid, quantity, itempriceatorder) 
VALUES (4, 2, 3, 70.00); 
UPDATE orders SET totalamount = 210.00 WHERE orderid = 4;

DELETE FROM orders;

create  database gujaratibhojanalaydb;