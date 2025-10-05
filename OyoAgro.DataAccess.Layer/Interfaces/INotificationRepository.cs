using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetList(NotificationParam param);
        Task<Notification> GetEntity(int notificationId);
        Task<List<Notification>> GetList();
        Task DeleteForm(int ids);
        Task SaveForm(Notification entity);
    }
}
