SET search_path = plpgsql;

-- smallest block
DO $$
BEGIN
   RAISE NOTICE 'Hello RKIT';
END;
$$;

-- Basic Anatomy of block
DO $$
DECLARE
    -- variables live here
BEGIN
    -- logic lives here
EXCEPTION
    -- error handling lives here
END;
$$;

-- Variable creation space 
DECLARE
   emp_name text;
   salary numeric := 50000;

-- Different Raise types
RAISE NOTICE  -- informational
RAISE WARNING -- caution
RAISE EXCEPTION -- crash immediately


-- Exception
DO $$
DECLARE
   x integer := 10;
   y integer := 0;
   result numeric;
BEGIN
   result := x / y;

EXCEPTION
   WHEN division_by_zero THEN
      RAISE NOTICE 'Cannot divide by zero!';
END;
$$;

--nested blocks
DO $$
BEGIN
   RAISE NOTICE 'Outer block';

   DECLARE
      msg text := 'Inner block';
   BEGIN
      RAISE NOTICE '%', msg;
   END;

END;
$$;


-- advance in declaring variable 
DECLARE
   x integer; -- by default it is null not 0

-- to get type from table directly
DECLARE
	salary employees.salary%TYPE;


-- For Storig Entire Row of table into one single variable 
-- there are two variations rowtype and record
-- Known structure → %ROWTYPE
-- Dynamic query → RECORD
DECLARE
   emp_record employees%ROWTYPE;
SELECT *
INTO emp
FROM employees
WHERE employee_id = 1;

DECLARE
   rec RECORD;
SELECT first_name, salary
INTO rec
FROM employees
WHERE employee_id = 1;


--- for declating constants 
DECLARE
   tax_rate CONSTANT numeric := 0.18;

-- using select into to asssign value to variable if multiple rows are returned then error but if no row then null to prevent that use "STRICT"
SELECT salary
INTO STRICT emp_salary
FROM employees
WHERE employee_id = 1;

-- Control Statements
-- if 
IF condition THEN
   -- code
END IF;

IF condition THEN
   -- true block
ELSE
   -- false block
END IF;



DO $$
DECLARE
   v_salary numeric := 60000;
BEGIN

IF v_salary > 100000 THEN
   RAISE NOTICE 'Executive';

ELSIF v_salary > 50000 THEN
   RAISE NOTICE 'Mid-level';

ELSE
   RAISE NOTICE 'Entry-level';
END IF;

END;
$$;

-- CASE Statement

CASE
   WHEN condition THEN result
   WHEN condition THEN result
   ELSE result
END CASE;

--e.g
DO $$
DECLARE
   v_rating integer := 1;
BEGIN

   CASE
      WHEN v_rating = 5 THEN
         RAISE NOTICE 'Excellent';

      WHEN v_rating >= 4 THEN
         RAISE NOTICE 'Good';

      ELSE
         RAISE NOTICE 'Average';
   END CASE;

END;
$$;


-- Loops
--1. LOOP (infinite until stopped)
--2. WHILE
--3. FOR 
--4. FOREACH (arrays)	

--runs forever until stopped
LOOP
   -- code
END LOOP;

--e.g
DO $$
DECLARE
   counter integer := 0;
BEGIN

   LOOP
   	 
   counter := counter + 1;

	continue when counter =3;

	      RAISE NOTICE 'Counter: %', counter;
 


   	     EXIT WHEN counter = 5;
   END LOOP;

END;
$$;


-- while loop 
WHILE condition LOOP
   -- code
END LOOP;

DO $$
DECLARE
   counter integer := 1;
BEGIN

   WHILE counter <= 5 LOOP
      RAISE NOTICE '%', counter;
      counter := counter + 1;
   END LOOP;

END;
$$;


--for loop
-- numeric for loop 
FOR i IN 1..5 LOOP
   RAISE NOTICE '%', i;
END LOOP;


--query based for loop
DO $$
DECLARE
   emp RECORD;
BEGIN

   FOR emp IN
      SELECT first_name, salary
      FROM employees
   LOOP
      RAISE NOTICE '% earns %',
            emp.first_name,
            emp.salary;
   END LOOP;

END;
$$;



-- foreach loop
DO $$
DECLARE
   nums integer[] := ARRAY[10,20,30];
   n integer;
BEGIN

   FOREACH n IN ARRAY nums LOOP
      RAISE NOTICE '%', n;
   END LOOP;

END;
$$;

CREATE TABLE departments (
    department_id SERIAL PRIMARY KEY,
    department_name TEXT NOT NULL UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE employees (
    employee_id SERIAL PRIMARY KEY,
    
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL,
    phone TEXT UNIQUE,
    
    hire_date DATE NOT NULL DEFAULT CURRENT_DATE,
    
    salary NUMERIC(10,2) CHECK (salary > 0),
    bonus NUMERIC(10,2) DEFAULT 0,
    
    is_active BOOLEAN DEFAULT TRUE,
    
    department_id INT,
    
    manager_id INT,
    
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_department
        FOREIGN KEY(department_id)
        REFERENCES departments(department_id),

    CONSTRAINT fk_manager
        FOREIGN KEY(manager_id)
        REFERENCES employees(employee_id)
);
INSERT INTO departments (department_name) VALUES
('Engineering'),
('HR'),
('Sales'),
('Finance'),
('Support');
INSERT INTO employees 
(first_name, last_name, email, phone, salary, department_id)
VALUES
('Amit', 'Shah', 'amit@company.com', '9990001111', 120000, 1),
('Neha', 'Verma', 'neha@company.com', '9990002222', 95000, 2);
INSERT INTO employees
(first_name, last_name, email, phone, salary, department_id, manager_id)
VALUES
('Rahul', 'Patel', 'rahul@company.com', '9990003333', 70000, 1, 1),
('Priya', 'Mehta', 'priya@company.com', '9990004444', 72000, 1, 1),
('Karan', 'Joshi', 'karan@company.com', '9990005555', 50000, 3, 2),
('Sneha', 'Iyer', 'sneha@company.com', '9990006666', 48000, 4, 2),
('Arjun', 'Reddy', 'arjun@company.com', '9990007777', 65000, 1, 1),
('Pooja', 'Nair', 'pooja@company.com', '9990008888', 52000, 5, 2);



-- FUNCTIONS
CREATE OR REPLACE FUNCTION function_name(parameters)
RETURNS return_type
LANGUAGE plpgsql
AS $$
DECLARE
   -- variables
BEGIN
   -- logic
   RETURN value;
END;
$$;

set search_path = plpgsql;
CREATE OR REPLACE FUNCTION get_employee_salary(
   p_employee_id integer
)
RETURNS numeric
LANGUAGE plpgsql
AS $$
DECLARE
   v_salary employees.salary%TYPE;
BEGIN

   SELECT salary
   INTO v_salary
   FROM employees
   WHERE employee_id = p_employee_id;

   RETURN v_salary;

END;
$$;

select get_employee_salary(2);

-- Returninng multiple values
CREATE or replace FUNCTION get_employee_info(
   p_employee_id integer,
   OUT o_name text,
   OUT o_salary numeric
)
LANGUAGE plpgsql
AS $$
BEGIN
   SELECT first_name, salary
   INTO o_name, o_salary
   FROM employees
   WHERE employee_id = p_employee_id;
END;
$$;

SELECT * FROM get_employee_info(1);

CREATE or replace FUNCTION get_high_paid_employees()
RETURNS TABLE(name text, salary_paid numeric)
LANGUAGE plpgsql
AS $$
BEGIN
   RETURN QUERY
   SELECT first_name, salary
   FROM employees
   WHERE salary > 50000;
END;
$$;

SELECT * FROM get_high_paid_employees();



-- Procedures
CREATE PROCEDURE procedure_name(parameters)
LANGUAGE plpgsql
AS $$
BEGIN

   -- logic

END;
$$;

CREATE OR REPLACE PROCEDURE give_bonus(
    bonus_percent NUMERIC
)
LANGUAGE plpgsql
AS $$
BEGIN

    UPDATE employees
    SET salary = salary + (salary * bonus_percent / 100);

    COMMIT;

END;
$$;

CALL give_bonus(10);


CREATE OR REPLACE PROCEDURE transfer_salary(
    from_emp INT,
    to_emp INT,
    amount NUMERIC
)
LANGUAGE plpgsql
AS $$
BEGIN

    -- deduct
    UPDATE employees
    SET salary = salary - amount
    WHERE employee_id = from_emp;

    -- add
    UPDATE employees
    SET salary = salary + amount
    WHERE employee_id = to_emp;

    COMMIT;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE NOTICE 'Transaction failed!';
END;
$$;

select * from employees;

call transfer_salary(1,8,1000);	
	


CREATE PROCEDURE get_total_employees(
    OUT total INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    SELECT COUNT(*) INTO total FROM employees;
END;
$$;

CALL get_total_employees(NULL);



-- triggers functions 
-- for declaring trigger we first need to make a triggger function 
CREATE OR REPLACE FUNCTION log_employee_insert()
RETURNS TRIGGER
LANGUAGE plpgsql
AS $$
BEGIN

    RAISE NOTICE 'New employee added: %', NEW.first_name;

    RETURN NEW;

END;
$$;

CREATE TRIGGER employee_insert_trigger
AFTER INSERT
ON employees
FOR EACH ROW
EXECUTE FUNCTION log_employee_insert();

INSERT INTO employees(first_name, last_name, email, phone, salary, department_id, manager_id)
VALUES ('Pooa', 'Nar', 'pooja@compa.com', '9990258888', 52000, 5, 2);

CREATE TABLE salary_audit (
    audit_id SERIAL,
    employee_id INT,
    old_salary NUMERIC,
    new_salary NUMERIC,
    changed_at TIMESTAMP DEFAULT NOW()
);

CREATE OR REPLACE FUNCTION track_salary_change()
RETURNS TRIGGER
LANGUAGE plpgsql
AS $$
BEGIN

    IF OLD.salary IS DISTINCT FROM NEW.salary THEN

        INSERT INTO salary_audit(
            employee_id,
            old_salary,
            new_salary
        )
        VALUES(
            OLD.employee_id,
            OLD.salary,
            NEW.salary
        );

    END IF;

    RETURN NEW;
END;
$$;

CREATE TRIGGER salary_update_trigger
AFTER UPDATE
ON employees
FOR EACH ROW
EXECUTE FUNCTION track_salary_change();

UPDATE employees set salary = 5000000 where employee_id = 1;

select * from salary_audit;


