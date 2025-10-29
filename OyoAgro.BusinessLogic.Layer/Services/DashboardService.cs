using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardCountsDto> GetGlobalCountsAsync()
        {
            try
            {
                var farmers = await _unitOfWork.FarmerRepository.GetList();
                var farms = await _unitOfWork.FarmRepository.GetList();
                var cropRegistries = await _unitOfWork.CropRegistryRepository.GetList();
                var livestockRegistries = await _unitOfWork.LiveStockRegistryRepository.GetList();

                return new DashboardCountsDto
                {
                    TotalFarmers = farmers?.Count ?? 0,
                    TotalFarms = farms?.Count ?? 0,
                    TotalCropRegistrations = cropRegistries?.Count ?? 0,
                    TotalLivestockRegistrations = livestockRegistries?.Count ?? 0,
                    LastUpdated = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting global counts: {ex.Message}");
                return new DashboardCountsDto { LastUpdated = DateTime.UtcNow };
            }
        }

        //public async Task<DashboardCountsDto> GetUserCountsAsync(int userId)
        //{
        //    try
        //    {
        //        var metrics = await _unitOfWork.DashboardMetricsRepository.GetMetricsAsync("User", userId);
        //        if (metrics == null)
        //        {
        //            return new DashboardCountsDto { LastUpdated = DateTime.UtcNow };
        //        }

        //        return new DashboardCountsDto
        //        {
        //            TotalFarmers = metrics.FarmerCount,
        //            TotalFarms = metrics.FarmCount,
        //            TotalCropRegistrations = metrics.CropRegistryCount,
        //            TotalLivestockRegistrations = metrics.LivestockRegistryCount,
        //            LastUpdated = metrics.LastUpdated
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error getting user counts: {ex.Message}");
        //        return new DashboardCountsDto { LastUpdated = DateTime.UtcNow };
        //    }
        //}

        //public async Task<DashboardCountsDto> GetFarmerCountsAsync(int farmerId)
        //{
        //    try
        //    {
        //        var metrics = await _unitOfWork.DashboardMetricsRepository.GetMetricsAsync("Farmer", farmerId);
        //        if (metrics == null)
        //        {
        //            return new DashboardCountsDto { LastUpdated = DateTime.UtcNow };
        //        }

        //        return new DashboardCountsDto
        //        {
        //            TotalFarmers = 1, // This farmer
        //            TotalFarms = metrics.FarmCount,
        //            TotalCropRegistrations = metrics.CropRegistryCount,
        //            TotalLivestockRegistrations = metrics.LivestockRegistryCount,
        //            LastUpdated = metrics.LastUpdated
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error getting farmer counts: {ex.Message}");
        //        return new DashboardCountsDto { LastUpdated = DateTime.UtcNow };
        //    }
        //}

        //public async Task<DashboardCountsDto> GetFarmCountsAsync(int farmId)
        //{
        //    try
        //    {
        //        var metrics = await _unitOfWork.DashboardMetricsRepository.GetMetricsAsync("Farm", farmId);
        //        if (metrics == null)
        //        {
        //            return new DashboardCountsDto { LastUpdated = DateTime.UtcNow };
        //        }

        //        return new DashboardCountsDto
        //        {
        //            TotalFarmers = 1, // The farmer who owns this farm
        //            TotalFarms = 1, // This farm
        //            TotalCropRegistrations = metrics.CropRegistryCount,
        //            TotalLivestockRegistrations = metrics.LivestockRegistryCount,
        //            LastUpdated = metrics.LastUpdated
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error getting farm counts: {ex.Message}");
        //        return new DashboardCountsDto { LastUpdated = DateTime.UtcNow };
        //    }
        //}
    }
}
