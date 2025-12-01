using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IDashboardMetricsRepository
    {
        Task SaveForm(DashboardMetrics entity);
        Task<List<DashboardMetrics>> GetList(DashboardMetricsParam param);
        Task<List<DashboardMetrics>> GetList();
        Task<DashboardMetrics?> GetEntity(int metricId);
        Task<DashboardMetrics?> GetMetricByPeriod(METRICNAMES metricName, int? userId = null, int? farmerId = null, long? farmId = null, int dailyPeriod = 0, int weeklyPeriod = 0, int monthlyPeriod = 0);
        Task UpdateMetricCount(METRICNAMES metricName, int incrementBy, int? userId = null, int? farmerId = null, long? farmId = null, long? entityId = null);
    }
}
