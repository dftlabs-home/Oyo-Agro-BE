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
    public class LgaServices : ILgaServices
    {
        private readonly IUnitOfWork _unitOfWork;


        public LgaServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Lga>> SaveEntity(LgaParam param)
        {
            var obj = new TData<Lga>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Lganame))
            {
                obj.Message = " Name is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Regionid == 0)
            {
                obj.Message = " Region is required";
                obj.Tag = 0;
                return obj;
            }


            var lga = new Lga()
            {
                Regionid = param.Regionid,
                Lganame = param.Lganame,
            };
            await _unitOfWork.LgaRepository.SaveForm(lga);
            obj.Tag = 1;
            obj.Data = lga;
            return obj;
        }

        public async Task<TData<List<Lga>>> GetList(LgaParam param)
        {
            var response = new TData<List<Lga>>();
            var obj = await _unitOfWork.LgaRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Lga>>> GetList()
        {
            var response = new TData<List<Lga>>();
            var obj = await _unitOfWork.LgaRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Lga>> GetEntity(int lgaId)
        {
            var response = new TData<Lga>();
            var obj = await _unitOfWork.LgaRepository.GetEntity(lgaId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Lga>> DeleteEntity(int lgaId)
        {
            var response = new TData<Lga>();
            await _unitOfWork.LgaRepository.DeleteForm(lgaId);
            response.Tag = 1;
            return response;
        }
        public async Task<TData<List<Lga>>> GetEntityByRegionId(int regionId)
        {
            var obj = new TData<List<Lga>>();
            var result = await _unitOfWork.LgaRepository.GetListByRegionId(regionId);
            obj.Data = result;
            obj.Tag = 1;
            return obj;
        }

    }
}
