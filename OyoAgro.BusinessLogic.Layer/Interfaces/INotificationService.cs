using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Interfaces
{

    public interface INotificationService
    {
        Task<TData<Notification>> SaveEntity(NotificationParam param);
        Task<TData<List<Notification>>> GetList(NotificationParam param);
        Task<TData<List<Notification>>> GetList();
        Task<TData<Notification>> GetEntity(int notificationId);
        Task<TData<Notification>> DeleteEntity(int notificationId);

    }
}
