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
    public class ProfileActivityService : IProfileActivityService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ProfileActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Profileactivity>> SaveEntity(ProfileActivityParam profileactivity)
        {
            var obj = new TData<Profileactivity>();
            if (profileactivity == null) 
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(profileactivity.Activityname)) 
            {
                obj.Message = "Activity Name is required";
                obj.Tag = 0;
                return obj;
            }

            if (profileactivity.Activityparentid == 0)
            {
                obj.Message = "Activity Parent is required";
                obj.Tag = 0;
                return obj;
            }

            var activity = new Profileactivity()
            {
                Activityname = profileactivity.Activityname,
                Activityparentid = profileactivity.Activityparentid,
            };
            await _unitOfWork.ProfileActivity.SaveForm(activity);
            obj.Tag = 1;
            obj.Data = activity;
            return obj;
        }

        public async Task<TData<Profileactivity>> GetList(ProfileActivityParam param)
        {
            var response = new TData<Profileactivity>();
            var obj = await _unitOfWork.ProfileActivity.GetList(param);            
            return response;
        }
        public async Task<TData<Profileactivity>> GetList()
        {
            var response = new TData<Profileactivity>();
            var obj = await _unitOfWork.ProfileActivity.GetList();            
            return response;
        }

         public async Task<TData<Profileactivity>> GetEntity(int activityId)
        {
            var response = new TData<Profileactivity>();
            var obj = await _unitOfWork.ProfileActivity.GetEntity(activityId);            
            return response;
        }


    }
}
