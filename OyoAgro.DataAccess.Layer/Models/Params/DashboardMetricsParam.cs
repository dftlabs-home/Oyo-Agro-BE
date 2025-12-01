using System;
using OyoAgro.DataAccess.Layer.Enums;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class DashboardMetricsParam
    {
        public int MetricId { get; set; }
        public METRICNAMES? MetricName { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? RelatedFarmerId { get; set; }
        public long? RelatedFarmId { get; set; }
        public int DailyPeriod { get; set; }
        public int WeeklyPeriod { get; set; }
        public int MonthlyPeriod { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}

