USE INFORMATION_SCHEMA;
SHOW TABLES;
SELECT
    TABLE_NAME
FROM
    TABLES
WHERE
    TABLE_SCHEMA = 'studentdb';
    
    
SELECT
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH
FROM
    COLUMNS
WHERE
    TABLE_SCHEMA = 'studentdb' AND TABLE_NAME = 'students';
    
SELECT
    CONSTRAINT_NAME,
    TABLE_NAME,
    COLUMN_NAME,
    REFERENCED_TABLE_NAME,
    REFERENCED_COLUMN_NAME
FROM
    KEY_COLUMN_USAGE
WHERE
    REFERENCED_TABLE_SCHEMA = 'hoteldb';
    
    
    
    
SHOW VARIABLES LIKE 'performance_schema';

USE PERFORMANCE_SCHEMA;

SHOW TABLES LIKE '%summary%';

SELECT
    digest_text,
    count_star,
    avg_timer_wait
FROM
    events_statements_summary_by_digest
ORDER BY
    avg_timer_wait DESC
LIMIT 10;