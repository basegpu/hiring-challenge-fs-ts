-- db schema
CREATE TABLE IF NOT EXISTS assets (
    id SERIAL PRIMARY KEY,
    description TEXT NOT NULL,
    latitude FLOAT NOT NULL,
    longitude FLOAT NOT NULL
);
CREATE TABLE IF NOT EXISTS signals (
    id SERIAL PRIMARY KEY,
    guid UUID NOT NULL,
    asset_id SERIAL NOT NULL,
    name VARCHAR(255) NOT NULL,
    unit VARCHAR(255) NOT NULL,
    FOREIGN KEY (asset_id) REFERENCES assets(id)
);
CREATE TABLE IF NOT EXISTS measurements (
    signal_id INT NOT NULL,
    timestamp TIMESTAMP NOT NULL,
    value FLOAT NOT NULL,
    FOREIGN KEY (signal_id) REFERENCES signals(id)
);

-- init assets table
INSERT INTO assets (id, description, latitude, longitude)
SELECT 
    (item->>'AssetID')::INTEGER AS id,
    item->>'descri' AS description,
    (item->>'Latitude')::FLOAT AS latitude,
    (item->>'Longitude')::FLOAT AS longitude
FROM
    jsonb_array_elements(pg_read_file('/tmp/assets.json')::jsonb) AS item;
-- init signals table
INSERT INTO signals (id, guid, asset_id, name, unit)
SELECT 
    (item->>'SignalId')::INTEGER AS id,
    (item->>'SignalGId')::UUID AS guid,
    (item->>'AssetId')::INTEGER AS asset_id,
    item->>'SignalName' AS name,
    item->>'Unit' AS unit
FROM
    jsonb_array_elements(pg_read_file('/tmp/signals.json')::jsonb) AS item;
-- init measurements table
CREATE TABLE staging_data (
    timestamp TIMESTAMP,
    signal_id INTEGER,
    value TEXT
);
COPY staging_data FROM '/tmp/measurements.csv'WITH (FORMAT csv, DELIMITER '|', HEADER true);
INSERT INTO measurements (timestamp, signal_id, value)
SELECT 
    timestamp,
    signal_id,
    REPLACE(value, ',', '.')::NUMERIC
FROM staging_data;
DROP TABLE staging_data;