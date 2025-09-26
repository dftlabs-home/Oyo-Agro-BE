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
    public class RegionService : IRegionService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RegionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Region>> SaveEntity(RegionParam param)
        {
            var obj = new TData<Region>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Regionname))
            {
                obj.Message = " Name is required";
                obj.Tag = 0;
                return obj;
            }
          

            var region = new Region()
            {
                Regionname = param.Regionname,
            };
            await _unitOfWork.RegionRepository.SaveForm(region);
            obj.Tag = 1;
            obj.Data = region;
            return obj;
        }

        public async Task<TData<List<Region>>> GetList(RegionParam param)
        {
            var response = new TData<List<Region>>();
            var obj = await _unitOfWork.RegionRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Region>>> GetList()
        {
            var response = new TData<List<Region>>();
            var obj = await _unitOfWork.RegionRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Region>> GetEntity(int regionId)
        {
            var response = new TData<Region>();
            var obj = await _unitOfWork.RegionRepository.GetEntity(regionId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Lga>> DeleteEntity(int regionId)
        {
            var response = new TData<Lga>();
            await _unitOfWork.RegionRepository.DeleteForm(regionId);
            response.Tag = 1;
            return response;
        }
      
    }
}
