using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class DashboardMetricsService : IDashboardMetricsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardMetricsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task IncrementCountAsync(METRICNAMES metricName, int? userId = null, int? farmerId = null, long? farmId = null, long? entityId = null)
        {
            try
            {
                // Validate requirements
                if (MetricRequirements.RequiresUser(metricName) && !userId.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine($"Warning: Metric {metricName} requires User but none provided");
                    return;
                }

                if (MetricRequirements.RequiresFarmer(metricName) && !farmerId.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine($"Warning: Metric {metricName} requires Farmer but none provided");
                    return;
                }

                if (MetricRequirements.RequiresFarm(metricName) && !farmId.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine($"Warning: Metric {metricName} requires Farm but none provided");
                    return;
                }

                await _unitOfWork.DashboardMetricsRepository.UpdateMetricCount(metricName, 1, userId, farmerId, farmId, entityId);
            }
            catch (Exception ex)
            {
                // Log error but don't throw - metrics shouldn't break main operations
                System.Diagnostics.Debug.WriteLine($"Error incrementing metric count: {ex.Message}");
            }
        }

        public async Task DecrementCountAsync(METRICNAMES metricName, int? userId = null, int? farmerId = null, long? farmId = null, long? entityId = null)
        {
            try
            {
                // Validate requirements
                if (MetricRequirements.RequiresUser(metricName) && !userId.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine($"Warning: Metric {metricName} requires User but none provided");
                    return;
                }

                if (MetricRequirements.RequiresFarmer(metricName) && !farmerId.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine($"Warning: Metric {metricName} requires Farmer but none provided");
                    return;
                }

                if (MetricRequirements.RequiresFarm(metricName) && !farmId.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine($"Warning: Metric {metricName} requires Farm but none provided");
                    return;
                }

                await _unitOfWork.DashboardMetricsRepository.UpdateMetricCount(metricName, -1, userId, farmerId, farmId, entityId);
            }
            catch (Exception ex)
            {
                // Log error but don't throw - metrics shouldn't break main operations
                System.Diagnostics.Debug.WriteLine($"Error decrementing metric count: {ex.Message}");
            }
        }

        public async Task<TData<List<DashboardMetricsDto>>> GetMetricsAsync(DashboardMetricsParam param)
        {
            var response = new TData<List<DashboardMetricsDto>>();
            try
            {
                var metrics = await _unitOfWork.DashboardMetricsRepository.GetList(param);
                
                var metricDtos = metrics.Select(m => new DashboardMetricsDto
                {
                    MetricId = m._MetricId,
                    MetricName = m._MetricName,
                    MetricValue = m._MetricValue,
                    //MetricMonthlyPeriod = m._MetricMonthlyPeriod,
                    //MetricWeeklyPeriod = m._MetricWeeklyPeriod,
                    //MetricDailyPeriod = m._MetricDailyPeriod,
                    MetricCreateDate = m._MetricCreateDate,
                    MetricUpdatedDate = m._MetricUpdatedDate,
                    CreatedByUserId = m.CreatedByUserId,
                    RelatedFarmerId = m.RelatedFarmerId,
                    RelatedFarmId = m.RelatedFarmId,
                    EntityId = m.EntityId
                }).ToList();

                response.Data = metricDtos;
                response.Tag = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Tag = 0;
            }
            return response;
        }

        public async Task<TData<DashboardMetricsSummaryDto>> GetMetricsSummaryAsync(int? userId = null, int? farmerId = null, long? farmId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var response = new TData<DashboardMetricsSummaryDto>();
            try
            {
                var param = new DashboardMetricsParam
                {
                    CreatedByUserId = userId,
                    RelatedFarmerId = farmerId,
                    RelatedFarmId = farmId,
                    StartDate = startDate,
                    EndDate = endDate
                };

                var allMetrics = await _unitOfWork.DashboardMetricsRepository.GetList(param);
                
                var summary = new DashboardMetricsSummaryDto
                {
                    TotalUsersCreated = allMetrics.Where(m => m._MetricName == METRICNAMES.USER_COUNT).Sum(m => m._MetricValue),
                    TotalFarmersCreated = allMetrics.Where(m => m._MetricName == METRICNAMES.FARMER_COUNT).Sum(m => m._MetricValue),
                    TotalFarmsCreated = allMetrics.Where(m => m._MetricName == METRICNAMES.FARM_COUNT).Sum(m => m._MetricValue),
                    TotalCropRegistriesCreated = allMetrics.Where(m => m._MetricName == METRICNAMES.CROP_REGISTRY_COUNT).Sum(m => m._MetricValue),
                    TotalLivestockRegistriesCreated = allMetrics.Where(m => m._MetricName == METRICNAMES.LIVESTOCK_REGISTRY_COUNT).Sum(m => m._MetricValue),
                    TotalAgroAlliedRegistriesCreated = allMetrics.Where(m => m._MetricName == METRICNAMES.AGRO_ALLIED_REGISTRY_COUNT).Sum(m => m._MetricValue),
                    MetricsByType = allMetrics.GroupBy(m => m._MetricName)
                        .ToDictionary(g => g.Key, g => g.Sum(m => m._MetricValue))
                };

                response.Data = summary;
                response.Tag = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Tag = 0;
            }
            return response;
        }


        public async Task<TData<List<DashboardMetrics>>> GetList()
        {
            var response = new TData<List<DashboardMetrics>>();
            var obj = await _unitOfWork.DashboardMetricsRepository.GetList();
            response.Data = obj;
            return response;
        }
    }
}

