CREATE DATABASE IF NOT EXISTS minipayrolldb;
USE minipayrolldb;

-- Departments Table (Master data for departments)
-- Table: prlm01
CREATE TABLE prlm01 (
    t01f01 INT PRIMARY KEY AUTO_INCREMENT,  -- dept_id: Unique Department ID
    t01f02 VARCHAR(100) NOT NULL UNIQUE     -- dept_name: Name of the Department
);


-- Employees Table (Master data for employees)
-- Table: prlm02
CREATE TABLE prlm02 (
    t02f01 INT PRIMARY KEY AUTO_INCREMENT,  -- emp_id: Unique Employee ID
    t02f02 VARCHAR(150) NOT NULL,           -- name: Employee Full Name
    t02f03 INT,                              -- dept_id: Foreign key linking to prlm01
    t02f04 DECIMAL(10, 2) NOT NULL,         -- salary: Base Salary of the Employee
    FOREIGN KEY (t02f03) REFERENCES prlm01(t01f01)  -- FK constraint to Departments
);

-- Salaries Table (Transactional records of monthly payments)
-- Table: prlt01
CREATE TABLE prlt01 (
    t03f01 INT PRIMARY KEY AUTO_INCREMENT,  -- salary_log_id: Unique Salary Record ID
    t03f02 INT,                              -- emp_id: Foreign key linking to prlm02
    t03f03 VARCHAR(20) NOT NULL,             -- month: Month of Payment
    t03f04 DECIMAL(10, 2) NOT NULL,          -- amount: Salary Amount Paid
    t03f05 DATE,                              -- pay_date: Date when salary was paid
    FOREIGN KEY (t03f02) REFERENCES prlm02(t02f01) -- FK constraint to Employees
);

-- Salary Audit Table (Audit trail for changes to master salary)
-- Table: prla01
CREATE TABLE prla01 (
    t04f01 INT PRIMARY KEY AUTO_INCREMENT,   -- audit_id: Unique Audit Record ID
    t04f02 INT,                               -- emp_id: Employee whose salary changed
    t04f03 DECIMAL(10,2),                     -- old_salary: Previous Salary Value
    t04f04 DECIMAL(10,2),                     -- new_salary: Updated Salary Value
    t04f05 DATETIME DEFAULT CURRENT_TIMESTAMP, -- change_date: Timestamp of Change
    t04f06 VARCHAR(150)                        -- changed_by: User who made the change
);

INSERT INTO prlm01 (t01f02) VALUES 
('Technology'), 
('Human Resources'), 
('Sales');

INSERT INTO prlm02 (t02f02, t02f03, t02f04) VALUES
('Aarav Sharma', 1, 90000.00),
('Priya Singh', 1, 95000.00),
('Rohan Mehta', 3, 75000.00),
('Aditi Gupta', 3, 80000.00),
('Vikram Rathore', 2, 65000.00);

INSERT INTO prlt01 (t03f02, t03f03, t03f04, t03f05) VALUES
(1, 'January', 90000.00, '2025-01-31'),
(2, 'January', 95000.00, '2025-01-31'),
(3, 'January', 75000.00, '2025-01-31'),
(1, 'February', 90000.00, '2025-02-28'),
(2, 'February', 95000.00, '2025-02-28');



-- Create a comprehensive report showing which employee from which department was paid what amount in a specific month.
SELECT
      emp.t02f02 AS employee_name,
      dept.t01f02 AS department_name,
      sal.t03f04 AS amount_paid,
      sal.t03f03 AS month
FROM
      prlm02 AS emp
JOIN
      prlm01 AS dept ON emp.t02f03 = dept.t01f01
JOIN
      prlt01 AS sal ON emp.t02f01 = sal.t03f02
WHERE
      sal.t03f03 = 'January';
      
-- Find employees whose salary is greater than the average salary of their respective department.
SELECT
      t02f02,
      t02f04
FROM
      prlm02 AS e1
WHERE
      t02f04 > (
            SELECT
                  AVG(t02f04)
            FROM
                  prlm02 AS e2
            WHERE
                  e2.t02f03 = e1.t02f03
      );
    
-- Stored Procedure to Calculate Yearly Salary    
DELIMITER $$
CREATE PROCEDURE usp_CalculateYearlySalary(IN p_emp_id INT, OUT p_yearly_salary DECIMAL(12, 2))
BEGIN
    DECLARE v_monthly_salary DECIMAL(10, 2);

    SELECT t02f04 INTO v_monthly_salary FROM prlm02 WHERE t02f01 = p_emp_id;

    IF v_monthly_salary IS NULL THEN
        SET p_yearly_salary = NULL; -- Indicates employee not found
    ELSE
        SET p_yearly_salary = v_monthly_salary * 12;
    END IF;
END$$
DELIMITER ;

CALL usp_CalculateYearlySalary(1, @yearly_salary);
SELECT @yearly_salary;


-- Trigger to Auto-Log Salary Insertions
DELIMITER $$
CREATE TRIGGER trg_after_employee_salary_update
AFTER UPDATE ON prlm02
FOR EACH ROW
BEGIN
    -- check if the salary value has actually changed
    IF OLD.t02f04 <> NEW.t02f04 THEN
        INSERT INTO prla01(t04f02, t04f03, t04f04, t04f06)
        VALUES (OLD.t02f01, OLD.t02f04, NEW.t02f04, USER());
    END IF;
END$$
DELIMITER ;


UPDATE prlm02 SET t02f04 = 92000.00 WHERE t02f01 = 1;
SELECT * FROM prla01;


-- View for Employee Salary Summary
CREATE OR REPLACE VIEW vw_employee_summary AS
SELECT
      emp.t02f01 AS employee_id,
      emp.t02f02 AS employee_name,
      emp.t02f04 AS employee_salary,
      dept.t01f02 AS department_name
      
FROM
      prlm02 AS emp
JOIN
      prlm01 AS dept ON emp.t02f03 = dept.t01f01;

SELECT 
		employee_id,
        employee_name,
        employee_salary,
        department_name
FROM 
		vw_employee_summary;
        
        
        
-- Monthly Payroll Run
DELIMITER $$
CREATE PROCEDURE usp_RunMonthlyPayroll(IN p_month VARCHAR(20), IN p_pay_date DATE)
BEGIN
    DECLARE done INT DEFAULT FALSE;
    DECLARE v_emp_id INT;
    DECLARE v_salary DECIMAL(10, 2);
    
    DECLARE emp_cursor CURSOR FOR SELECT t02f01, t02f04 FROM prlm02;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'An error occurred. Payroll run has been rolled back.' AS result;
    END;

    START TRANSACTION;
    
    OPEN emp_cursor;
    read_loop: LOOP
        FETCH emp_cursor INTO v_emp_id, v_salary;
        IF done THEN
            LEAVE read_loop;
        END IF;
        
        INSERT INTO prlt01 (t03f02, t03f03, t03f04, t03f05)
        VALUES (v_emp_id, p_month, v_salary, p_pay_date);
    END LOOP;
    
    CLOSE emp_cursor;
    COMMIT;
    SELECT 'Payroll run completed successfully.' AS result;
END$$
DELIMITER ;


CALL usp_RunMonthlyPayroll('November', '2025-10-05');
select * from prlt01;


-- User-Defined Function for Bonus Calculation
DELIMITER $$
CREATE FUNCTION ufn_CalculatePerformanceBonus(p_salary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
    
    DECLARE v_bonus DECIMAL(10,2);
    SET v_bonus = p_salary * 0.10; -- 10% of monthly salary
    RETURN v_bonus;
END$$
DELIMITER ;

SELECT
      t02f02 AS name,
      t02f04 AS salary,
      ufn_CalculatePerformanceBonus(t02f04) AS estimated_bonus
FROM
      prlm02;
      

-- User-Defined Function for Tax Calculation       
DELIMITER $$
CREATE FUNCTION ufn_CalculateTax(salary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
    DECLARE tax DECIMAL(10,2);
    IF salary > 70000 THEN SET tax = salary * 0.20;
    ELSEIF salary > 50000 THEN SET tax = salary * 0.10;
    ELSE SET tax = salary * 0.05;
    END IF;
    RETURN tax;
END $$
DELIMITER ;

SELECT
      t02f02 AS name,
      t02f04 AS salary,
      ufn_CalculateTax(t02f04) AS estimated_tax
FROM
      prlm02;

      
CREATE USER 'hr_user'@'localhost' IDENTIFIED BY 'hradmin123';

GRANT SELECT ON minipayrolldb.vw_employee_summary TO 'hr_user'@'localhost';


EXPLAIN
SELECT
      *
FROM
      prlm02
WHERE
      t02f02 = 'Rohan Mehta';
      
CREATE INDEX idx_emp_name ON prlm02 (t02f02);


--  Find the Department with the Highest Total Salary Expenditure using CTE
WITH DepartmentTotalSalaries AS (
    SELECT
          dept.t01f02 AS department_name,
          SUM(emp.t02f04) AS total_salary_expenditure
    FROM
          prlm02 AS emp
    JOIN
          prlm01 AS dept ON emp.t02f03 = dept.t01f01
    GROUP BY
          dept.t01f02
)
SELECT
      department_name,
      total_salary_expenditure
FROM
      DepartmentTotalSalaries
WHERE
      total_salary_expenditure = (
            SELECT
                  MAX(total_salary_expenditure)
            FROM
                  DepartmentTotalSalaries
      );