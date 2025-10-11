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
    public class CropRegistryService : ICropRegistryService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CropRegistryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Cropregistry>> SaveEntity(CropRegistryParam param)
        {
            var obj = new TData<Cropregistry>();
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
            if (param.Croptypeid == 0)
            {
                obj.Message = "Crop Type is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Areaplanted == 0)
            {
                obj.Message = "Area Planted is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Plantedquantity == 0)
            {
                obj.Message = "Planted Quantity is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Plantingdate == null)
            {
                obj.Message = "Date Planted is required";
                obj.Tag = 0;
                return obj;
            }

            var cropReg = new Cropregistry
            {
                Areaharvested = param.Areaharvested,
                Areaplanted = param.Areaplanted,
                Croptypeid = param.Croptypeid,
                Farmid = param.Farmid,
                Harvestdate = param.Harvestdate,
                Seasonid = param.Seasonid,
                Plantingdate = param.Plantingdate,
                Yieldquantity = param.Yieldquantity,
                Plantedquantity = param.Plantedquantity,
                Cropvariety = param.Cropvariety,


            };
            await _unitOfWork.CropRegistryRepository.SaveForm(cropReg);


            obj.Tag = 1;
            obj.Data = cropReg;
            return obj;
        }
        
        public async Task<TData<Cropregistry>> UpdateEntity(CropRegistryParam param)
        {
            var obj = new TData<Cropregistry>();
            if (param.Cropregistryid == 0)
            {
                obj.Message = "Registry ID is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Croptypeid == 0)
            {
                obj.Message = "Crop Type is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Seasonid == 0)
            {
                obj.Message = "Season is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Croptypeid == 0)
            {
                obj.Message = "Crop Type is required";
                obj.Tag = 0;
                return obj;
            }
           
            if (param.Plantingdate == null)
            {
                obj.Message = "Date Planted is required";
                obj.Tag = 0;
                return obj;
            }

            var cropReg = new Cropregistry
            {
                Cropregistryid = param.Cropregistryid,
                Areaharvested = param.Areaharvested,
                Areaplanted = param.Areaplanted,
                Croptypeid = param.Croptypeid,
                Farmid = param.Farmid,
                Harvestdate = param.Harvestdate,
                Seasonid = param.Seasonid,
                Plantingdate = param.Plantingdate,
                Yieldquantity = param.Yieldquantity,
                Plantedquantity = param.Plantedquantity,
                Cropvariety = param.Cropvariety,

            };
            await _unitOfWork.CropRegistryRepository.SaveForm(cropReg);


            obj.Tag = 1;
            obj.Data = cropReg;
            return obj;
        }

        public async Task<TData<List<Cropregistry>>> GetList(CropRegistryParam param)
        {
            var response = new TData<List<Cropregistry>>();
            var obj = await _unitOfWork.CropRegistryRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Cropregistry>>> GetList()
        {
            var response = new TData<List<Cropregistry>>();
            var obj = await _unitOfWork.CropRegistryRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Cropregistry>> GetEntity(int cropRegistryId)
        {
            var response = new TData<Cropregistry>();
            var obj = await _unitOfWork.CropRegistryRepository.GetEntity(cropRegistryId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Cropregistry>> DeleteEntity(int cropRegistryId)
        {
            var response = new TData<Cropregistry>();
            await _unitOfWork.CropRegistryRepository.DeleteForm(cropRegistryId);
            response.Tag = 1;
            return response;
        }

    }
}
