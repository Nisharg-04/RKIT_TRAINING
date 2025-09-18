DELIMITER $$

CREATE PROCEDURE getAllStudentsBasedOnCourseName (
      IN courseName VARCHAR(100)
)
BEGIN
      SELECT
            s.student_id,
            s.name,
            s.age,
            s.gender,
            s.course_id,
            s.email
      FROM
            students s
      INNER JOIN
            courses c
      ON
            s.course_id = c.course_id
      WHERE
            c.course_name = courseName;
END $$

DELIMITER ;


CALL getAllStudentsBasedOnCourseName('DBMS');

-- DROP PROCEDURE getAllStudentsBasedOnCourseName;

DELIMITER $$

CREATE FUNCTION calculateGrade (
      marks INT
)
RETURNS CHAR(1)
DETERMINISTIC
BEGIN
      DECLARE grade CHAR(1);

      IF marks >= 90 THEN
            SET grade = 'A';
      ELSEIF marks >= 80 THEN
            SET grade = 'B';
      ELSEIF marks >= 60 THEN
            SET grade = 'C';
      ELSE
            SET grade = 'D';
      END IF;

      RETURN grade;
END $$

DELIMITER ;



CREATE TABLE IF NOT EXISTS DeletedStudents (
      student_id INT,
      name VARCHAR(100),
      age INT,
      gender VARCHAR(10),
      course_id INT,
      email VARCHAR(100),
      deleted_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

SELECT
      s.student_id,
      s.name,
      m.score,
      calculateGrade(m.score) AS grade
FROM
      students s
INNER JOIN
      marks m
ON
      s.student_id = m.student_id



DELIMITER $$

CREATE TRIGGER after_student_delete
AFTER DELETE ON students
FOR EACH ROW
BEGIN
      INSERT INTO DeletedStudents (
            student_id,
            name,
            age,
            gender,
            course_id,
            email
      )
      VALUES (
            OLD.student_id,
            OLD.name,
            OLD.age,
            OLD.gender,
            OLD.course_id,
            OLD.email
      );
END $$

DELIMITER ;
