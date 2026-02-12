CREATE TYPE employment_status AS ENUM (
    'ACTIVE',
    'INACTIVE',
    'ON_LEAVE',
    'TERMINATED'
);


CREATE TABLE employees (
    -- Numeric Types
    employee_id        SERIAL PRIMARY KEY,
	-- in latest versin we use identity as employee_id INT GENERATED ALWAYS/BY DEFAULT AS IDENTITY 
    salary             NUMERIC(12,2),
    bonus_percentage   REAL,
    experience_years   SMALLINT,

    -- Character Types
    first_name         VARCHAR(50),
    last_name          CHAR(10),
    email              TEXT,

    -- Boolean	
    is_permanent       BOOLEAN DEFAULT TRUE,

    -- Date & Time
    date_of_birth      DATE,
    joining_time       TIME,
    created_at         TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    last_login         TIMESTAMPTZ,

    -- UUID
    employee_uuid      UUID DEFAULT gen_random_uuid(),

    -- ENUM
    status             employment_status,

    -- JSON / JSONB
    address            JSON,
    skills             JSONB,

    -- Array
    phone_numbers      TEXT[],

    -- Network Types
    office_ip          INET,
    mac_address        MACADDR,

    -- Binary Data
    profile_picture    BYTEA,

    -- Range Type
    working_hours      TSRANGE,

    -- Constraints
    UNIQUE (email)
);


INSERT INTO employees (
    salary, bonus_percentage, experience_years,
    first_name, last_name, email,
    is_permanent,
    date_of_birth, joining_time, last_login,
    status,
    address, skills,
    phone_numbers,
    office_ip, mac_address,
    working_hours
)
VALUES (
    85000.50, 10.5, 5,
    'Nisharg', 'Patel', 'nisharg.patel@company.com',
    TRUE,
    '1996-08-15', '09:30:00', CURRENT_TIMESTAMP,
    'ACTIVE',
    '{"city":"Ahmedabad","state":"Gujarat","pincode":380015}',
    '{"languages":["C#","SQL","PostgreSQL"],"level":"Senior"}',
    ARRAY['+91-9999999999', '+91-8888888888'],
    '192.168.1.10',
    '08:00:2b:01:02:03',
    '[2026-02-01 09:00, 2026-02-01 18:00)'
);


SELECT
    address->>'city' AS city,
    skills->'languages' AS tech_stack
FROM employees;


SELECT phone_numbers[1] FROM employees;

SELECT *
FROM employees
WHERE working_hours @> '2026-02-01 10:00'::timestamp; -- :: is used for type casting and @> used for contains

UPDATE employees
SET salary = salary + 5000,
    skills = skills || '{"certification":"Azure"}' -- || it is jsonb merge operator that adds fields to jsonb and replaces if already present in the object  
WHERE employee_id = 1;


--upsert
INSERT INTO employees (email, first_name, status)
VALUES ('nisharg.patel@company.com', 'Nisharg', 'ACTIVE')
ON CONFLICT (email)
DO 
UPDATE
SET status = EXCLUDED.status,
    last_login = CURRENT_TIMESTAMP;

select * from employees where employee_id =1;

DELETE FROM employees
WHERE status = 'TERMINATED';



ALTER TABLE employees
ALTER COLUMN employee_id
DROP DEFAULT;

ALTER TABLE employees
ALTER COLUMN employee_id
ADD GENERATED ALWAYS AS IDENTITY;

SELECT MAX(employee_id) FROM employees;
SELECT pg_get_serial_sequence('employees', 'employee_id');
SELECT setval(
    pg_get_serial_sequence('employees', 'employee_id'),
    (SELECT MAX(employee_id) FROM employees)
);


select * from employees;
ALTER TABLE employees
ALTER COLUMN first_name SET NOT NULL,
ALTER COLUMN email SET NOT NULL,
ALTER COLUMN status SET NOT NULL;

ALTER TABLE employees
ADD CONSTRAINT chk_salary_positive
CHECK (salary > 0);

ALTER TABLE employees
ALTER COLUMN status SET DEFAULT 'ACTIVE';

CREATE TABLE departments (
    department_id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    department_name TEXT UNIQUE NOT NULL
);




INSERT INTO departments (department_name)
VALUES ('IT'), ('HR'), ('Finance');

ALTER TABLE employees
ADD COLUMN department_id INT;

ALTER TABLE employees
ADD CONSTRAINT fk_employee_department
FOREIGN KEY (department_id)
REFERENCES departments(department_id);

UPDATE employees
SET department_id = 1
WHERE employee_id = 1;

INSERT INTO employees (first_name, email, status)
VALUES ('Jay', 'jay@company.com', 'ACTIVE')
RETURNING employee_id, employee_uuid, created_at;

UPDATE employees
SET salary = salary * 1.10
WHERE employee_id = 1
RETURNING employee_id, salary;

DELETE FROM employees
WHERE status = 'INACTIVE'
RETURNING employee_id, email;

SELECT
    e.employee_id,
    e.first_name,
    d.department_name
FROM employees e
JOIN departments d
ON e.department_id = d.department_id;

select * from employees;


INSERT INTO employees (
    first_name, last_name, email, status, salary, experience_years
)
VALUES
('Amit',  'Shah',   'amit.shah@company.com',  'ACTIVE',   60000, 3),
('Neha',  'Mehta',  'neha.mehta@company.com', 'ACTIVE',   72000, 5),
('Rahul', 'Verma',  'rahul.verma@company.com','ON_LEAVE', 55000, 2),
('Priya', 'Iyer',   'priya.iyer@company.com', 'ACTIVE',   80000, 7),
('Karan', 'Singh',  'karan.singh@company.com','INACTIVE', 45000, 1)
RETURNING employee_id;



SELECT * FROM departments;

UPDATE employees
SET department_id = 1
WHERE employee_id IN (1,2,3);

UPDATE employees
SET department_id = 2
WHERE employee_id IN (4,5);

UPDATE employees
SET department_id = 3
WHERE employee_id IN (6,7);


SELECT 
    first_name,
    salary,
    salary * 12 AS annual_salary
FROM employees;

SELECT *
FROM employees
WHERE salary > 70000;

SELECT *
FROM employees
WHERE bonus_percentage IS NULL;

SELECT *
FROM employees
WHERE email LIKE '%company.com';

SELECT first_name, salary
FROM employees
ORDER BY salary DESC;

-- page 1
SELECT first_name, salary
FROM employees
ORDER BY salary DESC
LIMIT 3;

-- page 2
SELECT first_name, salary
FROM employees
ORDER BY salary DESC
LIMIT 3 OFFSET 3;

SELECT 
    department_id,
    AVG(salary) AS avg_salary
FROM employees
GROUP BY department_id;

SELECT 
    status,
    COUNT(*) 
FROM employees
GROUP BY status;



SELECT
    d.department_name,
    COUNT(*) AS total_employees,
    ROUND(AVG(e.salary),2) AS avg_salary
FROM employees e
JOIN departments d
ON e.department_id = d.department_id
GROUP BY d.department_name;





CREATE VIEW active_employee_view AS
SELECT
    first_name,
    email,
    salary
FROM employees
WHERE status = 'ACTIVE';

select * from active_employee_view;


CREATE MATERIALIZED VIEW dept_salary_summary AS
SELECT
    department_id,
    AVG(salary) AS avg_salary
FROM employees
GROUP BY department_id;

select * from dept_salary_summary;
select * from employees where department_id =3;

REFRESH MATERIALIZED VIEW dept_salary_summary;















