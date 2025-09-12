-- 1. Display students with their enrolled course names using INNER JOIN.
SELECT 
	s.student_id, 
	s.name, 
	c.course_id, 
	c.course_name
FROM 
	students s
	INNER JOIN 
	courses c 
	ON 
	s.course_id = c.course_id;

-- 2. Display all students even if they are not enrolled in any course (LEFT JOIN).
SELECT 
	s.student_id, 
	s.name, 
	c.course_id, 
	c.course_name
FROM 
	students s
	LEFT JOIN 
	courses c 
	ON 
	s.course_id = c.course_id;

-- 3. Display all courses and their students (RIGHT JOIN).
SELECT 
	s.student_id, 
	s.name, 
	c.course_id, 
	c.course_name
FROM 
	students s
	RIGHT JOIN 
	courses c 
	ON 
	s.course_id = c.course_id;

-- 4. Find highest, lowest, and average marks per subject.
SELECT 
	subject, 
	MAX(score) AS max_score, 
	MIN(score) AS min_score,
	AVG(score) AS avg_score
FROM 
	marks
GROUP BY 
	subject;

-- 5. Count how many male and female students exist.
SELECT 
	gender, 
	COUNT(*) AS total_count
FROM 
	students
GROUP BY 
	gender;