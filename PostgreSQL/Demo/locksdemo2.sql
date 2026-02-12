set search_path = locksdemo;
-- update locak demo
UPDATE locksdemo.accounts
SET balance = balance + 300
WHERE id = 1;

-- select lock demo
UPDATE accounts
SET balance = 0
WHERE id = 2;


--deadlocak demo
BEGIN;
UPDATE accounts SET balance = balance - 100 WHERE id = 2;

UPDATE accounts SET balance = balance - 100 WHERE id = 1;


