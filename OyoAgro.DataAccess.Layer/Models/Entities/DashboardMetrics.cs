using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    [Table("dashboardmetrics")]
    public class DashboardMetrics : BaseEntity
    {
        [Key]
        [Column("Metric_Id")]
        public int _MetricId { get; set; }

        [Column("Metric_Name")]
        public METRICNAMES _MetricName { get; set; }

        [Column("Metric_Value")]
        public int _MetricValue { get; set; }

        //[Column("Metric_MonthlyPeriod")]
        //public int _MetricMonthlyPeriod { get; set; } // Year * 100 + Month (e.g., 202412 for Dec 2024)

        //[Column("Metric_WeeklyPeriod")]
        //public int _MetricWeeklyPeriod { get; set; } // Year * 100 + Week (e.g., 202450 for week 50 of 2024)

        //[Column("Metric_DailyPeriod")]
        //public int _MetricDailyPeriod { get; set; } // Year * 10000 + Month * 100 + Day (e.g., 20241225 for Dec 25, 2024)

        [Column("Metric_CreateDate")]
        public DateTime _MetricCreateDate { get; set; }

        [Column("Metric_UpdatedDate")]
        public DateTime _MetricUpdatedDate { get; set; }

        [Column("SysNumber")]
        public long _SysNumber { get; set; }

        // Additional fields for tracking relationships
        [Column("CreatedByUserId")]
        public int? CreatedByUserId { get; set; }

        [Column("RelatedFarmerId")]
        public int? RelatedFarmerId { get; set; }

        [Column("RelatedFarmId")]
        public long? RelatedFarmId { get; set; }

        [Column("EntityId")]
        public long? EntityId { get; set; } // ID of the entity that was operated on

        // Navigation properties
        public virtual Useraccount? CreatedByUser { get; set; }
        public virtual Farmer? RelatedFarmer { get; set; }
        public virtual Farm? RelatedFarm { get; set; }
    }
}
