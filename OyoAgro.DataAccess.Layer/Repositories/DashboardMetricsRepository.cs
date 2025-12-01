using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Extensions;
using OyoAgro.DataAccess.Layer.Helpers;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;
using OyoAgro.DataAccess.Layer.Models.Params;
using OyoAgro.DataAccess.Layer.Repositories.Base;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class DashboardMetricsRepository : DataRepository, IDashboardMetricsRepository
    {
        public async Task SaveForm(DashboardMetrics entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity._MetricId == 0)
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }
                await db.CommitTrans();
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw ex;
            }
        }

        public async Task<List<DashboardMetrics>> GetList(DashboardMetricsParam param)
        {
            var db = BaseRepository();
            var expression = ExtensionLinq.True<DashboardMetrics>();

            if (param.MetricName.HasValue)
            {
                expression = expression.And(t => t._MetricName == param.MetricName.Value);
            }

            if (param.CreatedByUserId.HasValue && param.CreatedByUserId.Value > 0)
            {
                expression = expression.And(t => t.CreatedByUserId == param.CreatedByUserId.Value);
            }

            if (param.RelatedFarmerId.HasValue && param.RelatedFarmerId.Value > 0)
            {
                expression = expression.And(t => t.RelatedFarmerId == param.RelatedFarmerId.Value);
            }

            if (param.RelatedFarmId.HasValue && param.RelatedFarmId.Value > 0)
            {
                expression = expression.And(t => t.RelatedFarmId == param.RelatedFarmId.Value);
            }

            //if (param.DailyPeriod > 0)
            //{
            //    expression = expression.And(t => t._MetricDailyPeriod == param.DailyPeriod);
            //}

            //if (param.WeeklyPeriod > 0)
            //{
            //    expression = expression.And(t => t._MetricWeeklyPeriod == param.WeeklyPeriod);
            //}

            //if (param.MonthlyPeriod > 0)
            //{
            //    expression = expression.And(t => t._MetricMonthlyPeriod == param.MonthlyPeriod);
            //}

            if (param.StartDate.HasValue)
            {
                expression = expression.And(t => t._MetricCreateDate >= param.StartDate.Value);
            }

            if (param.EndDate.HasValue)
            {
                expression = expression.And(t => t._MetricCreateDate <= param.EndDate.Value);
            }

            var list = await db.FindList(expression);
            return list.OrderByDescending(x => x._MetricCreateDate).ToList();
        }

        public async Task<List<DashboardMetrics>> GetList()
        {
            var db = BaseRepository();
            var list = await db.FindList<DashboardMetrics>();
            return list.OrderByDescending(x => x._MetricCreateDate).ToList();
        }

        public async Task<DashboardMetrics?> GetEntity(int metricId)
        {
            var db = BaseRepository();
            return await db.FindEntity<DashboardMetrics>(metricId);
        }

        public async Task<DashboardMetrics?> GetMetricByPeriod(METRICNAMES metricName, int? userId = null, int? farmerId = null, long? farmId = null, int dailyPeriod = 0, int weeklyPeriod = 0, int monthlyPeriod = 0)
        {
            var db = BaseRepository();
            var expression = ExtensionLinq.True<DashboardMetrics>();
            expression = expression.And(t => t._MetricName == metricName);

            if (userId.HasValue)
            {
                expression = expression.And(t => t.CreatedByUserId == userId.Value);
            }
            else
            {
                expression = expression.And(t => t.CreatedByUserId == null);
            }

            if (farmerId.HasValue)
            {
                expression = expression.And(t => t.RelatedFarmerId == farmerId.Value);
            }
            else
            {
                expression = expression.And(t => t.RelatedFarmerId == null);
            }

            if (farmId.HasValue)
            {
                expression = expression.And(t => t.RelatedFarmId == farmId.Value);
            }
            else
            {
                expression = expression.And(t => t.RelatedFarmId == null);
            }

            // For current count tracking, we use period = 0
            // So if all periods are 0, we're looking for the current count metric
            //if (dailyPeriod == 0 && weeklyPeriod == 0 && monthlyPeriod == 0)
            //{
            //    expression = expression.And(t => t._MetricDailyPeriod == 0 && t._MetricWeeklyPeriod == 0 && t._MetricMonthlyPeriod == 0);
            //}
            //else
            //{
            //    if (dailyPeriod > 0)
            //    {
            //        expression = expression.And(t => t._MetricDailyPeriod == dailyPeriod);
            //    }

            //    if (weeklyPeriod > 0)
            //    {
            //        expression = expression.And(t => t._MetricWeeklyPeriod == weeklyPeriod);
            //    }

            //    if (monthlyPeriod > 0)
            //    {
            //        expression = expression.And(t => t._MetricMonthlyPeriod == monthlyPeriod);
            //    }
            //}

            var metrics = await db.FindList(expression);
            return metrics.FirstOrDefault();
        }

        public async Task UpdateMetricCount(METRICNAMES metricName, int incrementBy, int? userId = null, int? farmerId = null, long? farmId = null, long? entityId = null)
        {
            var now = DateTime.UtcNow;
            var dailyPeriod = now.Year * 10000 + now.Month * 100 + now.Day;
            var weeklyPeriod = GetWeekOfYear(now);
            var monthlyPeriod = now.Year * 100 + now.Month;

            // Get the current period's metric (or create if doesn't exist)
            // For count tracking, we want to track the current count, not per-period counts
            // So we'll use a special period value of 0 to represent "current count"
            var existingMetric = await GetMetricByPeriod(metricName, userId, farmerId, farmId, 0, 0, 0);

            if (existingMetric != null)
            {
                existingMetric._MetricValue = Math.Max(0, existingMetric._MetricValue + incrementBy);
                existingMetric._MetricUpdatedDate = now;
                existingMetric.EntityId = entityId;
                await SaveForm(existingMetric);
            }
            else
            {
                // Create new metric with initial value
                var initialValue = Math.Max(0, incrementBy);
                var newMetric = new DashboardMetrics
                {
                    _MetricName = metricName,
                    _MetricValue = initialValue,
                    //_MetricDailyPeriod = 0, // 0 means "current count", not a specific period
                    //_MetricWeeklyPeriod = 0,
                    //_MetricMonthlyPeriod = 0,
                    _MetricCreateDate = now,
                    _MetricUpdatedDate = now,
                    CreatedByUserId = userId,
                    RelatedFarmerId = farmerId,
                    RelatedFarmId = farmId,
                    EntityId = entityId,
                    _SysNumber = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };
                await SaveForm(newMetric);
            }
        }

        private int GetWeekOfYear(DateTime date)
        {
            var year = date.Year;
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = (int)jan1.DayOfWeek;
            var firstMonday = jan1.AddDays((daysOffset > 0 ? 8 - daysOffset : 1 - daysOffset) - 1);
            var weekNum = ((date - firstMonday).Days / 7) + 1;
            return year * 100 + weekNum;
        }
    }
}
