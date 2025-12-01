-- Migration script to create DashboardMetrics table
-- This creates a table for storing transaction metrics with period tracking
-- Each metric tracks operations (CREATE, UPDATE, DELETE) with daily, weekly, and monthly periods

-- Create dashboardmetrics table
CREATE TABLE IF NOT EXISTS dashboardmetrics (
    "Metric_Id" SERIAL PRIMARY KEY,
    "Metric_Name" INTEGER NOT NULL,
    "Metric_Value" INTEGER NOT NULL DEFAULT 1,
    "Metric_MonthlyPeriod" INTEGER NOT NULL,
    "Metric_WeeklyPeriod" INTEGER NOT NULL,
    "Metric_DailyPeriod" INTEGER NOT NULL,
    "Metric_CreateDate" TIMESTAMP DEFAULT NOW(),
    "Metric_UpdatedDate" TIMESTAMP DEFAULT NOW(),
    "SysNumber" BIGINT,
    "CreatedByUserId" INTEGER,
    "RelatedFarmerId" INTEGER,
    "RelatedFarmId" BIGINT,
    "EntityId" BIGINT,
    createdat TIMESTAMP DEFAULT NOW(),
    updatedat TIMESTAMP DEFAULT NOW(),
    deletedat TIMESTAMP NULL
);

-- Create indexes for efficient queries
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_name ON dashboardmetrics("Metric_Name");
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_user ON dashboardmetrics("CreatedByUserId");
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_farmer ON dashboardmetrics("RelatedFarmerId");
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_farm ON dashboardmetrics("RelatedFarmId");
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_daily ON dashboardmetrics("Metric_DailyPeriod");
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_weekly ON dashboardmetrics("Metric_WeeklyPeriod");
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_monthly ON dashboardmetrics("Metric_MonthlyPeriod");
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_createdate ON dashboardmetrics("Metric_CreateDate" DESC);

-- Composite index for period-based queries
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_period ON dashboardmetrics("Metric_Name", "Metric_DailyPeriod", "Metric_WeeklyPeriod", "Metric_MonthlyPeriod");

-- Composite index for relationship queries
CREATE INDEX IF NOT EXISTS idx_dashboardmetrics_relationships ON dashboardmetrics("CreatedByUserId", "RelatedFarmerId", "RelatedFarmId");

-- Add foreign key constraints for referential integrity
-- Note: Drop constraints first if they exist (for re-running the script)

-- Foreign key to useraccount table
DO $$ 
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint WHERE conname = 'fk_dashboardmetrics_user'
    ) THEN
        ALTER TABLE dashboardmetrics 
        ADD CONSTRAINT fk_dashboardmetrics_user 
        FOREIGN KEY ("CreatedByUserId") REFERENCES useraccount(userid)
        ON DELETE SET NULL;
    END IF;
END $$;

-- Foreign key to farmer table
DO $$ 
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint WHERE conname = 'fk_dashboardmetrics_farmer'
    ) THEN
        ALTER TABLE dashboardmetrics 
        ADD CONSTRAINT fk_dashboardmetrics_farmer 
        FOREIGN KEY ("RelatedFarmerId") REFERENCES farmer(farmerid)
        ON DELETE SET NULL;
    END IF;
END $$;

-- Foreign key to farm table
DO $$ 
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint WHERE conname = 'fk_dashboardmetrics_farm'
    ) THEN
        ALTER TABLE dashboardmetrics 
        ADD CONSTRAINT fk_dashboardmetrics_farm 
        FOREIGN KEY ("RelatedFarmId") REFERENCES farm(farmid)
        ON DELETE SET NULL;
    END IF;
END $$;

-- Add comments to document the table and fields
COMMENT ON TABLE dashboardmetrics IS 'Table for storing transaction metrics with period tracking (daily, weekly, monthly)';
COMMENT ON COLUMN dashboardmetrics."Metric_Id" IS 'Primary key identifier for the metric record';
COMMENT ON COLUMN dashboardmetrics."Metric_Name" IS 'Enum value representing the operation type (e.g., FARM_CREATE, FARMER_UPDATE)';
COMMENT ON COLUMN dashboardmetrics."Metric_Value" IS 'Count value for this metric in the specified period';
COMMENT ON COLUMN dashboardmetrics."Metric_MonthlyPeriod" IS 'Period identifier: Year * 100 + Month (e.g., 202412 for December 2024)';
COMMENT ON COLUMN dashboardmetrics."Metric_WeeklyPeriod" IS 'Period identifier: Year * 100 + Week (e.g., 202450 for week 50 of 2024)';
COMMENT ON COLUMN dashboardmetrics."Metric_DailyPeriod" IS 'Period identifier: Year * 10000 + Month * 100 + Day (e.g., 20241225 for Dec 25, 2024)';
COMMENT ON COLUMN dashboardmetrics."Metric_CreateDate" IS 'When this metric record was first created';
COMMENT ON COLUMN dashboardmetrics."Metric_UpdatedDate" IS 'When this metric record was last updated';
COMMENT ON COLUMN dashboardmetrics."SysNumber" IS 'System number for tracking (Unix timestamp in milliseconds)';
COMMENT ON COLUMN dashboardmetrics."CreatedByUserId" IS 'ID of the user who performed the operation';
COMMENT ON COLUMN dashboardmetrics."RelatedFarmerId" IS 'ID of the farmer related to this operation (if applicable)';
COMMENT ON COLUMN dashboardmetrics."RelatedFarmId" IS 'ID of the farm related to this operation (if applicable)';
COMMENT ON COLUMN dashboardmetrics."EntityId" IS 'ID of the entity that was operated on (the actual record ID)';

-- Note: Metric_Name enum values:
-- USER_CREATE = 1, USER_UPDATE = 2, USER_DELETE = 3
-- FARMER_CREATE = 10, FARMER_UPDATE = 11, FARMER_DELETE = 12
-- FARM_CREATE = 20, FARM_UPDATE = 21, FARM_DELETE = 22
-- CROP_REGISTRY_CREATE = 30, CROP_REGISTRY_UPDATE = 31, CROP_REGISTRY_DELETE = 32
-- LIVESTOCK_REGISTRY_CREATE = 40, LIVESTOCK_REGISTRY_DELETE = 41
-- AGRO_ALLIED_REGISTRY_CREATE = 50, AGRO_ALLIED_REGISTRY_DELETE = 51
-- And many more... (see MetricNames.cs enum for complete list)
