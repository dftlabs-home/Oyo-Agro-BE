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

            if (string.IsNullOrEmpty(param.Firstname))
            {
                obj.Message = "First Name is required";
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
                obj.Message = "Last Name is required";
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

            if (param.Residentialaddressid == 0)
            {
                obj.Message = "Residential Address is required";
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
                Residentialaddressid = param.Residentialaddressid,
                Associationid = param.Associationid,
                Availablelabor = param.Availablelabor,
                Dateofbirth = param.Dateofbirth,
                Gender = param.Gender,
                Householdsize = param.Householdsize,
                Lastname = param.Lastname,
                Middlename = param.Middlename,
                Phonenumber = param.Phonenumber,
                Photourl = param.Photourl,
                
            };

            await _unitOfWork.FarmerRepository.SaveForm(farmerSave);
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
            await _unitOfWork.FarmerRepository.DeleteForm(farmerId);
            response.Tag = 1;
            return response;
        }

    }
}
