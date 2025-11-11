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
    public class PrimaryProductService : IPrimaryProductService
    {
        private readonly IUnitOfWork _unitOfWork;


        public PrimaryProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<PrimaryProduct>> SaveEntity(PrimaryProductParam param)
        {
            var obj = new TData<PrimaryProduct>();
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


            var crop = new PrimaryProduct()
            {
                Name = param.Name,
            };
            await _unitOfWork.PrimaryProductRepository.SaveForm(crop);
            obj.Tag = 1;
            obj.Data = crop;
            return obj;
        }

        public async Task<TData<PrimaryProduct>> UpdateEntity(PrimaryProductParam param)
        {
            var obj = new TData<PrimaryProduct>();
            if (param.PrimaryProductTypeId == 0)
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


            var crop = new PrimaryProduct()
            {
                PrimaryProductTypeId = param.PrimaryProductTypeId,
                Name = param.Name,
            };
            await _unitOfWork.PrimaryProductRepository.SaveForm(crop);
            obj.Tag = 1;
            obj.Data = crop;
            return obj;
        }




        public async Task<TData<List<PrimaryProduct>>> GetList()
        {
            var response = new TData<List<PrimaryProduct>>();
            var obj = await _unitOfWork.PrimaryProductRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<PrimaryProduct>> GetEntity(int primaryProductId)
        {
            var response = new TData<PrimaryProduct>();
            var obj = await _unitOfWork.PrimaryProductRepository.GetEntity(primaryProductId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<PrimaryProduct>> DeleteEntity(int id)
        {
            var response = new TData<PrimaryProduct>();
            await _unitOfWork.PrimaryProductRepository.DeleteForm(id);
            response.Tag = 1;
            return response;
        }


    }
}
