set search_path = windowfunctions;
CREATE TABLE employees (
    emp_id INT PRIMARY KEY,
    emp_name VARCHAR(50),
    dept_name VARCHAR(50),
    salary INT
);

INSERT INTO employees VALUES
(1, 'Amit', 'IT', 90000),
(2, 'Neha', 'HR', 60000),
(3, 'Raj', 'Finance', 85000),
(4, 'Priya', 'IT', 95000),
(5, 'Karan', 'Sales', 70000),
(6, 'Sneha', 'HR', 65000),
(7, 'Vikram', 'Finance', 80000),
(8, 'Riya', 'IT', 92000),
(9, 'Arjun', 'Sales', 72000),
(10, 'Meera', 'Finance', 88000),
(11, 'Yash', 'IT', 87000),
(12, 'Pooja', 'HR', 62000),
(13, 'Dev', 'Sales', 68000),
(14, 'Anjali', 'Finance', 91000),
(15, 'Rohit', 'IT', 89000),
(16, 'Kavya', 'HR', 64000),
(17, 'Harsh', 'Sales', 75000),
(18, 'Nisha', 'Finance', 83000),
(19, 'Manav', 'IT', 97000),
(20, 'Tina', 'Sales', 71000),
(21,'Ronak','IT',97000);


-- AGGREGRATE Functions

SELECT AVG(salary) AS avg_sal from employees;



SELECT *, AVG(salary) OVER() AS avg_sal from employees;


SELECT *, AVG(salary) OVER(partition by dept_name) AS dept_avg from employees;

SELECT *,SUM(salary) OVER () FROM employees;
SELECT *,SUM(salary) OVER (partition by dept_name) FROM employees;
SELECT *,SUM(salary) OVER (order by emp_id) FROM employees;
SELECT *,SUM(salary) OVER (partition by dept_name order by emp_id) FROM employees;

SELECT *,AVG(salary) OVER () FROM employees;
SELECT *,AVG(salary) OVER (partition by dept_name) FROM employees;
SELECT *,AVG(salary) OVER (order by salary) FROM employees;
SELECT *,AVG(salary) OVER (partition by dept_name order by salary) FROM employees;



-- department salary percentage
SELECT emp_id,emp_name,salary,dept_name,ROUND( salary *100 /SUM(salary) OVER (partition by dept_name) ,2)as dept_per from employees;


SELECT emp_name,salary,dept_name , 
	AVG(salary) OVER() AS avg_company_sal,
	AVG(salary) OVER (partition by dept_name) AS avg_dept_sal,
	AVG(salary) OVER (partition by dept_name order by salary) as running_avg_per_dept,
	SUM(salary) OVER() AS sum_company_sal,
	SUM(salary) OVER (partition by dept_name) AS sum_dept_sal,
	SUM(salary) OVER (order by salary) as running_sal,
	SUM(salary) OVER (partition by dept_name order by salary) as running_total_per_dept
FROM 
 employees;

select *,max(salary) over(partition by dept_name order by salary desc) from employees;




-- Ranking functions 

SELECT emp_name,
       dept_name,
       salary,
       ROW_NUMBER() OVER(
            PARTITION BY dept_name
            ORDER BY salary DESC
       ) AS row_num
FROM employees;

SELECT emp_name,
       salary,
       RANK() OVER(ORDER BY salary DESC)
FROM employees;


SELECT emp_name,
       salary,
       DENSE_RANK() OVER(ORDER BY salary DESC)
FROM employees;


--Strict Top N Per Department
WITH ranked AS (
    SELECT *,
           ROW_NUMBER() OVER(
               PARTITION BY dept_name
               ORDER BY salary DESC, emp_id
           ) AS  rn
    FROM employees
)
SELECT emp_name,dept_name,salary
FROM ranked
WHERE rn <= 3;


-- Highest Paid Employee Per Department
WITH ranked AS (
    SELECT *,
           RANK() OVER(
               PARTITION BY dept_name
               ORDER BY salary DESC
           ) AS  rn
    FROM employees
)
SELECT emp_name,dept_name,salary
FROM ranked
WHERE rn = 1;

-- Find the 2nd highest salary employee in each department.
WITH ranked AS (SELECT *, DENSE_RANK() OVER(partition by dept_name order by salary desc) as rn from employees)
SELECT emp_name, dept_name,salary from ranked where rn =2;

--Find the lowest paid employee per department.
WITH ranked AS (SELECT *, DENSE_RANK() OVER(partition by dept_name order by salary ) as rn from employees)
SELECT emp_name, dept_name,salary from ranked where rn =1;

-- value functions
/*
LAG(column, offset, default)
OVER(PARTITION BY ... ORDER BY ...)
*/

SELECT emp_id,
       dept_name,
       salary,
       LAG(salary,1,0) OVER(
           PARTITION BY dept_name
           ORDER BY salary
       ) AS prev_salary,
       salary - LAG(salary,1,0) OVER(
           PARTITION BY dept_name
           ORDER BY salary
       ) AS salary_diff
FROM employees;



SELECT emp_id,
       salary,
       LEAD(salary) OVER(
           PARTITION BY dept_name
           ORDER BY salary
       ) AS next_salary
FROM employees;

-- Find employees whose salary jump from previous is more than 20%.
SELECT *,
       LAG(salary,1,salary) OVER(ORDER BY salary) AS prev_salary,
       (salary - LAG(salary,1,salary) OVER(ORDER BY salary))
       * 100.0
       / LAG(salary,1,salary) OVER(ORDER BY salary) AS jump_percent
FROM employees;

-- Default Frame:
-- RANGE BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW

-- firstvalue
SELECT emp_name,
       dept_name,
       salary,
       FIRST_VALUE(salary) OVER(
           PARTITION BY dept_name
           ORDER BY salary DESC
       ) AS highest_salary
FROM employees;

-- LAST_VALUE()
-- basic expectation for the baove with the same result but in current window frame the last row is the 
-- current row it self as the window frame is default
SELECT emp_name,
       dept_name,
       salary,
       LAST_VALUE(salary) OVER(
           PARTITION BY dept_name
           ORDER BY salary 
       ) AS highest_salary
FROM employees;

SELECT emp_name,
       dept_name,
       salary,
      LAST_VALUE(salary) OVER(
    PARTITION BY dept_name
    ORDER BY salary
    ROWS BETWEEN UNBOUNDED PRECEDING
    AND UNBOUNDED FOLLOWING
)
 AS highest_salary
FROM employees;


SELECT
emp_name,
       salary,
       NTILE(4) OVER(ORDER BY salary) AS salary_band
FROM employees

with ranked as (SELECT
emp_name,
       salary,
       NTILE(4) OVER(ORDER BY salary) AS salary_band
FROM employees)
select salary_band,count(*) from ranked group by salary_band;



SELECT emp_name,
       dept_name,
       salary,
       NTH_VALUE(salary,2) OVER(
           PARTITION BY dept_name
           ORDER BY salary DESC
           ROWS BETWEEN UNBOUNDED PRECEDING 
           AND UNBOUNDED FOLLOWING -- if not written this then null for highest salary in particular dept this column will be null
       ) AS second_highest
FROM employees;


-- CUME_DIST()
-- (rows <= current) / total rows


SELECT emp_name,
       salary,
       CUME_DIST() OVER(ORDER BY salary) AS cume_dist
FROM employees;

-- top earners of the comapany
-- percentile filtering
SELECT *
FROM (
    SELECT *,
           CUME_DIST() OVER(ORDER BY salary) AS cd
    FROM employees
) t
WHERE cd >= 0.8;


-- percent rank
SELECT emp_name,
       salary,
       PERCENT_RANK() OVER(ORDER BY salary)
FROM employees;

-- Find employees in top 25%.

SELECT *
FROM (
    SELECT *,
           PERCENT_RANK() OVER(ORDER BY salary) pr
    FROM employees
) t
WHERE pr >= 0.75;


-- PERCENT_RANK:
-- How far am I from the lowest rank?
-- CUME_DIST:
-- How many people are below or equal to me?
