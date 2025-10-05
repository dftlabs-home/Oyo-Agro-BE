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
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;


        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Notification>> SaveEntity(NotificationParam param)
        {
            var obj = new TData<Notification>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (param.Createdby == 0)
            {
                obj.Message = "Created by is required";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Title))
            {
                obj.Message = " Title is required";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Message))
            {
                obj.Message = " Message is required";
                obj.Tag = 0;
                return obj;
            }


            var association = new Notification()
            {
                Createdby= param.Createdby,
                Title = param.Title,
                Message = param.Message,
                Isread = param.Isread,
            };
            await _unitOfWork.NotificationRepository.SaveForm(association);
            obj.Tag = 1;
            obj.Data = association;
            return obj;
        }

        public async Task<TData<List<Notification>>> GetList(NotificationParam param)
        {
            var response = new TData<List<Notification>>();
            var obj = await _unitOfWork.NotificationRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Notification>>> GetList()
        {
            var response = new TData<List<Notification>>();
            var obj = await _unitOfWork.NotificationRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Notification>> GetEntity(int notificationId)
        {
            var response = new TData<Notification>();
            var obj = await _unitOfWork.NotificationRepository.GetEntity(notificationId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Notification>> DeleteEntity(int notificationId)
        {
            var response = new TData<Notification>();
            await _unitOfWork.NotificationRepository.DeleteForm(notificationId);
            response.Tag = 1;
            return response;
        }

    }
}
