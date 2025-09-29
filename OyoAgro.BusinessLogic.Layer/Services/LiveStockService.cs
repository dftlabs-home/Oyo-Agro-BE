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
    public class LiveStockService : ILiveStockService
    {
        private readonly IUnitOfWork _unitOfWork;


        public LiveStockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Livestock>> SaveEntity(LivestockParam param)
        {
            var obj = new TData<Livestock>();
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


            var livestock = new Livestock()
            {
                Name = param.Name,
            };
            await _unitOfWork.LivestockRepository.SaveForm(livestock);
            obj.Tag = 1;
            obj.Data = livestock;
            return obj;
        }

        public async Task<TData<List<Livestock>>> GetList(LivestockParam param)
        {
            var response = new TData<List<Livestock>>();
            var obj = await _unitOfWork.LivestockRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Livestock>>> GetList()
        {
            var response = new TData<List<Livestock>>();
            var obj = await _unitOfWork.LivestockRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Livestock>> GetEntity(int livestockId)
        {
            var response = new TData<Livestock>();
            var obj = await _unitOfWork.LivestockRepository.GetEntity(livestockId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Livestock>> DeleteEntity(int livestockId)
        {
            var response = new TData<Livestock>();
            await _unitOfWork.LivestockRepository.DeleteForm(livestockId);
            response.Tag = 1;
            return response;
        }


    }
}
