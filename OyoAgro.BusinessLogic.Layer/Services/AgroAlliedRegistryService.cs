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
    public class AgroAlliedRegistryService : IAgroAlliedRegistryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgroAlliedRegistryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<AgroAlliedRegistry>> SaveEntity(AgroAlliedRegistryParam param)
        {
            var obj = new TData<AgroAlliedRegistry>();
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
            if (param.BusinessType == 0)
            {
                obj.Message = "Business type is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.PrimaryProduct == 0)
            {
                obj.Message = "Primary product is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.ProductionCapacity == 0)
            {
                obj.Message = "Production capacity is required";
                obj.Tag = 0;
                return obj;
            }

            var cropReg = new AgroAlliedRegistry
            {
                ProductionCapacity = param.ProductionCapacity,
                Seasonid = param.Seasonid,
                Farmid = param.Farmid,
                BusinessType = param.BusinessType,
                PrimaryProduct = param.PrimaryProduct,


            };
            await _unitOfWork.AgroAlliedRegistryRepository.SaveForm(cropReg);

            // Track counts: Global, User, Farmer, and Farm
            //await TrackLivestockRegistryCounts(param.Farmid, 1);

            obj.Tag = 1;
            obj.Data = cropReg;
            return obj;
        }

        public async Task<TData<List<AgroAlliedRegistry>>> GetList(AgroAlliedRegistryParam param)
        {
            var response = new TData<List<AgroAlliedRegistry>>();
            var obj = await _unitOfWork.AgroAlliedRegistryRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<AgroAlliedRegistry>>> GetListByFarm(long FarmId)
        {
            var response = new TData<List<AgroAlliedRegistry>>();
            var obj = await _unitOfWork.AgroAlliedRegistryRepository.GetEntitybyFarmId(FarmId);
            response.Data = obj;
            return response;
        }


        public async Task<TData<List<AgroAlliedRegistry>>> GetList()
        {
            var response = new TData<List<AgroAlliedRegistry>>();
            var obj = await _unitOfWork.AgroAlliedRegistryRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<AgroAlliedRegistry>> GetEntity(int registryId)
        {
            var response = new TData<AgroAlliedRegistry>();
            var obj = await _unitOfWork.AgroAlliedRegistryRepository.GetEntity(registryId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<AgroAlliedRegistry>> DeleteEntity(int registryId)
        {
            var response = new TData<AgroAlliedRegistry>();

            // Get livestock registry details before deletion to track counts
            var livestockRegistry = await _unitOfWork.AgroAlliedRegistryRepository.GetEntity(registryId);
            //if (livestockRegistry != null)
            //{
            //    // Track counts: Global, User, Farmer, and Farm (decrement)
            //    await TrackLivestockRegistryCounts(livestockRegistry.Farmid, -1);
            //}

            await _unitOfWork.LiveStockRegistryRepository.DeleteForm(registryId);
            response.Tag = 1;
            return response;
        }


    }
}
