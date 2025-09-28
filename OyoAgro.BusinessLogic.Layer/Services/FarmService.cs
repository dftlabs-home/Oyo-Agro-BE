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

            if (param.Farmaddressid == 0)
            {
                obj.Message = "Farm Address is required";
                obj.Tag = 0;
                return obj;
            }

         
            var farmSave = new Farm
            {
                Farmaddressid = param.Farmaddressid,
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

        public async Task<TData<Farm>> GetEntity(int farmId)
        {
            var response = new TData<Farm>();
            var obj = await _unitOfWork.FarmRepository.GetEntity(farmId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Farm>> DeleteEntity(int farmId)
        {
            var response = new TData<Farm>();
            await _unitOfWork.FarmRepository.DeleteForm(farmId);
            response.Tag = 1;
            return response;
        }

    }
}
