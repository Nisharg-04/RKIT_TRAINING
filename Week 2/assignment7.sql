SELECT
      subject AS name
FROM
      marks

UNION

SELECT
      course_name AS name
FROM
      courses;
      
      
SELECT
      subject AS name
FROM
      marks

UNION ALL

SELECT
      course_name AS name
FROM
      courses;