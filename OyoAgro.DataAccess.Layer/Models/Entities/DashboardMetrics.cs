using System;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public class DashboardMetrics : BaseEntity
    {
        public int DashboardMetricsId { get; set; }
        
        // Entity Type and ID to identify what this metric belongs to
        public string EntityType { get; set; } = null!; // "User", "Farmer", "Farm", "Global"
        public int? EntityId { get; set; } // ID of the entity (null for Global)
        
        // Count fields
        public int FarmerCount { get; set; } = 0;
        public int FarmCount { get; set; } = 0;
        public int CropRegistryCount { get; set; } = 0;
        public int LivestockRegistryCount { get; set; } = 0;
        
        // Additional metrics
        public decimal TotalAreaPlanted { get; set; } = 0;
        public decimal TotalAreaHarvested { get; set; } = 0;
        public decimal TotalYieldQuantity { get; set; } = 0;
        public decimal TotalPlantedQuantity { get; set; } = 0;
        public int TotalLivestockQuantity { get; set; } = 0;
        
        // Metadata
        public DateTime LastCalculated { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? Notes { get; set; }
        
        // Navigation properties (optional, for reference)
        public virtual Useraccount? User { get; set; }
        public virtual Farmer? Farmer { get; set; }
        public virtual Farm? Farm { get; set; }
    }
}
