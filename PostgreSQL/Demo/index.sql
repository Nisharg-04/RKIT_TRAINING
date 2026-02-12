-- INdexs
--B-Tree (DEFAULT — 90% of cases)
--Postgres automatically creates B-tree unless you specify otherwise.

--Best for:
--equality (=)
--ranges (< > BETWEEN)
--IN
--IS NULL
--prefix search (LIKE 'abc%')

CREATE INDEX idx_emp_email
ON employees(email);

/*
Hash Index

Used mainly for:

WHERE column = value


NOT for:
ranges
sorting
*/

/*
GIN (Generalized Inverted Index)
Search inside data

Best for:
JSONB
arrays
full-text search
*/

CREATE INDEX idx_skills
ON employees
USING GIN(skills);


-- used when query is like this
-- WHERE skills @> '{"postgres": true}'

/*
GiST
Used for:
geometric data
ranges
PostGIS
nearest neighbor searches
*/

--queruy like this
--Find restaurants within 5km

/*
BRIN (Block Range Index)
EXTREMELY underrated.
Instead of indexing every row…
It indexes block ranges.

Best for:
huge tables
time-series data
logs
append-only tables

Example:

logs table → billions of rows ordered by timestamp


BRIN = tiny index + very fast filtering.
*/



-- Covering index 


--Indexes normally do NOT store full row data.

--They store:

--indexed column + pointer to table row
/*
So database must:

Find value in index
Jump to table (heap fetch)

That jump is expensive.

A covering index contains ALL columns needed by the query.

So database never touches the table.

This is called:

Index Only Scan
Example Table
employees
-----------
id
email
first_name
last_name
salary


Query:
SELECT first_name, last_name
FROM employees
WHERE email = 'jay@gmail.com';

Normal Index
CREATE INDEX idx_email
ON employees(email);


Execution:
find email in index
jump to table
fetch names



Covering Index (PostgreSQL syntax)

CREATE INDEX idx_email_cover
ON employees(email)
INCLUDE(first_name, last_name);


Now index stores:
email | first_name | last_name


Database never reads table
Classic Production Example
Login system:
SELECT id, password_hash
FROM users
WHERE email = ?
*/



/*
PARTIAL INDEX
Concept:
Index ONLY a subset of rows.
Instead of indexing the entire table.

Example Table
orders
-----------
id
customer_id
status
created_at


Statuses:

completed

pending

cancelled


Typical Query:
SELECT *
FROM orders
WHERE status = 'pending';


But maybe:
95% orders are completed.

Why index them?
Waste of space.

Partial Index:
CREATE INDEX idx_pending_orders
ON orders(customer_id)
WHERE status = 'pending';


Now index contains ONLY pending rows.

Tiny index.

SUPER fast.

*/





CREATE DATABASE ncs_index_training;

CREATE TABLE users (
    user_id BIGSERIAL PRIMARY KEY,
    email TEXT NOT NULL,
    full_name TEXT,
    country TEXT,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE products (
    product_id BIGSERIAL PRIMARY KEY,
    name TEXT,
    category TEXT,
    price NUMERIC(10,2),
    stock INT,
    metadata JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE orders (
    order_id BIGSERIAL PRIMARY KEY,
    user_id BIGINT REFERENCES users(user_id),
    status TEXT,
    total_amount NUMERIC(12,2),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE order_items (
    order_item_id BIGSERIAL PRIMARY KEY,
    order_id BIGINT REFERENCES orders(order_id),
    product_id BIGINT REFERENCES products(product_id),
    quantity INT,
    price NUMERIC(10,2)
);
CREATE TABLE event_logs (
    event_id BIGSERIAL PRIMARY KEY,
    user_id BIGINT,
    event_type TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
INSERT INTO users (email, full_name, country, is_active)
SELECT
    'user' || g || '@mail.com',
    'User ' || g,
    (ARRAY['India','US','UK','Germany','Canada'])[floor(random()*5+1)],
    random() > 0.2
FROM generate_series(1,100000) g;


INSERT INTO products (name, category, price, stock, metadata)
SELECT
    'Product ' || g,
    (ARRAY['Electronics','Clothing','Books','Home'])[floor(random()*4+1)],
    round((random()*1000)::numeric,2),
    (random()*500)::int,
    jsonb_build_object('brand', 'Brand ' || (g % 50))
FROM generate_series(1,20000) g;

INSERT INTO orders (user_id, status, total_amount, created_at)
SELECT
    u.user_id,
    (ARRAY['completed','pending','cancelled'])[floor(random()*3+1)],
    round((random()*5000)::numeric,2),
    NOW() - (random()*interval '365 days')
FROM users u
JOIN generate_series(1,5) g ON true;


INSERT INTO order_items (order_id, product_id, quantity, price)
SELECT
    o.order_id,
    floor(random()*20000 + 1)::int,
    floor(random()*5 + 1)::int,
    round((random()*1000)::numeric,2)
FROM orders o
JOIN generate_series(1,2) g ON true;


INSERT INTO event_logs (user_id, event_type, created_at)
SELECT
    u.user_id,
    (ARRAY['login','logout','purchase','view'])[floor(random()*4+1)],
    NOW() - (random()*interval '365 days')
FROM users u
JOIN generate_series(1,10) g ON true;

SELECT COUNT(*) FROM users; -- 1 lakh
SELECT COUNT(*) FROM products; -- 20 k
SELECT COUNT(*) FROM orders;-- 5 lakh
SELECT COUNT(*) FROM order_items; -- 10 lakh
SELECT COUNT(*) FROM event_logs; -- 10 lakh

	
EXPLAIN ANALYZE
SELECT *
FROM users
WHERE email = 'user50000@mail.com';
/*
"Seq Scan on users  (cost=0.00..2278.00 rows=1 width=50) (actual time=5.391..10.997 rows=1.00 loops=1)"
"  Filter: (email = 'user50000@mail.com'::text)"
"  Rows Removed by Filter: 99999"
"  Buffers: shared hit=1028"
"Planning Time: 0.095 ms"
"Execution Time: 11.020 ms"
*/
CREATE INDEX idx_users_email
ON users(email);

EXPLAIN 
SELECT *
FROM users
WHERE email = 'user50000@mail.com';
"Index Scan using idx_users_email on users  (cost=0.42..8.44 rows=1 width=50)"
"  Index Cond: (email = 'user50000@mail.com'::text)"

/*
"Index Scan using idx_users_email on users  (cost=0.42..8.44 rows=1 width=50) (actual time=0.059..0.060 rows=1.00 loops=1)"
"  Index Cond: (email = 'user50000@mail.com'::text)"
"  Index Searches: 1"
"  Buffers: shared hit=1 read=3"
"Planning:"
"  Buffers: shared hit=16 read=1"
"Planning Time: 1.595 ms"
"Execution Time: 0.077 ms"
*/


EXPLAIN ANALYZE
SELECT *
FROM users
WHERE is_active = true;
/*
"Seq Scan on users  (cost=0.00..2028.00 rows=80557 width=50) (actual time=0.023..9.551 rows=80264.00 loops=1)"
"  Filter: is_active"
"  Rows Removed by Filter: 19736"
"  Buffers: shared hit=1028"
"Planning Time: 0.103 ms"
"Execution Time: 12.170 ms"
*/

EXPLAIN ANALYZE
SELECT *
FROM users
WHERE is_active = false;
/*
"Seq Scan on users  (cost=0.00..2028.00 rows=19443 width=50) (actual time=0.018..10.205 rows=19736.00 loops=1)"
"  Filter: (NOT is_active)"
"  Rows Removed by Filter: 80264"
"  Buffers: shared hit=1028"
"Planning Time: 0.106 ms"
"Execution Time: 10.994 ms"
*/


--80 % active 20% inactive so if i create index also then also it will not work
CREATE INDEX idx_users_active
ON users(is_active);

EXPLAIN ANALYZE
SELECT *
FROM users
WHERE is_active = true;
/*
"Seq Scan on users  (cost=0.00..2028.00 rows=80557 width=50) (actual time=0.015..10.942 rows=80264.00 loops=1)"
"  Filter: is_active"
"  Rows Removed by Filter: 19736"
"  Buffers: shared hit=1028"
"Planning:"
"  Buffers: shared hit=16 read=1"
"Planning Time: 1.366 ms"
"Execution Time: 13.726 ms"
*/

EXPLAIN ANALYZE
SELECT *
FROM users
WHERE is_active = false;

/*
"Bitmap Heap Scan on users  (cost=218.98..1441.41 rows=19443 width=50) (actual time=0.881..5.167 rows=19736.00 loops=1)"
"  Recheck Cond: (NOT is_active)"
"  Heap Blocks: exact=1028"
"  Buffers: shared hit=1028 read=18"
"  ->  Bitmap Index Scan on idx_users_active  (cost=0.00..214.11 rows=19443 width=0) (actual time=0.759..0.759 rows=19736.00 loops=1)"
"        Index Cond: (is_active = false)"
"        Index Searches: 1"
"        Buffers: shared read=18"
"Planning Time: 0.093 ms"
"Execution Time: 5.818 ms"
*/

--partial index
CREATE INDEX idx_users_inactive
ON users(user_id)
WHERE is_active = false;

EXPLAIN ANALYZE
SELECT *
FROM users
WHERE is_active = false;
/*
"Index Scan using idx_users_inactive on users  (cost=0.29..718.93 rows=19443 width=50) (actual time=0.057..7.748 rows=19736.00 loops=1)"
"  Index Searches: 1"
"  Buffers: shared hit=1028 read=55"
"Planning:"
"  Buffers: shared hit=16 read=1"
"Planning Time: 1.844 ms"
"Execution Time: 8.736 ms"
*/

EXPLAIN ANALYZE
SELECT *
FROM users
WHERE is_active = true;
/*
"Seq Scan on users  (cost=0.00..2028.00 rows=80557 width=50) (actual time=0.021..9.985 rows=80264.00 loops=1)"
"  Filter: is_active"
"  Rows Removed by Filter: 19736"
"  Buffers: shared hit=1028"
"Planning Time: 0.131 ms"
"Execution Time: 12.721 ms"
*/


EXPLAIN ANALYZE
SELECT *
FROM orders
WHERE user_id = 500
ORDER BY created_at DESC;
/*
"Sort  (cost=8106.72..8106.74 rows=5 width=39) (actual time=56.772..63.483 rows=5.00 loops=1)"
"  Sort Key: created_at DESC"
"  Sort Method: quicksort  Memory: 25kB"
"  Buffers: shared hit=1871 read=2634"
"  ->  Gather  (cost=1000.00..8106.67 rows=5 width=39) (actual time=1.219..63.422 rows=5.00 loops=1)"
"        Workers Planned: 2"
"        Workers Launched: 2"
"        Buffers: shared hit=1868 read=2634"
"        ->  Parallel Seq Scan on orders  (cost=0.00..7106.17 rows=2 width=39) (actual time=2.616..21.052 rows=1.67 loops=3)"
"              Filter: (user_id = 500)"
"              Rows Removed by Filter: 166665"
"              Buffers: shared hit=1868 read=2634"
"Planning:"
"  Buffers: shared hit=28 read=2 dirtied=1"
"Planning Time: 0.464 ms"
"Execution Time: 63.537 ms"
*/

CREATE INDEX idx_orders_user_created
ON orders(user_id, created_at DESC);


EXPLAIN ANALYZE
SELECT *
FROM orders
WHERE user_id = 500
ORDER BY created_at DESC;

/*
"Index Scan using idx_orders_user_created on orders  (cost=0.42..24.15 rows=5 width=39) (actual time=0.167..0.175 rows=5.00 loops=1)"
"  Index Cond: (user_id = 500)"
"  Index Searches: 1"
"  Buffers: shared hit=7 read=4"
"Planning:"
"  Buffers: shared hit=18 read=1"
"Planning Time: 1.545 ms"
"Execution Time: 0.206 ms"
*/


EXPLAIN ANALYZE
SELECT user_id,order_id
FROM orders
WHERE user_id = 500;
/*
"Index Scan using idx_orders_user_created on orders  (cost=0.42..24.15 rows=5 width=16) (actual time=0.038..0.049 rows=5.00 loops=1)"
"  Index Cond: (user_id = 500)"
"  Index Searches: 1"
"  Buffers: shared hit=8"
"Planning Time: 0.140 ms"
"Execution Time: 0.096 ms"
*/

CREATE INDEX idx_orders_covering
ON orders(user_id)
INCLUDE(order_id);

EXPLAIN ANALYZE
SELECT user_id,order_id
FROM orders
WHERE user_id = 500;
/*
"Index Only Scan using idx_orders_covering on orders  (cost=0.42..4.51 rows=5 width=16) (actual time=0.033..0.035 rows=5.00 loops=1)"
"  Index Cond: (user_id = 500)"
"  Heap Fetches: 0"
"  Index Searches: 1"
"  Buffers: shared hit=4"
"Planning Time: 0.196 ms"
"Execution Time: 0.061 ms"
*/

EXPLAIN ANALYZE
SELECT *
FROM products
WHERE metadata @> '{"brand":"Brand 10"}';

/*
"Seq Scan on products  (cost=0.00..519.00 rows=400 width=72) (actual time=0.029..14.527 rows=400.00 loops=1)"
"  Filter: (metadata @> '{""brand"": ""Brand 10""}'::jsonb)"
"  Rows Removed by Filter: 19600"
"  Buffers: shared hit=269"
"Planning Time: 0.128 ms"
"Execution Time: 14.580 ms"
*/


CREATE INDEX idx_products_metadata
ON products
USING GIN(metadata);

EXPLAIN ANALYZE
SELECT *
FROM products
WHERE metadata @> '{"brand":"Brand 10"}';
/*
"Bitmap Heap Scan on products  (cost=15.04..302.02 rows=400 width=72) (actual time=0.491..1.089 rows=400.00 loops=1)"
"  Recheck Cond: (metadata @> '{""brand"": ""Brand 10""}'::jsonb)"
"  Heap Blocks: exact=268"
"  Buffers: shared hit=279"
"  ->  Bitmap Index Scan on idx_products_metadata  (cost=0.00..14.94 rows=400 width=0) (actual time=0.432..0.432 rows=400.00 loops=1)"
"        Index Cond: (metadata @> '{""brand"": ""Brand 10""}'::jsonb)"
"        Index Searches: 1"
"        Buffers: shared hit=11"
"Planning:"
"  Buffers: shared hit=22 read=1"
"Planning Time: 1.469 ms"
"Execution Time: 1.152 ms"
*/


EXPLAIN ANALYZE
SELECT *
FROM event_logs
WHERE created_at > NOW() - interval '7 days';
/*
"Gather  (cost=1000.00..17799.47 rows=18898 width=30) (actual time=0.548..209.619 rows=18780.00 loops=1)"
"  Workers Planned: 2"
"  Workers Launched: 2"
"  Buffers: shared hit=7264 read=354"
"  ->  Parallel Seq Scan on event_logs  (cost=0.00..14909.67 rows=7874 width=30) (actual time=0.056..164.264 rows=6260.00 loops=3)"
"        Filter: (created_at > (now() - '7 days'::interval))"
"        Rows Removed by Filter: 327073"
"        Buffers: shared hit=7264 read=354"
"Planning:"
"  Buffers: shared hit=7"
"Planning Time: 0.161 ms"
"Execution Time: 210.636 ms"
*/

CREATE INDEX idx_event_logs_brin
ON event_logs
USING BRIN(created_at);

EXPLAIN ANALYZE
SELECT *
FROM event_logs
WHERE created_at > NOW() - interval '1 day';

SELECT correlation
FROM pg_stats
WHERE tablename = 'event_logs'
AND attname = 'created_at';

CREATE TABLE event_logs_sorted AS
SELECT *
FROM event_logs
ORDER BY created_at;


EXPLAIN ANALYZE
SELECT *
FROM event_logs_sorted
WHERE created_at > NOW() - interval '7 day';
/*
"Gather  (cost=1000.00..17812.87 rows=18412 width=30) (actual time=326.365..365.750 rows=18759.00 loops=1)"
"  Workers Planned: 2"
"  Workers Launched: 2"
"  Buffers: shared hit=2774 read=4906"
"  ->  Parallel Seq Scan on event_logs_sorted  (cost=0.00..14971.67 rows=7672 width=30) (actual time=270.636..281.334 rows=6253.00 loops=3)"
"        Filter: (created_at > (now() - '7 days'::interval))"
"        Rows Removed by Filter: 327080"
"        Buffers: shared hit=2774 read=4906"
"Planning Time: 0.157 ms"
"Execution Time: 366.846 ms"
*/

CREATE INDEX idx_event_logs_brin_sorted
ON event_logs_sorted
USING BRIN(created_at);


SELECT correlation
FROM pg_stats
WHERE tablename = 'event_logs_sorted'
AND attname = 'created_at';




EXPLAIN ANALYZE
SELECT *
FROM event_logs_sorted
WHERE created_at > NOW() - interval '7 day';
/*
"Bitmap Heap Scan on event_logs_sorted  (cost=16.67..8280.00 rows=18411 width=30) (actual time=3.565..13.519 rows=18757.00 loops=1)"
"  Recheck Cond: (created_at > (now() - '7 days'::interval))"
"  Rows Removed by Index Recheck: 6542"
"  Heap Blocks: lossy=256"
"  Buffers: shared hit=261"
"  ->  Bitmap Index Scan on idx_event_logs_brin_sorted  (cost=0.00..12.07 rows=33333 width=0) (actual time=0.105..0.105 rows=2560.00 loops=1)"
"        Index Cond: (created_at > (now() - '7 days'::interval))"
"        Index Searches: 1"
"        Buffers: shared hit=5"
"Planning:"
"  Buffers: shared hit=16"
"Planning Time: 1.564 ms"
"Execution Time: 14.471 ms"
*/


-- full text search

-- there are two nbasic ts_vector -> it is datatype that stores lexemes 
-- tsquery -> actual word that neeed tib searched

SELECT to_tsvector('The quick brown fox jumps over the lazy dog.');

SELECT to_tsquery('jumping');
-- The match operator (@@)
-- tsvector @@ tsquery

SELECT
  to_tsvector(
    'The quick brown fox jumps over the lazy dog.'
  ) @@ to_tsquery('jumping') ;	



CREATE TABLE articles (
    id BIGSERIAL PRIMARY KEY,
    title TEXT,
    content TEXT,
    author TEXT,
    created_at TIMESTAMP DEFAULT now()
);

	INSERT INTO articles (title, content, author)
	SELECT
	    'PostgreSQL Guide ' || g,
	    
	    CASE 
	        WHEN random() < 0.3 THEN
	            'PostgreSQL is a powerful relational database used for high performance applications and scalable systems'
	        WHEN random() < 0.6 THEN
	            'Learn database indexing strategies including btree gin and brin for faster queries'
	        ELSE
	            'Modern backend systems require efficient search capabilities and optimized queries'
	    END,
	    
	    'Author ' || (g % 50)
	FROM generate_series(1,200000) g;

	EXPLAIN ANALYZE
SELECT *
FROM articles
WHERE content ILIKE '%database%';
/*
"Gather  (cost=1000.00..6509.01 rows=25 width=112) (actual time=0.497..216.482 rows=144107.00 loops=1)"
"  Workers Planned: 2"
"  Workers Launched: 2"
"  Buffers: shared hit=4229"
"  ->  Parallel Seq Scan on articles  (cost=0.00..5506.51 rows=10 width=112) (actual time=0.066..158.009 rows=48035.67 loops=3)"
"        Filter: (content ~~* '%database%'::text)"
"        Rows Removed by Filter: 18631"
"        Buffers: shared hit=4229"
"Planning:"
"  Buffers: shared hit=2"
"Planning Time: 0.107 ms"
"Execution Time: 224.143 ms"
*/

ALTER TABLE articles
ADD COLUMN search_vector tsvector ;


UPDATE articles
SET search_vector =
    to_tsvector('english', title || ' ' || content);

CREATE INDEX idx_articles_fts
ON articles
USING GIN(search_vector);


EXPLAIN ANALYZE
SELECT *
FROM articles
WHERE search_vector @@ to_tsquery('database');

/*
"Gather  (cost=1987.84..50278.84 rows=144000 width=322) (actual time=24.723..109.284 rows=144107.00 loops=1)"
"  Workers Planned: 2"
"  Workers Launched: 2"
"  Buffers: shared hit=9141 read=1"
"  ->  Parallel Bitmap Heap Scan on articles  (cost=987.84..34878.84 rows=60000 width=322) (actual time=8.790..39.774 rows=48035.67 loops=3)"
"        Recheck Cond: (search_vector @@ to_tsquery('database'::text))"
"        Heap Blocks: exact=5997"
"        Buffers: shared hit=9141 read=1"
"        Worker 0:  Heap Blocks: exact=1562"
"        Worker 1:  Heap Blocks: exact=1509"
"        ->  Bitmap Index Scan on idx_articles_fts  (cost=0.00..951.84 rows=144000 width=0) (actual time=22.290..22.291 rows=144107.00 loops=1)"
"              Index Cond: (search_vector @@ to_tsquery('database'::text))"
"              Index Searches: 1"
"              Buffers: shared hit=34"
"Planning:"
"  Buffers: shared hit=34 read=1"
"Planning Time: 1.892 ms"
"Execution Time: 116.824 ms"
*/





