SELECT
    s.student_id,
    s.name,
    s.age,
    s.gender,
    s.course_id,
    s.email,
    m.score
FROM
    students s
    INNER JOIN marks m 
        ON s.student_id = m.student_id
WHERE
    m.score > (
        SELECT AVG(score)
        FROM marks
    );


SELECT
    student_id,
    name,
    age,
    gender,
    course_id,
    email
FROM
    students
WHERE
    course_id = (
        SELECT course_id
        FROM students
        WHERE name = 'Nisharg Soni'
    )
    AND name <> 'Nisharg Soni';
    
    
    SELECT
    s1.student_id,
    s1.name,
    s1.age,
    s1.gender,
    s1.course_id,
    s1.email
FROM
    students s1
WHERE
    s1.course_id = (
        SELECT s2.course_id
        FROM students s2
        WHERE s2.name = 'Nisharg Soni'
          AND s2.course_id = s1.course_id
    )
    AND s1.name <> 'Nisharg Soni';
    
    
SELECT
    s1.student_id,
    s1.name,
    s1.age,
    s1.gender,
    s1.course_id,
    s1.email
FROM
    students s1
WHERE
    EXISTS (
        SELECT 1
        FROM students s2
        WHERE s2.name = 'Nisharg Soni'
          AND s2.course_id = s1.course_id
    )
    AND s1.name <> 'Nisharg Soni';


