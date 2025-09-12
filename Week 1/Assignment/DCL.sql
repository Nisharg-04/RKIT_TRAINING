CREATE USER 'analyst'@'localhost' IDENTIFIED BY 'SecurePassword123';
GRANT SELECT ON studentdb.* To analyst@localhost;
REVOKE ALL PRIVILEGES ON studentdb.* FROM 'analyst'@'localhost';
