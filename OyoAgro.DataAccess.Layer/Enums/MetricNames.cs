using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Enums
{
    /// <summary>
    /// Enum representing count metrics for entities in the system
    /// These track the actual count of items, not the number of operations
    /// </summary>
    public enum METRICNAMES
    {
        // User Count (No requirements)
        USER_COUNT = 1,

        // Farmer Count (Requires User)
        FARMER_COUNT = 10,

        // Farm Count (Requires User and Farmer)
        FARM_COUNT = 20,

        // Crop Registry Count (Requires User, Farmer, and Farm)
        CROP_REGISTRY_COUNT = 30,

        // Livestock Registry Count (Requires User, Farmer, and Farm)
        LIVESTOCK_REGISTRY_COUNT = 40,

        // AgroAllied Registry Count (Requires User, Farmer, and Farm)
        AGRO_ALLIED_REGISTRY_COUNT = 50,

        // Association Count (Requires User)
        ASSOCIATION_COUNT = 60,

        // Crop Count (Can track User)
        CROP_COUNT = 70,

        // Livestock Count (Can track User)
        LIVESTOCK_COUNT = 80,

        // Season Count (Can track User)
        SEASON_COUNT = 90,

        // FarmType Count (Can track User)
        FARM_TYPE_COUNT = 100,

        // BusinessType Count (Can track User)
        BUSINESS_TYPE_COUNT = 110,

        // PrimaryProduct Count (Can track User)
        PRIMARY_PRODUCT_COUNT = 120,

        // Notification Count (Requires User)
        NOTIFICATION_COUNT = 130,

        // NotificationTarget Count (Requires User)
        NOTIFICATION_TARGET_COUNT = 140
    }

    /// <summary>
    /// Helper class to determine requirements for each metric
    /// </summary>
    public static class MetricRequirements
    {
        public static bool RequiresUser(METRICNAMES metric)
        {
            return metric switch
            {
                METRICNAMES.FARMER_COUNT
                or METRICNAMES.FARM_COUNT
                or METRICNAMES.CROP_REGISTRY_COUNT
                or METRICNAMES.LIVESTOCK_REGISTRY_COUNT
                or METRICNAMES.AGRO_ALLIED_REGISTRY_COUNT
                or METRICNAMES.ASSOCIATION_COUNT
                or METRICNAMES.NOTIFICATION_COUNT
                or METRICNAMES.NOTIFICATION_TARGET_COUNT => true,
                _ => false
            };
        }

        public static bool RequiresFarmer(METRICNAMES metric)
        {
            return metric switch
            {
                METRICNAMES.FARM_COUNT
                or METRICNAMES.CROP_REGISTRY_COUNT
                or METRICNAMES.LIVESTOCK_REGISTRY_COUNT
                or METRICNAMES.AGRO_ALLIED_REGISTRY_COUNT => true,
                _ => false
            };
        }

        public static bool RequiresFarm(METRICNAMES metric)
        {
            return metric switch
            {
                METRICNAMES.CROP_REGISTRY_COUNT
                or METRICNAMES.LIVESTOCK_REGISTRY_COUNT
                or METRICNAMES.AGRO_ALLIED_REGISTRY_COUNT => true,
                _ => false
            };
        }

        public static string GetRequirementDescription(METRICNAMES metric)
        {
            var requirements = new List<string>();
            if (RequiresUser(metric)) requirements.Add("User");
            if (RequiresFarmer(metric)) requirements.Add("Farmer");
            if (RequiresFarm(metric)) requirements.Add("Farm");

            return requirements.Count > 0 
                ? $"Requires: {string.Join(", ", requirements)}" 
                : "No specific requirements";
        }
    }
}

