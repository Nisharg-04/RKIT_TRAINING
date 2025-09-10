-- 1. Insert at least 5 rows into each table.
-- Insert into Courses
INSERT INTO courses (course_name, duration) VALUES
('DBMS', '6 months'),
('OS', '1 year'),
('Python', '1 year'),
('Java', '6 months'),
('C++', '6 months');

-- Insert into Students
INSERT INTO Students (name, age, gender, course_id, email) VALUES
('Nisharg Soni', 21, 'Male', 1, 'nisharg@gmail.com'),
('Dakshil Gorasiya', 19, 'Male', 3, 'dakshil@gmail.com'),
('Diya Mehta', 22, 'Female', 2, 'diya@gmail.com'),
('Manish Patel', 20, 'Male', NULL, 'manish@gmail.com'),
('Krisha Shah', 23, 'Female', 1, 'krisha@gmail.com');

-- Insert into Marks
INSERT INTO Marks (student_id, subject, score) VALUES
(1, 'DBMS', 88.5),
(2, 'Python', 92.0),
(3, 'OS', 85.0),
(4, 'DLD', 78.0),
(5, 'DBMS', 91.5);

-- 2. Update one student’s course.
UPDATE Students SET course_id = 4 WHERE student_id = 2;

-- 3. Delete a student record.
DELETE FROM Students WHERE student_id = 4;

