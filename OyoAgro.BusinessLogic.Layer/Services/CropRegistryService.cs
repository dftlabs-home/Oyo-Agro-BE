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
    public class CropRegistryService : ICropRegistryService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CropRegistryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Cropregistry>> SaveEntity(CropRegistryParam param)
        {
            var obj = new TData<Cropregistry>();
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
            if (param.Croptypeid == 0)
            {
                obj.Message = "Crop Type is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Areaplanted == 0)
            {
                obj.Message = "Area Planted is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Plantedquantity == 0)
            {
                obj.Message = "Planted Quantity is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Plantingdate == null)
            {
                obj.Message = "Date Planted is required";
                obj.Tag = 0;
                return obj;
            }
            
            if (param.Plantingdate != null && param.Harvestdate != null)
            {
                if (param.Plantingdate > param.Harvestdate)
                {
                    obj.Message = "Planting date cannot be greater than harvest date";
                    obj.Tag = 0;
                    return obj;
                }
                
            }

            var cropReg = new Cropregistry
            {
                Areaharvested = param.Areaharvested,
                Areaplanted = param.Areaplanted,
                Croptypeid = param.Croptypeid,
                Farmid = param.Farmid,
                Harvestdate = param.Harvestdate,
                Seasonid = param.Seasonid,
                Plantingdate = param.Plantingdate,
                Yieldquantity = param.Yieldquantity,
                Plantedquantity = param.Plantedquantity,
                Cropvariety = param.Cropvariety,


            };
            await _unitOfWork.CropRegistryRepository.SaveForm(cropReg);

            // Track counts: Global, User, Farmer, and Farm
            //await TrackCropRegistryCounts(param.Farmid, 1);

            obj.Tag = 1;
            obj.Data = cropReg;
            return obj;
        }
        
        public async Task<TData<Cropregistry>> UpdateEntity(CropRegistryParam param)
        {
            var obj = new TData<Cropregistry>();
            if (param.Cropregistryid == 0)
            {
                obj.Message = "Registry ID is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Croptypeid == 0)
            {
                obj.Message = "Crop Type is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Seasonid == 0)
            {
                obj.Message = "Season is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Croptypeid == 0)
            {
                obj.Message = "Crop Type is required";
                obj.Tag = 0;
                return obj;
            }
           
            if (param.Plantingdate == null)
            {
                obj.Message = "Date Planted is required";
                obj.Tag = 0;
                return obj;
            }

            var cropReg = new Cropregistry
            {
                Cropregistryid = param.Cropregistryid,
                Areaharvested = param.Areaharvested,
                Areaplanted = param.Areaplanted,
                Croptypeid = param.Croptypeid,
                Farmid = param.Farmid,
                Harvestdate = param.Harvestdate,
                Seasonid = param.Seasonid,
                Plantingdate = param.Plantingdate,
                Yieldquantity = param.Yieldquantity,
                Plantedquantity = param.Plantedquantity,
                Cropvariety = param.Cropvariety,

            };
            await _unitOfWork.CropRegistryRepository.SaveForm(cropReg);


            obj.Tag = 1;
            obj.Data = cropReg;
            return obj;
        }

        public async Task<TData<List<Cropregistry>>> GetList(CropRegistryParam param)
        {
            var response = new TData<List<Cropregistry>>();
            var obj = await _unitOfWork.CropRegistryRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Cropregistry>>> GetList()
        {
            var response = new TData<List<Cropregistry>>();
            var obj = await _unitOfWork.CropRegistryRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Cropregistry>> GetEntity(int cropRegistryId)
        {
            var response = new TData<Cropregistry>();
            var obj = await _unitOfWork.CropRegistryRepository.GetEntity(cropRegistryId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Cropregistry>> DeleteEntity(int cropRegistryId)
        {
            var response = new TData<Cropregistry>();
            
            // Get crop registry details before deletion to track counts
            var cropRegistry = await _unitOfWork.CropRegistryRepository.GetEntity(cropRegistryId);
            //if (cropRegistry != null)
            //{
            //    // Track counts: Global, User, Farmer, and Farm (decrement)
            //    await TrackCropRegistryCounts(cropRegistry.Farmid, -1);
            //}
            
            await _unitOfWork.CropRegistryRepository.DeleteForm(cropRegistryId);
            response.Tag = 1;
            return response;
        }

        /// <summary>
        /// Track crop registry counts for global, user, farmer, and farm statistics
        /// </summary>
        /// <param name="farmId">Farm ID</param>
        /// <param name="incrementBy">Amount to increment/decrement (1 or -1)</param>
        //private async Task TrackCropRegistryCounts(int farmId, int incrementBy)
        //{
        //    try
        //    {
        //        // Get farm details to get farmer and user information
        //        var farm = await _unitOfWork.FarmRepository.GetEntity(farmId);
        //        if (farm?.Farmer != null)
        //        {
        //            // Update farmer's crop registry count
        //            await UpdateFarmerCropRegistryCount(farm.Farmerid, incrementBy);
                    
        //            // Update user's crop registry count (the user who created/manages this farmer)
        //            if (farm.Farmer.UserId.HasValue)
        //            {
        //                await UpdateUserCropRegistryCount(farm.Farmer.UserId.Value, incrementBy);
        //            }
        //        }
                
        //        // Update farm's crop registry count
        //        await UpdateFarmCropRegistryCount(farmId, incrementBy);
                
        //        // Update global crop registry count
        //        await UpdateGlobalCropRegistryCount(incrementBy);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error but don't fail the main operation
        //        System.Diagnostics.Debug.WriteLine($"Error tracking crop registry counts: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update global crop registry count
        /// </summary>
        //private async Task UpdateGlobalCropRegistryCount(int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("Global", null, "CropRegistryCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("Global", null, "CropRegistryCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating global crop registry count: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update farmer's crop registry count
        /// </summary>
        //private async Task UpdateFarmerCropRegistryCount(int farmerId, int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("Farmer", farmerId, "CropRegistryCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("Farmer", farmerId, "CropRegistryCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating farmer crop registry count: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update user's crop registry count
        /// </summary>
        //private async Task UpdateUserCropRegistryCount(int userId, int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("User", userId, "CropRegistryCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("User", userId, "CropRegistryCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating user crop registry count: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update farm's crop registry count
        /// </summary>
        //private async Task UpdateFarmCropRegistryCount(int farmId, int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("Farm", farmId, "CropRegistryCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("Farm", farmId, "CropRegistryCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating farm crop registry count: {ex.Message}");
        //    }
        //}

    }
}
