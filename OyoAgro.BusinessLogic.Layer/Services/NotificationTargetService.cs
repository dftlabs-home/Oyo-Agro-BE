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
    public class NotificationTargetService : INotificationTargetService
    {
        private readonly IUnitOfWork _unitOfWork;


        public NotificationTargetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Notificationtarget>> SaveEntity(NotificationTargetParam param)
        {
            var obj = new TData<Notificationtarget>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (param.Notificationid == 0)
            {
                obj.Message = " Notiication is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Regionid == 0)
            {
                obj.Message = " Region No is required";
                obj.Tag = 0;
                return obj;
            }

            if (param.Lgaid == 0)
            {
                obj.Message = " LGA is required";
                obj.Tag = 0;
                return obj;
            }


            var association = new Notificationtarget()
            {
                Notificationid = param.Notificationid,
                Regionid = param.Regionid,
                Lgaid = param.Lgaid,
                Userid = param.Userid,
            };
            await _unitOfWork.NotificationTargetRepository.SaveForm(association);
            obj.Tag = 1;
            obj.Data = association;
            return obj;
        }

        public async Task<TData<List<Notificationtarget>>> GetList(NotificationTargetParam param)
        {
            var response = new TData<List<Notificationtarget>>();
            var obj = await _unitOfWork.NotificationTargetRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Notificationtarget>>> GetList()
        {
            var response = new TData<List<Notificationtarget>>();
            var obj = await _unitOfWork.NotificationTargetRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Notificationtarget>> GetEntity(int targetId)
        {
            var response = new TData<Notificationtarget>();
            var obj = await _unitOfWork.NotificationTargetRepository.GetEntity(targetId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Notificationtarget>> DeleteEntity(int targetId)
        {
            var response = new TData<Notificationtarget>();
            await _unitOfWork.NotificationTargetRepository.DeleteForm(targetId);
            response.Tag = 1;
            return response;
        }

    }
}
