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
    public class ProfileActivityParentService : IProfileActivityParentService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ProfileActivityParentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Profileactivityparent>> SaveEntity(ProfileActivityParentParam profileactivity)
        {
            var obj = new TData<Profileactivityparent>();
            if (profileactivity == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(profileactivity.Activityparentname))
            {
                obj.Message = "Parent Name is required";
                obj.Tag = 0;
                return obj;
            }        

            var activity = new Profileactivityparent()
            {
                Activityparentname = profileactivity.Activityparentname,
            };
            await _unitOfWork.ProfileActivityParent.SaveForm(activity);
            obj.Tag = 1;
            obj.Data = activity;
            return obj;
        }

        public async Task<TData<Profileactivityparent>> GetList(ProfileActivityParentParam param)
        {
            var response = new TData<Profileactivityparent>();
            var obj = await _unitOfWork.ProfileActivityParent.GetList(param);
            return response;
        }
        public async Task<TData<Profileactivityparent>> GetList()
        {
            var response = new TData<Profileactivityparent>();
            var obj = await _unitOfWork.ProfileActivityParent.GetList();
            return response;
        }

        public async Task<TData<Profileactivityparent>> GetEntity(int parentId)
        {
            var response = new TData<Profileactivityparent>();
            var obj = await _unitOfWork.ProfileActivity.GetEntity(parentId);
            return response;
        }
    }
}
