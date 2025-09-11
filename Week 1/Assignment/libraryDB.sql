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

ALTER TABLE books
ADD COLUMN genre VARCHAR(50) NOT NULL DEFAULT 'Uncategorized';

SELECT * FROM books;


ALTER TABLE books
ADD CONSTRAINT chk_price CHECK (price > 0);


UPDATE books SET genre = 'Contemporary' WHERE book_id = 1;
UPDATE books SET genre = 'Classic' WHERE book_id = 2;
UPDATE books SET genre = 'Mythology' WHERE book_id = 3;
UPDATE books SET genre = 'Fiction' WHERE book_id = 4;
UPDATE books SET genre = 'Classic' WHERE book_id = 5;


ALTER TABLE borrowers
ADD COLUMN borrower_email VARCHAR(255),
ADD COLUMN due_date DATE;

ALTER TABLE borrowers
ADD CONSTRAINT chk_return_date CHECK (return_date IS NULL OR return_date >= borrow_date);

ALTER TABLE borrowers
ADD CONSTRAINT chk_email CHECK (borrower_email LIKE '%@%.%');

SELECT * FROM borrowers;

UPDATE borrowers SET borrower_email = 'rajesh@example.com', due_date = '2025-08-22' WHERE borrower_id = 1;
UPDATE borrowers SET borrower_email = 'sneha@example.com', due_date = '2025-08-25' WHERE borrower_id = 2;
UPDATE borrowers SET borrower_email = 'anil@example.com', due_date = '2025-08-30' WHERE borrower_id = 3;
UPDATE borrowers SET borrower_email = 'priya@example.com', due_date = '2025-09-01' WHERE borrower_id = 4;
UPDATE borrowers SET borrower_email = 'vikas@example.com', due_date = '2025-09-05' WHERE borrower_id = 5;

-- 6. Find all books that are currently overdue.
SELECT
    br.borrower_name,
    br.borrower_email,
    b.title,
    br.due_date
FROM borrowers AS br
JOIN books AS b ON br.book_id = b.book_id
WHERE br.return_date IS NULL AND br.due_date < CURDATE();

-- 7. List all books that have never been borrowed.
SELECT b.title
FROM books AS b
LEFT JOIN borrowers AS br ON b.book_id = br.book_id
WHERE br.borrower_id IS NULL;

-- 8. Categorize books by their publication era using a CASE statement.
SELECT
    title,
    publish_year,
    CASE
        WHEN publish_year < 1990 THEN 'Classic Era'
        WHEN publish_year >= 1990 AND publish_year < 2010 THEN 'Modern Era'
        ELSE 'Contemporary Era'
    END AS era
FROM books;

-- 9. Find the average number of days a book is borrowed for.
SELECT
    AVG(DATEDIFF(return_date, borrow_date)) AS avg_borrow_duration_days
FROM borrowers
WHERE return_date IS NOT NULL;

-- 10. Create a summary report of how many books were borrowed in each month of 2025.
SELECT
    COUNT(CASE WHEN MONTH(borrow_date) = 8 THEN 1 END) AS 'August_Borrows',
    COUNT(CASE WHEN MONTH(borrow_date) = 9 THEN 1 END) AS 'September_Borrows'
FROM borrowers
WHERE YEAR(borrow_date) = 2025;