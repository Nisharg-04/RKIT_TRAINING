set search_path= locksdemo;
CREATE TABLE accounts (
    id INT PRIMARY KEY,
    name TEXT,
    balance INT
);

INSERT INTO accounts VALUES
(1, 'Alice', 1000),
(2, 'Bob', 2000);


-- update demo
BEGIN;

UPDATE accounts
SET balance = balance - 500	
WHERE id = 1;

Commit;

-- select demo

BEGIN;

SELECT *
FROM accounts
WHERE id = 2
FOR UPDATE;

commit;


--deadlock demo

BEGIN;
UPDATE accounts SET balance = balance - 100 WHERE id = 1;

UPDATE accounts SET balance = balance - 100 WHERE id = 2;




