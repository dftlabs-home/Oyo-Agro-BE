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
    public class ProfileAdditionalActivityService : IProfileAdditionalActivityService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ProfileAdditionalActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Profileadditionalactivity>> SaveEntity(ProfileadditionalactivityParam profileactivity)
        {
            var obj = new TData<Profileadditionalactivity>();
            if (profileactivity == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (profileactivity.UserId == 0)
            {
                obj.Message = "User cannot be null";
                obj.Tag = 0;
                return obj;
            }
            if (profileactivity.Activityid == 0)
            {
                obj.Message = "activity cannot be null";
                obj.Tag = 0;
                return obj;
            }

            var activity = new Profileadditionalactivity()
            {
                Userid = profileactivity.UserId,
                Activityid = profileactivity.Activityid,
                Canadd = profileactivity.Canadd,
                Canapprove = profileactivity.Canapprove,
                Candelete = profileactivity.Candelete,
                Canedit = profileactivity.Canedit,
                Expireon = profileactivity.Expireon,
            };
            await _unitOfWork.ProfileAdditionalActivities.SaveForm(activity);
            obj.Tag = 1;
            obj.Data = activity;
            return obj;
        }

        public async Task<TData<Profileadditionalactivity>> GetList(ProfileadditionalactivityParam param)
        {
            var response = new TData<Profileadditionalactivity>();
            var obj = await _unitOfWork.ProfileAdditionalActivities.GetList(param);
            return response;
        }
        public async Task<TData<Profileadditionalactivity>> GetList()
        {
            var response = new TData<Profileadditionalactivity>();
            var obj = await _unitOfWork.ProfileActivityParent.GetList();
            return response;
        }

        public async Task<TData<Profileadditionalactivity>> GetEntity(int profileAdditionalActivityId)
        {
            var response = new TData<Profileadditionalactivity>();
            var obj = await _unitOfWork.ProfileActivity.GetEntity(profileAdditionalActivityId);
            return response;
        }
        public async Task<TData> DeleteEntity(int profileAdditionalActivityId)
        {
            var obj = new TData();
            var response = new TData<Profileadditionalactivity>();
            await _unitOfWork.ProfileActivity.DeleteForm(profileAdditionalActivityId);
            obj.Tag = 1;
            return response;
        }

    }
}
