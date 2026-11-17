#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 \
    --username "$POSTGRES_USER" \
    --dbname "$POSTGRES_DB" \
    -v reader_password="$SQLI_READER_PASSWORD" <<'SQL'

CREATE TABLE "Products" (
    "Id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    "Name" VARCHAR(150) NOT NULL,
    "Price" NUMERIC(10, 2) NOT NULL
);

INSERT INTO "Products" ("Name", "Price")
VALUES
    ('Laptop', 3499.00),
    ('Keyboard', 249.00),
    ('Mouse', 129.00),
    ('Monitor', 1199.00),
    ('Headphones', 399.00);

CREATE ROLE sqli_reader
    LOGIN PASSWORD :'reader_password';

GRANT CONNECT ON DATABASE cyberlab_sqli TO sqli_reader;
GRANT USAGE ON SCHEMA public TO sqli_reader;
GRANT SELECT ON TABLE "Products" TO sqli_reader;

SQL
