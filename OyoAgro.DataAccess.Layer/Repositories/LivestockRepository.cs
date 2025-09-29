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
    public class LivestockRepository : DataRepository, ILivestockRepository
    {
        public async Task<List<Livestock>> GetList(LivestockParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Livestock>> GetList()
        {
            var list = await BaseRepository().FindList<Livestock>();
            return list.ToList();
        }

        public async Task<Livestock> GetEntity(int livestockId)
        {
            var list = await BaseRepository().FindEntity<Livestock>(x => x.Livestocktypeid== livestockId);
            return list;
        }


        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Livestock>(ids);
        }

        public async Task SaveForm(Livestock entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Livestocktypeid == 0)
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



        private Expression<Func<Livestock, bool>> ListFilter(LivestockParam param)
        {
            var expression = ExtensionLinq.True<Livestock>();
            if (param != null)
            {
                if (param.Livestocktypeid > 0)
                {
                    expression = expression.And(t => t.Livestocktypeid == param.Livestocktypeid);
                }

            }
            return expression;
        }

    }
}
