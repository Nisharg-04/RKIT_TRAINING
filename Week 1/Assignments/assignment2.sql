USE studentdb;

-- 1. Create tables
CREATE TABLE students(
student_id INT PRIMARY KEY AUTO_INCREMENT,
name VARCHAR(100),
age INT,
gender ENUM('Male', 'Female'),
course_id INT
);

CREATE TABLE courses (
course_id INT PRIMARY KEY AUTO_INCREMENT,
course_name VARCHAR(100),
duration VARCHAR(50)
);

CREATE TABLE marks (
mark_id INT PRIMARY KEY AUTO_INCREMENT,
student_id INT,
subject VARCHAR(100),
score DECIMAL(5,2)
);

-- 2. Modify Students table to add a new column email.
ALTER TABLE Students ADD COLUMN email VARCHAR(100);


-- 3. Drop the Marks table and recreate it with the same structure.
DROP TABLE IF EXISTS Marks;

CREATE TABLE marks (
mark_id INT PRIMARY KEY AUTO_INCREMENT,
student_id INT,
subject VARCHAR(100),
score DECIMAL(5,2)
);