using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class DashboardMetricsRepository : DataRepository, IDashboardMetricsRepository
    {
        private AppDbContext _context;
        public DashboardMetricsRepository(AppDbContext context) 
        {
            _context = context;
        }

        //public async Task<DashboardMetrics?> GetMetricsAsync(string entityType, int? entityId = null)
        //{
        //    return await _context.DashboardMetrics
        //        .Where(d => d.EntityType == entityType && 
        //                   d.EntityId == entityId && 
        //                   d.Deletedat == null)
        //        .OrderByDescending(d => d.Createdat)
        //        .FirstOrDefaultAsync();
        //}

        //public async Task<DashboardMetrics> GetOrCreateMetricsAsync(string entityType, int? entityId = null)
        //{
        //    var metrics = await GetMetricsAsync(entityType, entityId);
        //    if (metrics == null)
        //    {
        //        metrics = new DashboardMetrics
        //        {
        //            EntityType = entityType,
        //            EntityId = entityId,
        //            Createdat = DateTime.UtcNow,
        //            Updatedat = DateTime.UtcNow,
        //            LastCalculated = DateTime.UtcNow,
        //            LastUpdated = DateTime.UtcNow
        //        };
        //        await AddAsync(metrics);
        //        await _context.SaveChangesAsync();
        //    }
        //    return metrics;
        //}

        //public async Task IncrementCountAsync(string entityType, int? entityId, string countField, int incrementBy = 1)
        //{
        //    var metrics = await GetOrCreateMetricsAsync(entityType, entityId);
        //    var property = typeof(DashboardMetrics).GetProperty(countField);

        //    if (property != null)
        //    {
        //        if (property.PropertyType == typeof(int))
        //        {
        //            var currentValue = (int)property.GetValue(metrics);
        //            var newValue = Math.Max(0, currentValue + incrementBy);
        //            property.SetValue(metrics, newValue);
        //        }
        //        else if (property.PropertyType == typeof(decimal))
        //        {
        //            var currentValue = (decimal)property.GetValue(metrics);
        //            var newValue = Math.Max(0, currentValue + incrementBy);
        //            property.SetValue(metrics, newValue);
        //        }
                
        //        metrics.LastUpdated = DateTime.UtcNow;
        //        metrics.Updatedat = DateTime.UtcNow;
        //        await UpdateAsync(metrics);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task DecrementCountAsync(string entityType, int? entityId, string countField, int decrementBy = 1)
        //{
        //    var metrics = await GetMetricsAsync(entityType, entityId);
        //    if (metrics == null) return; // Can't decrement if metrics don't exist

        //    var property = typeof(DashboardMetrics).GetProperty(countField);
        //    if (property != null)
        //    {
        //        if (property.PropertyType == typeof(int))
        //        {
        //            var currentValue = (int)property.GetValue(metrics);
        //            var newValue = Math.Max(0, currentValue - decrementBy);
        //            property.SetValue(metrics, newValue);
        //        }
        //        else if (property.PropertyType == typeof(decimal))
        //        {
        //            var currentValue = (decimal)property.GetValue(metrics);
        //            var newValue = Math.Max(0, currentValue - decrementBy);
        //            property.SetValue(metrics, newValue);
        //        }
                
        //        metrics.LastUpdated = DateTime.UtcNow;
        //        metrics.Updatedat = DateTime.UtcNow;
        //        await UpdateAsync(metrics);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task UpdateCountAsync(string entityType, int? entityId, string countField, int newValue)
        //{
        //    var metrics = await GetOrCreateMetricsAsync(entityType, entityId);
        //    var property = typeof(DashboardMetrics).GetProperty(countField);

        //    if (property != null)
        //    {
        //        property.SetValue(metrics, Math.Max(0, newValue));
        //        metrics.LastUpdated = DateTime.UtcNow;
        //        metrics.Updatedat = DateTime.UtcNow;
        //        await UpdateAsync(metrics);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task UpdateMetricAsync(string entityType, int? entityId, string fieldName, object value)
        //{
        //    var metrics = await GetOrCreateMetricsAsync(entityType, entityId);
        //    var property = typeof(DashboardMetrics).GetProperty(fieldName);

        //    if (property != null)
        //    {
        //        property.SetValue(metrics, value);
        //        metrics.LastUpdated = DateTime.UtcNow;
        //        metrics.Updatedat = DateTime.UtcNow;
        //        await UpdateAsync(metrics);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
