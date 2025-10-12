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
    public class SeasonServices : ISeasonServices
    {
        private readonly IUnitOfWork _unitOfWork;


        public SeasonServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public async Task<TData<Season>> SaveEntity(SeasonParam param)
        {
            var obj = new TData<Season>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (param.Year == 0)
            {
                obj.Message = " Year is required";
                obj.Tag = 0;
                return obj;
            }
             if (param.Enddate == null)
            {
                obj.Message = " End date is required";
                obj.Tag = 0;
                return obj;
            }

             if (param.Startdate == null)
            {
                obj.Message = " start date is required";
                obj.Tag = 0;
                return obj;
            }


            var crop = new Season()
            {
                Name = param.Name,
                Startdate = param.Startdate,
                Enddate = param.Enddate,
                Year = param.Year,
                
            };
            await _unitOfWork.SeasonRepository.SaveForm(crop);
            obj.Tag = 1;
            obj.Data = crop;
            return obj;
        }

        public async Task<TData<Season>> UpdateEntity(SeasonParam param)
        {
            var obj = new TData<Season>();

            if (param.Seasonid == 0)
            {
                obj.Message = " seasonid is required";
                obj.Tag = 0;
                return obj;
            }


            if (param.Year == 0)
            {
                obj.Message = " Year is required";
                obj.Tag = 0;
                return obj;
            }
            if (param.Enddate == null)
            {
                obj.Message = " End date is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Startdate == null)
            {
                obj.Message = " start date is required";
                obj.Tag = 0;
                return obj;
            }



            var crop = new Season()
            {
                Seasonid = param.Seasonid,
                Year = param.Year,
                Startdate = param.Startdate,
                Enddate = param.Enddate,
              
            };
            await _unitOfWork.SeasonRepository.SaveForm(crop);
            obj.Tag = 1;
            obj.Data = crop;
            return obj;
        }



        public async Task<TData<List<Season>>> GetList(SeasonParam param)
        {
            var response = new TData<List<Season>>();
            var obj = await _unitOfWork.SeasonRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Season>>> GetList()
        {
            var response = new TData<List<Season>>();
            var obj = await _unitOfWork.SeasonRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Season>> GetEntity(int seasonId)
        {
            var response = new TData<Season>();
            var obj = await _unitOfWork.SeasonRepository.GetEntity(seasonId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Season>> DeleteEntity(int seasonId)
        {
            var response = new TData<Season>();
            await _unitOfWork.SeasonRepository.DeleteForm(seasonId);
            response.Tag = 1;
            return response;
        }


    }
}
