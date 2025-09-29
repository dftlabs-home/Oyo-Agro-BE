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
            await _unitOfWork.LiveStockRegistryRepository.DeleteForm(registryId);
            response.Tag = 1;
            return response;
        }


    }
}
