using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Enums;

namespace OyoAgro.DataAccess.Layer.Models.Dtos
{
    public class DashboardMetricsDto
    {
        public int MetricId { get; set; }
        public METRICNAMES MetricName { get; set; }
        public int MetricValue { get; set; }
        public int MetricMonthlyPeriod { get; set; }
        public int MetricWeeklyPeriod { get; set; }
        public int MetricDailyPeriod { get; set; }
        public DateTime MetricCreateDate { get; set; }
        public DateTime MetricUpdatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? RelatedFarmerId { get; set; }
        public long? RelatedFarmId { get; set; }
        public long? EntityId { get; set; }
    }

    public class DashboardMetricsSummaryDto
    {
        public int TotalUsersCreated { get; set; }
        public int TotalFarmersCreated { get; set; }
        public int TotalFarmsCreated { get; set; }
        public int TotalCropRegistriesCreated { get; set; }
        public int TotalLivestockRegistriesCreated { get; set; }
        public int TotalAgroAlliedRegistriesCreated { get; set; }
        public Dictionary<METRICNAMES, int> MetricsByType { get; set; } = new();
    }
}

