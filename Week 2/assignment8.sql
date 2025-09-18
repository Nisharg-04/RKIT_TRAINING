ALTER TABLE
      students
ADD PRIMARY KEY
      (student_id);
      
ALTER TABLE
      courses
MODIFY
      course_id INT AUTO_INCREMENT;
      
      
CREATE INDEX
      idx_email
ON
      students (email);
      
      
EXPLAIN
SELECT
      student_id,
      name,
      course_id
FROM
      students
WHERE
      email = 'nisharg@gmail.com';
      
      
-- Before Index:
-- type = ALL → Full table scan.
-- possible_keys = NULL → No index available for optimizer.
-- rows = 5 → All rows checked.
-- Extra = Using where → Filtering applied after scanning.


-- After Index:
-- type = ref → MySQL used an index-based reference lookup.
-- possible_keys = idx_email → Optimizer considered the email index.
-- key = idx_email → Index on email was chosen.
-- rows = 1 → MySQL jumped directly to the matching row.



