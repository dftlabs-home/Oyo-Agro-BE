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
    public class NotificationTargetRepository : DataRepository, INotificationTargetRepository
    {
        public async Task<List<Notificationtarget>> GetList(NotificationTargetParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }
        public async Task<Notificationtarget> GetEntity(int targetId)
        {
            var list = await BaseRepository().FindEntity<Notificationtarget>(x => x.Targetid == targetId);
            return list;
        }

        public async Task<List<Notificationtarget>> GetList()
        {
            var list = await BaseRepository().FindList<Notificationtarget>();
            return list.ToList();
        }
        
        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Address>(x => x.Addressid == ids);
        }

        public async Task SaveForm(Notificationtarget entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Targetid == 0)
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



        private Expression<Func<Notificationtarget, bool>> ListFilter(NotificationTargetParam param)
        {
            var expression = ExtensionLinq.True<Notificationtarget>();
            if (param != null)
            {
                if (param.Userid > 0)
                {
                    expression = expression.And(t => t.Userid == param.Userid);
                }

                if (param.Notificationid> 0)
                {
                    expression = expression.And(t => t.Regionid == param.Regionid);
                }

                if (param.Lgaid> 0)
                {
                    expression = expression.And(t => t.Lgaid == param.Lgaid);
                }

                     if (param.Regionid > 0)
                {
                    expression = expression.And(t => t.Regionid == param.Regionid);
                }



            }
            return expression;
        }

    }
}

