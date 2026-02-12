set search_path = json;
CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    data JSONB
);
INSERT INTO products (data)
VALUES
    ('{
        "name": "iPhone 15 Pro",
        "category": "Electronics",
        "description": "The latest iPhone with advanced features.",
        "brand": "Apple",
        "price": 999.99,
        "attributes": {
            "color": "Graphite",
            "storage": "256GB",
            "display": "6.1-inch Super Retina XDR display",
            "processor": "A15 Bionic chip"
        },
        "tags": ["smartphone", "iOS", "Apple"]
    }'),
    ('{
        "name": "Samsung Galaxy Watch 4",
        "category": "Electronics",
        "description": "A smartwatch with health tracking and stylish design.",
        "brand": "Samsung",
        "price": 349.99,
        "attributes": {
            "color": "Black",
            "size": "42mm",
            "display": "AMOLED display",
            "sensors": ["heart rate monitor", "ECG", "SpO2"]
        },
        "tags": ["smartwatch", "wearable", "Samsung"]
    }'),
    ('{
        "name": "Leather Case for iPhone 15 Pro",
        "category": "Accessories",
        "description": "Premium leather case for iPhone 15 Pro.",
        "brand": "Apple",
        "price": 69.99,
        "attributes": {
            "color": "Saddle Brown",
            "material": "Genuine leather",
            "compatible_devices": ["iPhone 15 Pro", "iPhone 15 Pro Max"]
        },
        "tags": ["phone case", "accessory", "Apple"]
    }'),
    ('{
        "name": "Wireless Charging Pad",
        "category": "Accessories",
        "description": "Fast wireless charger compatible with smartphones and smartwatches.",
        "brand": "Anker",
        "price": 29.99,
        "attributes": {
            "color": "White",
            "compatible_devices": ["iPhone", "Samsung Galaxy", "Apple Watch", "Samsung Galaxy Watch"]
        },
        "tags": ["accessory", "wireless charger"]
    }')
RETURNING *;


-- ->
SELECT
  data -> 'name' AS product_name
FROM
  products;

 -- ->>
 SELECT
  data ->> 'name' AS product_name
FROM
  products;

 -- #>
 SELECT data -> 'attributes' -> 'color' as COLOR FROM products;
 
 SELECT data #> '{attributes,color}' as COLOR FROM products;

 -- #>>.

  SELECT data -> 'attributes' ->> 'color' as COLOR FROM products;
 
 SELECT data #>> '{attributes,color}' as COLOR FROM products;


 -- @>
 SELECT
  id,
  data ->> 'name' product_name
FROM
  products
WHERE
  data @> '{"category": "Electronics"}';

 -- <@

 SELECT
  data ->> 'name' name,
  data ->> 'price' price
FROM
  products
WHERE
  '{"price": 999.99}' :: jsonb <@ data;

 -- || 
 SELECT
  '{"name": "iPad"}' :: jsonb ||
   '{"price": 799}' :: jsonb
AS product;

-- ?
SELECT
  id,
  data ->> 'name' product_name,
  data ->> 'price' price
FROM
  products
WHERE
  data ? 'price';



 SELECT
  data ->> 'name' product_name,
  data ->> 'tags' tags
FROM
  products
WHERE
  data-> 'tags' ? 'Apple'

  -- ?|
  SELECT
  data ->> 'name' product_name,
  data ->> 'attributes' attributes
FROM
  products
WHERE
  data -> 'attributes' ?| array ['storage', 'size'];

  -- ?&
  SELECT
  data ->> 'name' product_name,
  data ->> 'attributes' attributes
FROM
  products
WHERE
  data -> 'attributes' ?& array ['color', 'storage'];


  -- -
  SELECT
  '{"name": "John Doe", "age": 22}' :: jsonb - 'name' result;

  SELECT
  '{"name": "John Doe", "age": 22, "email": "john.doe@example.com"}' :: jsonb - ARRAY[ 'age',
  'email' ] result;


-- @> similar to exists
  SELECT
  data ->> 'name' product_name,
  data ->> 'price' price
FROM
  products
WHERE
  data @? '$.price ? (@ > 999)';

  -- @@
  SELECT
  data ->> 'name' product_name,
  data ->> 'price' price
FROM
  products
WHERE
  data @@ '$.price > 999';