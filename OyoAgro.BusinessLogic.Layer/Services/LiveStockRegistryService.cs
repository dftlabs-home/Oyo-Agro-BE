using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class LiveStockRegistryService : ILiveStockRegistryService
    {

        private readonly IUnitOfWork _unitOfWork;

        public LiveStockRegistryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Livestockregistry>> SaveEntity(LiveStockRegistryParam param)
        {
            var obj = new TData<Livestockregistry>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (param.Farmid == 0)
            {
                obj.Message = "Farm is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Seasonid == 0)
            {
                obj.Message = "Season is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Livestocktypeid == 0)
            {
                obj.Message = "Live stock type is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Quantity == 0)
            {
                obj.Message = "Quantity is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Startdate == null)
            {
                obj.Message = "Start date is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Enddate == null)
            {
                obj.Message = "Planted is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Startdate > param.Enddate)
            {
                obj.Message = "start date cannot be greater than end date";
                obj.Tag = 0;
                return obj;
            }

            var cropReg = new Livestockregistry
            {
                Quantity = param.Quantity,
               Seasonid = param.Seasonid,
               Farmid = param.Farmid,
               Livestocktypeid = param.Livestocktypeid,
               Startdate = param.Startdate,
               Enddate = param.Enddate,
               

            };
            await _unitOfWork.LiveStockRegistryRepository.SaveForm(cropReg);

            // Track counts: Global, User, Farmer, and Farm
            //await TrackLivestockRegistryCounts(param.Farmid, 1);

            obj.Tag = 1;
            obj.Data = cropReg;
            return obj;
        }

        public async Task<TData<List<Livestockregistry>>> GetList(LiveStockRegistryParam param)
        {
            var response = new TData<List<Livestockregistry>>();
            var obj = await _unitOfWork.LiveStockRegistryRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Livestockregistry>>> GetList()
        {
            var response = new TData<List<Livestockregistry>>();
            var obj = await _unitOfWork.LiveStockRegistryRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Livestockregistry>> GetEntity(int registryId)
        {
            var response = new TData<Livestockregistry>();
            var obj = await _unitOfWork.LiveStockRegistryRepository.GetEntity(registryId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Livestockregistry>> DeleteEntity(int registryId)
        {
            var response = new TData<Livestockregistry>();
            
            // Get livestock registry details before deletion to track counts
            var livestockRegistry = await _unitOfWork.LiveStockRegistryRepository.GetEntity(registryId);
            //if (livestockRegistry != null)
            //{
            //    // Track counts: Global, User, Farmer, and Farm (decrement)
            //    await TrackLivestockRegistryCounts(livestockRegistry.Farmid, -1);
            //}
            
            await _unitOfWork.LiveStockRegistryRepository.DeleteForm(registryId);
            response.Tag = 1;
            return response;
        }

        /// <summary>
        /// Track livestock registry counts for global, user, farmer, and farm statistics
        /// </summary>
        /// <param name="farmId">Farm ID</param>
        /// <param name="incrementBy">Amount to increment/decrement (1 or -1)</param>
        //private async Task TrackLivestockRegistryCounts(int farmId, int incrementBy)
        //{
        //    try
        //    {
        //        // Get farm details to get farmer and user information
        //        var farm = await _unitOfWork.FarmRepository.GetEntity(farmId);
        //        if (farm?.Farmer != null)
        //        {
        //            // Update farmer's livestock registry count
        //            await UpdateFarmerLivestockRegistryCount(farm.Farmerid, incrementBy);
                    
        //            // Update user's livestock registry count (the user who created/manages this farmer)
        //            if (farm.Farmer.UserId.HasValue)
        //            {
        //                await UpdateUserLivestockRegistryCount(farm.Farmer.UserId.Value, incrementBy);
        //            }
        //        }
                
        //        // Update farm's livestock registry count
        //        //await UpdateFarmLivestockRegistryCount(farmId, incrementBy);
                
        //        // Update global livestock registry count
        //        //await UpdateGlobalLivestockRegistryCount(incrementBy);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error but don't fail the main operation
        //        System.Diagnostics.Debug.WriteLine($"Error tracking livestock registry counts: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update global livestock registry count
        /// </summary>
        //private async Task UpdateGlobalLivestockRegistryCount(int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("Global", null, "LivestockRegistryCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("Global", null, "LivestockRegistryCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating global livestock registry count: {ex.Message}");
        //    }
        //}

        ///// <summary>
        /// Update farmer's livestock registry count
        /// </summary>
        //private async Task UpdateFarmerLivestockRegistryCount(int farmerId, int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("Farmer", farmerId, "LivestockRegistryCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("Farmer", farmerId, "LivestockRegistryCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating farmer livestock registry count: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update user's livestock registry count
        /// </summary>
        //private async Task UpdateUserLivestockRegistryCount(int userId, int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("User", userId, "LivestockRegistryCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("User", userId, "LivestockRegistryCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating user livestock registry count: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update farm's livestock registry count
        /// </summary>
        //private async Task UpdateFarmLivestockRegistryCount(int farmId, int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("Farm", farmId, "LivestockRegistryCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("Farm", farmId, "LivestockRegistryCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating farm livestock registry count: {ex.Message}");
        //    }
        //}

    }
}
