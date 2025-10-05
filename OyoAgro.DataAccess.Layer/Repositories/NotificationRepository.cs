using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Extensions;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class NotificationRepository : DataRepository, INotificationRepository
    {
        public async Task<List<Notification>> GetList(NotificationParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }
        public async Task<Notification> GetEntity(int notificationId)
        {
            var list = await BaseRepository().FindEntity<Notification>(x => x.Notificationid == notificationId);
            return list;
        }

        public async Task<List<Notification>> GetList()
        {
            var list = await BaseRepository().FindList<Notification>();
            return list.ToList();
        }

        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Notification>(x => x.Notificationid == ids);
        }

        public async Task SaveForm(Notification entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Notificationid == 0)
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }



                await db.CommitTrans();
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;
            }
        }



        private Expression<Func<Notification, bool>> ListFilter(NotificationParam param)
        {
            var expression = ExtensionLinq.True<Notification>();
            if (param != null)
            {
                if (param.Createdby > 0)
                {
                    expression = expression.And(t => t.Createdby == param.Createdby);
                }

                if (param.Notificationid > 0)
                {
                    expression = expression.And(t => t.Notificationid == param.Notificationid);
                }


            }
            return expression;
        }

    }
}
