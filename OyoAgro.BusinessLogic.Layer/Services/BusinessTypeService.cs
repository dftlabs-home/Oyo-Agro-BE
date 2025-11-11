using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class BusinessTypeService : IBusinessTypeService
    {
        private readonly IUnitOfWork _unitOfWork;


        public BusinessTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<BusinessType>> SaveEntity(BusinessTypeParam param)
        {
            var obj = new TData<BusinessType>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Name))
            {
                obj.Message = " Name is required";
                obj.Tag = 0;
                return obj;
            }


            var crop = new BusinessType()
            {
                Name = param.Name,
            };
            await _unitOfWork.BusinessTypeRepository.SaveForm(crop);
            obj.Tag = 1;
            obj.Data = crop;
            return obj;
        }

        public async Task<TData<BusinessType>> UpdateEntity(BusinessTypeParam param)
        {
            var obj = new TData<BusinessType>();
            if (param.BusinessTypeId == 0)
            {
                obj.Message = " Type ID is required ";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Name))
            {
                obj.Message = " Name is required";
                obj.Tag = 0;
                return obj;
            }


            var crop = new BusinessType()
            {
                BusinessTypeId = param.BusinessTypeId,
                Name = param.Name,
            };
            await _unitOfWork.BusinessTypeRepository.SaveForm(crop);
            obj.Tag = 1;
            obj.Data = crop;
            return obj;
        }



       
        public async Task<TData<List<BusinessType>>> GetList()
        {
            var response = new TData<List<BusinessType>>();
            var obj = await _unitOfWork.BusinessTypeRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<BusinessType>> GetEntity(int businessTypeId)
        {
            var response = new TData<BusinessType>();
            var obj = await _unitOfWork.BusinessTypeRepository.GetEntity(businessTypeId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<BusinessType>> DeleteEntity(int id)
        {
            var response = new TData<BusinessType>();
            await _unitOfWork.BusinessTypeRepository.DeleteForm(id);
            response.Tag = 1;
            return response;
        }


    }
}
