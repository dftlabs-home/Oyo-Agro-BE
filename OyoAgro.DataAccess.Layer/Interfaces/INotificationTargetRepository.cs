using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface INotificationTargetRepository
    {
        Task<List<Notificationtarget>> GetList(NotificationTargetParam param);
        Task<List<Notificationtarget>> GetList();
        Task DeleteForm(int ids);
        Task SaveForm(Notificationtarget entity);
        Task<Notificationtarget> GetEntity(int targetId);
    }
}
