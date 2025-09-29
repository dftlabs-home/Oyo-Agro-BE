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
    public class FarmTypeService : IFarmTypeService
    {
        private readonly IUnitOfWork _unitOfWork;


        public FarmTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Farmtype>> SaveEntity(FarmTypeParam param)
        {
            var obj = new TData<Farmtype>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Typename))
            {
                obj.Message = " Name is required";
                obj.Tag = 0;
                return obj;
            }
            var type = new Farmtype()
            {
                Typename = param.Typename,
            };
            await _unitOfWork.FarmTypeRepository.SaveForm(type);
            obj.Tag = 1;
            obj.Data = type;
            return obj;
        }

        public async Task<TData<List<Farmtype>>> GetList(FarmTypeParam param)
        {
            var response = new TData<List<Farmtype>>();
            var obj = await _unitOfWork.FarmTypeRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Farmtype>>> GetList()
        {
            var response = new TData<List<Farmtype>>();
            var obj = await _unitOfWork.FarmTypeRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Farmtype>> GetEntity(int farmTypeId)
        {
            var response = new TData<Farmtype>();
            var obj = await _unitOfWork.FarmTypeRepository.GetEntity(farmTypeId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Farmtype>> DeleteEntity(int farmTypeId)
        {
            var response = new TData<Farmtype>();
            await _unitOfWork.FarmTypeRepository.DeleteForm(farmTypeId);
            response.Tag = 1;
            return response;
        }


    }

}
