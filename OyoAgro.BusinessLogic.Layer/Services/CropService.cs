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
    public class CropService : ICropService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CropService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Crop>> SaveEntity(CropParam param)
        {
            var obj = new TData<Crop>();
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

        
            var crop = new Crop()
            {
                Name = param.Name,
            };
            await _unitOfWork.CropRepository.SaveForm(crop);
            obj.Tag = 1;
            obj.Data = crop;
            return obj;
        }

        public async Task<TData<List<Crop>>> GetList(CropParam param)
        {
            var response = new TData<List<Crop>>();
            var obj = await _unitOfWork.CropRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Crop>>> GetList()
        {
            var response = new TData<List<Crop>>();
            var obj = await _unitOfWork.CropRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Crop>> GetEntity(int cropId)
        {
            var response = new TData<Crop>();
            var obj = await _unitOfWork.CropRepository.GetEntity(cropId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Crop>> DeleteEntity(int cropId)
        {
            var response = new TData<Crop>();
            await _unitOfWork.CropRepository.DeleteForm(cropId);
            response.Tag = 1;
            return response;
        }


    }
}
