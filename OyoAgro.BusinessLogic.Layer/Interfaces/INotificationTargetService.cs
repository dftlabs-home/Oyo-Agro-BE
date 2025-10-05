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
    public  interface INotificationTargetService
    {
        Task<TData<Notificationtarget>> SaveEntity(NotificationTargetParam param);
        Task<TData<List<Notificationtarget>>> GetList(NotificationTargetParam param);
        Task<TData<List<Notificationtarget>>> GetList();
        Task<TData<Notificationtarget>> GetEntity(int targetId);
        Task<TData<Notificationtarget>> DeleteEntity(int targetId);

    }
}
