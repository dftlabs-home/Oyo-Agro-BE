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
    public class RegionRepository : DataRepository, IRegionRepository
    {
        public async Task<List<Region>> GetList(RegionParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Region>> GetList()
        {
            var list = await BaseRepository().FindList<Region>();
            return list.ToList();
        }

        public async Task<Region> GetEntity(int regionId)
        {
            var list = await BaseRepository().FindEntity<Region>(x => x.Regionid == regionId);
            return list;
        }



        public async Task<List<Region>> GetListbyId(int regionId)
        {
            var list = await BaseRepository().FindList<Region>(x => x.Regionid == regionId);
            return list.ToList();
        }

       

        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Region>(x=> x.Regionid == ids);
        }

        public async Task SaveForm(Region entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Regionid == 0)
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



        private Expression<Func<Region, bool>> ListFilter(RegionParam param)
        {
            var expression = ExtensionLinq.True<Region>();
            if (param != null)
            {
                if (param.Regionid > 0)
                {
                    expression = expression.And(t => t.Regionid == param.Regionid);
                }

               

            }
            return expression;
        }

    }
}
