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
    public class LiveStockRegistryRepository : DataRepository, ILiveStockRegistryRepository
    {
        public async Task<List<Livestockregistry>> GetList(LiveStockRegistryParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Livestockregistry>> GetList()
        {
            var list = await BaseRepository().FindList<Livestockregistry>();
            return list.ToList();
        }

        public async Task<Livestockregistry> GetEntity(int RegistryId)
        {
            var list = await BaseRepository().FindEntity<Livestockregistry>(x => x.Livestockregistryid == RegistryId);
            return list;
        }


        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Livestockregistry>(ids);
        }

        public async Task SaveForm(Livestockregistry entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Livestockregistryid == 0)
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



        private Expression<Func<Livestockregistry, bool>> ListFilter(LiveStockRegistryParam param)
        {
            var expression = ExtensionLinq.True<Livestockregistry>();
            if (param != null)
            {
                if (param.Livestockregistryid > 0)
                {
                    expression = expression.And(t => t.Livestockregistryid == param.Livestockregistryid);
                }
                if (param.Farmid > 0)
                {
                    expression = expression.And(t => t.Farmid == param.Farmid);
                }

            }
            return expression;
        }

    }
}
