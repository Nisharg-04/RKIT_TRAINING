-- 1. Write a query to fetch all students above age 20.
SELECT * FROM students WHERE age > 20;

-- 2. Fetch all students ordered by name alphabetically.
SELECT * FROM students ORDER BY name ASC;	

-- 3. Show total number of students enrolled in each course using GROUP BY.
SELECT course_id, COUNT(*) AS total_students
FROM students
GROUP BY course_id;

-- 4. Show courses that have more than 2 students using HAVING
SELECT course_id, COUNT(*) AS student_count
FROM students
GROUP BY course_id
HAVING student_count > 2;