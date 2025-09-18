CREATE VIEW StudentCourseView AS
SELECT
      s.name AS student_name,
      c.course_name
FROM
      students s
INNER JOIN
      courses c
ON
      s.course_id = c.course_id;
      
     
SELECT
      student_name,
      course_name
FROM
      StudentCourseView
WHERE
      course_name = 'DBMS';