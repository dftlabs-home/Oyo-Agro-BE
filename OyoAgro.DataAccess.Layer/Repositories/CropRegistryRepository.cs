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
    public class CropRegistryRepository : DataRepository, ICropRegistryRepository
    {
        public async Task<List<Cropregistry>> GetList(CropRegistryParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Cropregistry>> GetList()
        {
            var list = await BaseRepository().FindList<Cropregistry>();
            return list.ToList();
        }
      
        public async Task<Cropregistry> GetEntity(int cropRegistryId)
        {
            var list = await BaseRepository().FindEntity<Cropregistry>(x => x.Cropregistryid == cropRegistryId);
            return list;
        }


        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Cropregistry>(ids);
        }

        public async Task SaveForm(Cropregistry entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Cropregistryid == 0)
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



        private Expression<Func<Cropregistry, bool>> ListFilter(CropRegistryParam param)
        {
            var expression = ExtensionLinq.True<Cropregistry>();
            if (param != null)
            {
                if (param.Cropregistryid > 0)
                {
                    expression = expression.And(t => t.Cropregistryid == param.Cropregistryid);
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
