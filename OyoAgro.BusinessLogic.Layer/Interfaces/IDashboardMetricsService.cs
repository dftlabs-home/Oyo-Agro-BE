using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Interfaces
{
    public interface IDashboardMetricsService
    {
        Task IncrementCountAsync(METRICNAMES metricName, int? userId = null, int? farmerId = null, long? farmId = null, long? entityId = null);
        Task DecrementCountAsync(METRICNAMES metricName, int? userId = null, int? farmerId = null, long? farmId = null, long? entityId = null);
        Task<TData<List<DashboardMetricsDto>>> GetMetricsAsync(DashboardMetricsParam param);
        Task<TData<DashboardMetricsSummaryDto>> GetMetricsSummaryAsync(int? userId = null, int? farmerId = null, long? farmId = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<TData<List<DashboardMetrics>>> GetList();
    }
}

