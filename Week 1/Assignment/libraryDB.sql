CREATE SCHEMA `librarydb` ;

USE librarydb;

CREATE TABLE authors (
    author_id INT PRIMARY KEY AUTO_INCREMENT,
    author_name VARCHAR(150),
    country VARCHAR(100)
);

CREATE TABLE books (
    book_id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(200),
    author_id INT,
    publish_year INT,
    price DECIMAL(6,2),
    FOREIGN KEY (author_id) REFERENCES Authors(author_id)
);

CREATE TABLE borrowers (
    borrower_id INT PRIMARY KEY AUTO_INCREMENT,
    book_id INT,
    borrower_name VARCHAR(150),
    borrow_date DATE,
    return_date DATE,
    FOREIGN KEY (book_id) REFERENCES Books(book_id)
);

INSERT INTO Authors (author_name, country) VALUES
('Chetan Bhagat', 'India'),
('Ruskin Bond', 'India'),
('Amish Tripathi', 'India'),
('Arundhati Roy', 'India'),
('R.K. Narayan', 'India');

INSERT INTO Books (title, author_id, publish_year, price) VALUES
('Five Point Someone', 1, 2004, 299.00),
('The Room on the Roof', 5, 1956, 199.00),
('The Shiva Trilogy', 3, 2010, 499.00),
('The God of Small Things', 4, 1997, 399.00),
('Our Trees Still Grow in Dehra', 2, 1991, 249.00);


INSERT INTO Borrowers (book_id, borrower_name, borrow_date, return_date) VALUES
(1, 'Rajesh Kumar', '2025-08-01', '2025-08-15'),
(3, 'Sneha Sharma', '2025-08-05', NULL),
(4, 'Anil Verma', '2025-08-10', '2025-08-20'),
(2, 'Priya Singh', '2025-08-12', NULL),
(5, 'Vikas Mehta', '2025-08-15', '2025-08-25');


-- 1. Retrieve the list of all books along with their author's name.
SELECT b.title, a.author_name
FROM Books b
INNER JOIN Authors a ON b.author_id = a.author_id;

-- 2. Find all authors from the India who published books after 2009.
SELECT a.author_name,b.publish_year
FROM Authors a
INNER JOIN Books b ON a.author_id = b.author_id
WHERE a.country = 'India' AND b.publish_year >2009;

-- 3. List all borrowers who haven’t returned the book yet.
SELECT borrower_name, borrow_date
FROM borrowers
WHERE return_date IS NULL;

-- 4. Number of books per author
SELECT a.author_name, COUNT(b.book_id) AS total_books
FROM authors a
LEFT JOIN books b ON a.author_id = b.author_id
GROUP BY a.author_id;

-- 5. Average price of books per author
SELECT a.author_name, AVG(b.price) AS avg_price
FROM authors a
INNER JOIN books b ON a.author_id = b.author_id
GROUP BY a.author_id;









