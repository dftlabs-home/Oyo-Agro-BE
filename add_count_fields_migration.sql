-- Migration script to add count fields to existing tables
-- Run this script to add dashboard count tracking fields

-- Add count fields to useraccount table
ALTER TABLE useraccount 
ADD COLUMN farmercount INTEGER DEFAULT 0,
ADD COLUMN farmcount INTEGER DEFAULT 0,
ADD COLUMN cropregistrycount INTEGER DEFAULT 0,
ADD COLUMN livestockregistrycount INTEGER DEFAULT 0;

-- Add count fields to farmer table
ALTER TABLE farmer 
ADD COLUMN farmcount INTEGER DEFAULT 0,
ADD COLUMN cropregistrycount INTEGER DEFAULT 0,
ADD COLUMN livestockregistrycount INTEGER DEFAULT 0;

-- Add count fields to farm table
ALTER TABLE farm 
ADD COLUMN cropregistrycount INTEGER DEFAULT 0,
ADD COLUMN livestockregistrycount INTEGER DEFAULT 0;

-- Update existing records to have correct counts
-- This will calculate the actual counts for existing data

-- Update farmer counts in useraccount table
UPDATE useraccount 
SET farmercount = (
    SELECT COUNT(*) 
    FROM farmer 
    WHERE farmer.userid = useraccount.userid 
    AND farmer.deletedat IS NULL
);

-- Update farm counts in useraccount table
UPDATE useraccount 
SET farmcount = (
    SELECT COUNT(*) 
    FROM farm 
    INNER JOIN farmer ON farm.farmerid = farmer.farmerid
    WHERE farmer.userid = useraccount.userid 
    AND farm.deletedat IS NULL
);

-- Update crop registry counts in useraccount table
UPDATE useraccount 
SET cropregistrycount = (
    SELECT COUNT(*) 
    FROM cropregistry 
    INNER JOIN farm ON cropregistry.farmid = farm.farmid
    INNER JOIN farmer ON farm.farmerid = farmer.farmerid
    WHERE farmer.userid = useraccount.userid 
    AND cropregistry.deletedat IS NULL
);

-- Update livestock registry counts in useraccount table
UPDATE useraccount 
SET livestockregistrycount = (
    SELECT COUNT(*) 
    FROM livestockregistry 
    INNER JOIN farm ON livestockregistry.farmid = farm.farmid
    INNER JOIN farmer ON farm.farmerid = farmer.farmerid
    WHERE farmer.userid = useraccount.userid 
    AND livestockregistry.deletedat IS NULL
);

-- Update farm counts in farmer table
UPDATE farmer 
SET farmcount = (
    SELECT COUNT(*) 
    FROM farm 
    WHERE farm.farmerid = farmer.farmerid 
    AND farm.deletedat IS NULL
);

-- Update crop registry counts in farmer table
UPDATE farmer 
SET cropregistrycount = (
    SELECT COUNT(*) 
    FROM cropregistry 
    INNER JOIN farm ON cropregistry.farmid = farm.farmid
    WHERE farm.farmerid = farmer.farmerid 
    AND cropregistry.deletedat IS NULL
);

-- Update livestock registry counts in farmer table
UPDATE farmer 
SET livestockregistrycount = (
    SELECT COUNT(*) 
    FROM livestockregistry 
    INNER JOIN farm ON livestockregistry.farmid = farm.farmid
    WHERE farm.farmerid = farmer.farmerid 
    AND livestockregistry.deletedat IS NULL
);

-- Update crop registry counts in farm table
UPDATE farm 
SET cropregistrycount = (
    SELECT COUNT(*) 
    FROM cropregistry 
    WHERE cropregistry.farmid = farm.farmid 
    AND cropregistry.deletedat IS NULL
);

-- Update livestock registry counts in farm table
UPDATE farm 
SET livestockregistrycount = (
    SELECT COUNT(*) 
    FROM livestockregistry 
    WHERE livestockregistry.farmid = farm.farmid 
    AND livestockregistry.deletedat IS NULL
);

-- Add comments to document the new fields
COMMENT ON COLUMN useraccount.farmercount IS 'Number of farmers created by this user';
COMMENT ON COLUMN useraccount.farmcount IS 'Number of farms created by this user';
COMMENT ON COLUMN useraccount.cropregistrycount IS 'Number of crop registrations created by this user';
COMMENT ON COLUMN useraccount.livestockregistrycount IS 'Number of livestock registrations created by this user';

COMMENT ON COLUMN farmer.farmcount IS 'Number of farms owned by this farmer';
COMMENT ON COLUMN farmer.cropregistrycount IS 'Number of crop registrations for this farmer';
COMMENT ON COLUMN farmer.livestockregistrycount IS 'Number of livestock registrations for this farmer';

COMMENT ON COLUMN farm.cropregistrycount IS 'Number of crop registrations for this farm';
COMMENT ON COLUMN farm.livestockregistrycount IS 'Number of livestock registrations for this farm';
