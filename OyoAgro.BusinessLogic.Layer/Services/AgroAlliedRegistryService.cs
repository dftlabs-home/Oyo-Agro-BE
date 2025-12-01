using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class AgroAlliedRegistryService : IAgroAlliedRegistryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDashboardMetricsService _metricsService;

        public AgroAlliedRegistryService(IUnitOfWork unitOfWork, IDashboardMetricsService metricsService)
        {
            _unitOfWork = unitOfWork;
            _metricsService = metricsService;
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
            if (param.BusinessTypeId == 0)
            {
                obj.Message = "Business type is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.PrimaryProductId == 0)
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
                BusinessTypeId = param.BusinessTypeId,
                PrimaryProductTypeId = param.PrimaryProductId,


            };
            await _unitOfWork.AgroAlliedRegistryRepository.SaveForm(cropReg);

            // Increment agro-allied registry count
            try
            {
                var farm = await _unitOfWork.FarmRepository.GetEntity(param.Farmid);
                if (farm != null)
                {
                    var farmer = await _unitOfWork.FarmerRepository.GetEntity(farm.Farmerid);
                    int? userId = farmer?.UserId;

                    await _metricsService.IncrementCountAsync(
                        METRICNAMES.AGRO_ALLIED_REGISTRY_COUNT,
                        userId: userId,
                        farmerId: farm.Farmerid,
                        farmId: param.Farmid,
                        entityId: cropReg.AgroAlliedRegistryid
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error incrementing agro-allied registry count: {ex.Message}");
            }

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

            // Get agro-allied registry details before deletion
            var agroAlliedRegistry = await _unitOfWork.AgroAlliedRegistryRepository.GetEntity(registryId);
            
            // Decrement agro-allied registry count
            if (agroAlliedRegistry != null)
            {
                try
                {
                    var farm = await _unitOfWork.FarmRepository.GetEntity(agroAlliedRegistry.Farmid);
                    if (farm != null)
                    {
                        var farmer = await _unitOfWork.FarmerRepository.GetEntity(farm.Farmerid);
                        int? userId = farmer?.UserId;

                        await _metricsService.DecrementCountAsync(
                            METRICNAMES.AGRO_ALLIED_REGISTRY_COUNT,
                            userId: userId,
                            farmerId: farm.Farmerid,
                            farmId: agroAlliedRegistry.Farmid,
                            entityId: agroAlliedRegistry.AgroAlliedRegistryid
                        );
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error decrementing agro-allied registry count: {ex.Message}");
                }
            }

            await _unitOfWork.AgroAlliedRegistryRepository.DeleteForm(registryId);
            response.Tag = 1;
            return response;
        }


    }
}
