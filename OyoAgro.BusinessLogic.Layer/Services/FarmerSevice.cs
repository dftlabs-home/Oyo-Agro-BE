using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Conversion;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class FarmerSevice : IFarmerSevice
    {
        private readonly IUnitOfWork _unitOfWork;


        public FarmerSevice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Farmer>> SaveEntity(FarmerParam param)
        {
            var obj = new TData<Farmer>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Firstname))
            {
                obj.Message = "First Name is required";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Lastname))
            {
                obj.Message = "First Name is required";
                obj.Tag = 0;
                return obj;
            }

           

            if (string.IsNullOrEmpty(param.Email))
            {
                obj.Message = "Email Address is required";
                obj.Tag = 0;
                return obj;
            }
            if (string.IsNullOrEmpty(param.Gender))
            {
                obj.Message = "Gender is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Dateofbirth == null)
            {
                obj.Message = "DOB is required";
                obj.Tag = 0;
                return obj;
            }
            if (string.IsNullOrEmpty(param.Phonenumber))
            {
                obj.Message = "Phone Number is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Associationid == 0)
            {
                obj.Message = "Association is required";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Streetaddress))
            {
                obj.Message = "Street Address is required";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Town))
            {
                obj.Message = "Town is required";
                obj.Tag = 0;
                return obj;
            } 
            
            if (string.IsNullOrEmpty(param.Postalcode))
            {
                obj.Message = "Postal Code is required";
                obj.Tag = 0;
                return obj;
            }

             
            if (param.Lgaid == 0)
            {
                obj.Message = "LGA is required";
                obj.Tag = 0;
                return obj;
            }



            var emailExist = await _unitOfWork.FarmerRepository.GetEntitybyEmail(param.Email);
            if (emailExist != null)
            {
                obj.Message = "Email Address exists";
                obj.Tag = 0;
                return obj;
            }

            var phoneExist = await _unitOfWork.FarmerRepository.GetEntitybyPhonel(param.Phonenumber);
            if (phoneExist != null)
            {
                obj.Message = "Phone No exists";
                obj.Tag = 0;
                return obj;
            }

            var farmerSave = new Farmer
            {
                Email = param.Email,
                Firstname = param.Firstname,
                Associationid = param.Associationid,
                Availablelabor = param.Availablelabor,  
                Dateofbirth = param.Dateofbirth,
                Gender = param.Gender,
                Householdsize = param.Householdsize,
                Lastname = param.Lastname,
                Middlename = param.Middlename,
                Phonenumber = param.Phonenumber,
                Photourl = param.Photourl,
                UserId = param.UserId,
            };

            await _unitOfWork.FarmerRepository.SaveForm(farmerSave);

            // Track counts: Global and User
            //await TrackFarmerCounts(param.UserId, 1);

            var farmerAddress = new Address
            {
                Farmerid = farmerSave.Farmerid,
                Lgaid = param.Lgaid,
                Postalcode = param.Postalcode,
                Streetaddress = param.Streetaddress,
                Latitude = param.Latitude,
                Longitude = param.Longitude,
                Town = param.Town,
            };
            await _unitOfWork.AddressRepository.SaveForm(farmerAddress);


            obj.Tag = 1;
            obj.Data = farmerSave;
            return obj;
        }
        
        public async Task<TData<Farmer>> UpdateEntity(FarmerParam param)
        {
            var obj = new TData<Farmer>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }
            if (param.Farmerid == 0)
            {
                obj.Message = "FarmerId is required";
                obj.Tag = 0;
                return obj;
            }

            var farmer =  await _unitOfWork.FarmerRepository.GetEntity(param.Farmerid);
            if (farmer == null)
            {
                obj.Message = "farmer not found";
                obj.Tag = 0;
                return obj;
            }

            var farmerSave = new Farmer
            {
                Farmerid = param.Farmerid,
                Email = param.Email,
                Firstname = param.Firstname,
                Associationid = param.Associationid,
                Availablelabor = param.Availablelabor,  
                Dateofbirth = param.Dateofbirth,
                Gender = param.Gender,
                Householdsize = param.Householdsize,
                Lastname = param.Lastname,
                Middlename = param.Middlename,
                Phonenumber = param.Phonenumber,
                Photourl = param.Photourl,
                UserId = param.UserId,
            };

            await _unitOfWork.FarmerRepository.SaveForm(farmerSave);

            //var farmerAddress = new Address
            //{
            //    Farmerid = farmerSave.Farmerid,
            //    Lgaid = param.Lgaid,
            //    Postalcode = param.Postalcode,
            //    Streetaddress = param.Streetaddress,
            //    Latitude = param.Latitude,
            //    Longitude = param.Longitude,
            //    Town = param.Town,
            //};
            //await _unitOfWork.AddressRepository.SaveForm(farmerAddress);


            obj.Tag = 1;
            obj.Data = farmerSave;
            return obj;
        }

        public async Task<TData<List<Farmer>>> GetList(FarmerParam param)
        {
            var response = new TData<List<Farmer>>();
            var obj = await _unitOfWork.FarmerRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Farmer>>> GetListByUserId(int userId)
        {
            var response = new TData<List<Farmer>>();
            var obj = await _unitOfWork.FarmerRepository.GetEntitybyUserId(userId);
            response.Data = obj;
            return response;
        }


        public async Task<TData<List<Farmer>>> GetList()
        {
            var response = new TData<List<Farmer>>();
            var obj = await _unitOfWork.FarmerRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Farmer>> GetEntity(int farmerId)
        {
            var response = new TData<Farmer>();
            var obj = await _unitOfWork.FarmerRepository.GetEntity(farmerId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Farmer>> DeleteEntity(int farmerId)
        {
            var response = new TData<Farmer>();
            
            // Get farmer details before deletion to track counts
            var farmer = await _unitOfWork.FarmerRepository.GetEntity(farmerId);
            //if (farmer != null)
            //{
            //    // Track counts: Global and User (decrement)
            //    await TrackFarmerCounts(farmer.UserId, -1);
            //}
            
            await _unitOfWork.FarmerRepository.DeleteForm(farmerId);
            response.Tag = 1;
            return response;
        }

        /// <summary>
        /// Track farmer counts for global and user statistics
        /// </summary>
        /// <param name="userId">User ID (who created the farmer)</param>
        /// <param name="incrementBy">Amount to increment/decrement (1 or -1)</param>
        //private async Task TrackFarmerCounts(int? userId, int incrementBy)
        //{
        //    try
        //    {
        //        if (userId.HasValue)
        //        {
        //            // Update user's farmer count
        //            await UpdateUserFarmerCount(userId.Value, incrementBy);
        //        }
                
        //        // Update global farmer count
        //        await UpdateGlobalFarmerCount(incrementBy);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error but don't fail the main operation
        //        System.Diagnostics.Debug.WriteLine($"Error tracking farmer counts: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update global farmer count
        /// </summary>
        //private async Task UpdateGlobalFarmerCount(int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("Global", null, "FarmerCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("Global", null, "FarmerCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating global farmer count: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Update user's farmer count
        /// </summary>
        //private async Task UpdateUserFarmerCount(int userId, int incrementBy)
        //{
        //    try
        //    {
        //        if (incrementBy > 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.IncrementCountAsync("User", userId, "FarmerCount", incrementBy);
        //        }
        //        else if (incrementBy < 0)
        //        {
        //            await _unitOfWork.DashboardMetricsRepository.DecrementCountAsync("User", userId, "FarmerCount", Math.Abs(incrementBy));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error updating user farmer count: {ex.Message}");
        //    }
        //}

    }
}
