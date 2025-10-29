-- Migration script to create DashboardMetrics table
-- This creates a dedicated table for storing dashboard metrics/counts

-- Create dashboardmetrics table
CREATE TABLE dashboardmetrics (
    dashboardmetricsid SERIAL PRIMARY KEY,
    entitytype VARCHAR(50) NOT NULL,
    entityid INTEGER,
    
    -- Count fields
    farmercount INTEGER DEFAULT 0,
    farmcount INTEGER DEFAULT 0,
    cropregistrycount INTEGER DEFAULT 0,
    livestockregistrycount INTEGER DEFAULT 0,
    
    -- Additional metrics
    totalareaplanted DECIMAL(18,2) DEFAULT 0,
    totalareaharvested DECIMAL(18,2) DEFAULT 0,
    totalyieldquantity DECIMAL(18,2) DEFAULT 0,
    totalplantedquantity DECIMAL(18,2) DEFAULT 0,
    totallivestockquantity INTEGER DEFAULT 0,
    
    -- Metadata
    lastcalculated TIMESTAMP DEFAULT NOW(),
    lastupdated TIMESTAMP DEFAULT NOW(),
    notes TEXT,
    
    -- Base entity fields
    createdat TIMESTAMP DEFAULT NOW(),
    updatedat TIMESTAMP DEFAULT NOW(),
    deletedat TIMESTAMP NULL
);

-- Create indexes for efficient queries
CREATE INDEX idx_dashboardmetrics_entity ON dashboardmetrics (entitytype, entityid);
CREATE INDEX idx_dashboardmetrics_entitytype ON dashboardmetrics (entitytype);
CREATE INDEX idx_dashboardmetrics_entityid ON dashboardmetrics (entityid);

-- Add foreign key constraints (optional, for referential integrity)
-- Note: These are optional and can be commented out if you prefer not to have FK constraints

-- Foreign key to useraccount table
ALTER TABLE dashboardmetrics 
ADD CONSTRAINT dashboardmetrics_userid_fkey 
FOREIGN KEY (entityid) REFERENCES useraccount(userid) 
ON DELETE CASCADE;

-- Foreign key to farmer table
ALTER TABLE dashboardmetrics 
ADD CONSTRAINT dashboardmetrics_farmerid_fkey 
FOREIGN KEY (entityid) REFERENCES farmer(farmerid) 
ON DELETE CASCADE;

-- Foreign key to farm table
ALTER TABLE dashboardmetrics 
ADD CONSTRAINT dashboardmetrics_farmid_fkey 
FOREIGN KEY (entityid) REFERENCES farm(farmid) 
ON DELETE CASCADE;

-- Add comments to document the table and fields
COMMENT ON TABLE dashboardmetrics IS 'Dedicated table for storing dashboard metrics and counts';
COMMENT ON COLUMN dashboardmetrics.entitytype IS 'Type of entity: User, Farmer, Farm, or Global';
COMMENT ON COLUMN dashboardmetrics.entityid IS 'ID of the entity (null for Global metrics)';
COMMENT ON COLUMN dashboardmetrics.farmercount IS 'Number of farmers';
COMMENT ON COLUMN dashboardmetrics.farmcount IS 'Number of farms';
COMMENT ON COLUMN dashboardmetrics.cropregistrycount IS 'Number of crop registrations';
COMMENT ON COLUMN dashboardmetrics.livestockregistrycount IS 'Number of livestock registrations';
COMMENT ON COLUMN dashboardmetrics.totalareaplanted IS 'Total area planted';
COMMENT ON COLUMN dashboardmetrics.totalareaharvested IS 'Total area harvested';
COMMENT ON COLUMN dashboardmetrics.totalyieldquantity IS 'Total yield quantity';
COMMENT ON COLUMN dashboardmetrics.totalplantedquantity IS 'Total planted quantity';
COMMENT ON COLUMN dashboardmetrics.totallivestockquantity IS 'Total livestock quantity';
COMMENT ON COLUMN dashboardmetrics.lastcalculated IS 'When the metrics were last calculated';
COMMENT ON COLUMN dashboardmetrics.lastupdated IS 'When the metrics were last updated';

-- Insert initial global metrics record
INSERT INTO dashboardmetrics (entitytype, entityid, farmercount, farmcount, cropregistrycount, livestockregistrycount)
VALUES ('Global', NULL, 0, 0, 0, 0);

-- Create a function to initialize metrics for existing entities
CREATE OR REPLACE FUNCTION initialize_dashboard_metrics()
RETURNS VOID AS $$
BEGIN
    -- Initialize metrics for all users
    INSERT INTO dashboardmetrics (entitytype, entityid, farmercount, farmcount, cropregistrycount, livestockregistrycount)
    SELECT 'User', u.userid, 0, 0, 0, 0
    FROM useraccount u
    WHERE u.deletedat IS NULL
    AND NOT EXISTS (
        SELECT 1 FROM dashboardmetrics dm 
        WHERE dm.entitytype = 'User' AND dm.entityid = u.userid
    );
    
    -- Initialize metrics for all farmers
    INSERT INTO dashboardmetrics (entitytype, entityid, farmercount, farmcount, cropregistrycount, livestockregistrycount)
    SELECT 'Farmer', f.farmerid, 0, 0, 0, 0
    FROM farmer f
    WHERE f.deletedat IS NULL
    AND NOT EXISTS (
        SELECT 1 FROM dashboardmetrics dm 
        WHERE dm.entitytype = 'Farmer' AND dm.entityid = f.farmerid
    );
    
    -- Initialize metrics for all farms
    INSERT INTO dashboardmetrics (entitytype, entityid, farmercount, farmcount, cropregistrycount, livestockregistrycount)
    SELECT 'Farm', f.farmid, 0, 0, 0, 0
    FROM farm f
    WHERE f.deletedat IS NULL
    AND NOT EXISTS (
        SELECT 1 FROM dashboardmetrics dm 
        WHERE dm.entitytype = 'Farm' AND dm.entityid = f.farmid
    );
END;
$$ LANGUAGE plpgsql;

-- Execute the function to initialize metrics for existing entities
SELECT initialize_dashboard_metrics();

-- Drop the function as it's no longer needed
DROP FUNCTION initialize_dashboard_metrics();
