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
using OyoAgro.DataAccess.Layer.Models.ViewModels;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class FarmService : IFarmService
    {
        private readonly IUnitOfWork _unitOfWork;


        public FarmService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Farm>> SaveEntity(FarmParam param)
        {
            var obj = new TData<Farm>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (param.Farmerid == 0)
            {
                obj.Message = "Farmer is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Farmtypeid == 0)
            {
                obj.Message = "Farm Type is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Farmsize == 0)
            {
                obj.Message = "Farm Size is required";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Postalcode))
            {
                obj.Message = "Postal Code is required";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Streetaddress))
            {
                obj.Message = "Address is required";
                obj.Tag = 0;
                return obj;
            }
            if (string.IsNullOrEmpty(param.Town))
            {
                obj.Message = "Town is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Lgaid == 0)
            {
                obj.Message = "LGA is required";
                obj.Tag = 0;
                return obj;
            }


            var farmSave = new Farm
            {
                Farmerid = param.Farmerid,
                Farmsize = param.Farmsize,
                Farmtypeid = param.Farmtypeid,

            };

            await _unitOfWork.FarmRepository.SaveForm(farmSave);

            // Track counts: Global, User, and Farmer
            //await TrackFarmCounts(param.Farmerid, param.UserId, 1);

            var farmAddress = new Address
            {
                Farmid = Convert.ToInt32(farmSave.Farmid),
                Lgaid = param.Lgaid,
                Postalcode = param.Postalcode,
                Streetaddress = param.Streetaddress,
                Latitude = param.Latitude,
                Longitude = param.Longitude,
                Town = param.Town,
            };
            await _unitOfWork.AddressRepository.SaveForm(farmAddress);

            obj.Tag = 1;
            obj.Data = farmSave;
            return obj;
        }


        public async Task<TData<Farm>> UpdateEntity(FarmParam param)
        {
            var obj = new TData<Farm>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (param.Farmerid == 0)
            {
                obj.Message = "Farmer is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Farmid == 0)
            {
                obj.Message = "Farm Id is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Farmtypeid == 0)
            {
                obj.Message = "Farm Type is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Farmsize == 0)
            {
                obj.Message = "Farm Size is required";
                obj.Tag = 0;
                return obj;
            }

            //if (string.IsNullOrEmpty(param.Postalcode))
            //{
            //    obj.Message = "Postal Code is required";
            //    obj.Tag = 0;
            //    return obj;
            //}

            //if (string.IsNullOrEmpty(param.Streetaddress))
            //{
            //    obj.Message = "Address is required";
            //    obj.Tag = 0;
            //    return obj;
            //}
            //if (string.IsNullOrEmpty(param.Town))
            //{
            //    obj.Message = "Town is required";
            //    obj.Tag = 0;
            //    return obj;
            //}

            //if (param.Lgaid == 0)
            //{
            //    obj.Message = "LGA is required";
            //    obj.Tag = 0;
            //    return obj;
            //}

            var farm = await _unitOfWork.FarmRepository.GetEntity(param.Farmid);
            if (farm == null)
            {
                obj.Message = "Farm not found";
                obj.Tag = 0;
                return obj;
            }

            var farmSave = new Farm
            {
                Farmid = param.Farmid,
                Farmerid = param.Farmerid,
                Farmsize = param.Farmsize,
                Farmtypeid = param.Farmtypeid,

            };

            await _unitOfWork.FarmRepository.SaveForm(farmSave);
          

            obj.Tag = 1;
            obj.Data = farmSave;
            return obj;
        }

        public async Task<TData<List<Farm>>> GetList(FarmParam param)
        {
            var response = new TData<List<Farm>>();
            var obj = await _unitOfWork.FarmRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Farm>>> GetList()
        {
            var response = new TData<List<Farm>>();
            var obj = await _unitOfWork.FarmRepository.GetList();
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Farm>>> GetListByFarmerId(int farmerId)
        {
            var response = new TData<List<Farm>>();
            var obj = await _unitOfWork.FarmRepository.GetListbyFarmerId(farmerId);
            response.Data = obj;
            return response;
        }

        public async Task<TData<FarmViewModel>> GetEntity(int farmId)
        {
            var response = new TData<FarmViewModel>
            {
                Data = new FarmViewModel()
            };

            var farm = await _unitOfWork.FarmRepository.GetEntity(farmId);
            if (farm == null)
                return response;

            var farmAddress = await _unitOfWork.AddressRepository
                .GetEntitybyFarmId(farmId);
            var farmer = await _unitOfWork.FarmerRepository
                .GetEntity(farm.Farmerid);
            var farmType = await _unitOfWork.FarmTypeRepository
                .GetEntity(farm.Farmtypeid);
            var lga = farmAddress != null
                ? await _unitOfWork.LgaRepository.GetEntity((int)farmAddress.Lgaid)
                : null;

            response.Data.Farmid = farm.Farmid;
            response.Data.Farmer = $"{farmer?.Firstname ?? ""} {farmer?.Lastname ?? ""}".Trim();
            response.Data.Farmtype = farmType?.Typename;
            response.Data.Farmsize = farm.Farmsize;
            response.Data.Longitude = farmAddress?.Longitude;
            response.Data.Latitude = farmAddress?.Latitude;
            response.Data.Streetaddress = farmAddress?.Streetaddress;
            response.Data.Postalcode = farmAddress?.Postalcode;
            response.Data.Lga = lga?.Lganame;

            return response;
        }
        public async Task<TData<Farm>> DeleteEntity(int farmId)
        {
            var response = new TData<Farm>();
            
            // Get farm details before deletion to track counts
            var farm = await _unitOfWork.FarmRepository.GetEntity(farmId);
            //if (farm != null)
            //{
            //    // Track counts: Global, User, and Farmer (decrement)
            //    await TrackFarmCounts(farm.Farmerid, farm.Farmer?.UserId, -1);
            //}
            
            await _unitOfWork.FarmRepository.DeleteForm(farmId);
            response.Tag = 1;
            return response;
        }

        /// <summary>
        /// Track farm counts for global, user, and farmer statistics
        /// </summary>
        /// <param name="farmerId">Farmer ID</param>
        /// <param name="userId">User ID (who created the farm)</param>
        /// <param name="incrementBy">Amount to increment/decrement (1 or -1)</param>
        //private async Task TrackFarmCounts(int farmerId, int? userId, int incrementBy)
        //{
        //    try
        //    {
        //        // Get farmer details to ensure we have the correct user
        //        var farmer = await _unitOfWork.FarmerRepository.GetEntity(farmerId);
        //        if (farmer?.UserId.HasValue == true)
        //        {
        //            // Update farmer's farm count
        //            await UpdateFarmerFarmCount(farmerId, incrementBy);
                    
        //            // Update user's farm count (the user who created/manages this farmer)
        //            await UpdateUserFarmCount(farmer.UserId.Value, incrementBy);
        //        }
                
        //        // Update global farm count
        //        await UpdateGlobalFarmCount(incrementBy);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error but don't fail the main operation
        //        System.Diagnostics.Debug.WriteLine($"Error tracking farm counts: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update global farm count
        /// </summary>
        //private async Task UpdateGlobalFarmCount(int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("Global", null, "FarmCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("Global", null, "FarmCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating global farm count: {ex.Message}");
        //    }
        //}

        ///// <summary>
        /// Update farmer's farm count
        /// </summary>
        //private async Task UpdateFarmerFarmCount(int farmerId, int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("Farmer", farmerId, "FarmCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("Farmer", farmerId, "FarmCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating farmer farm count: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update user's farm count
        /// </summary>
        //private async Task UpdateUserFarmCount(int userId, int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("User", userId, "FarmCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("User", userId, "FarmCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating user farm count: {ex.Message}");
        //    }
        //}

    }
}
