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
    public class SeasonRepository : DataRepository, ISeasonRepository
    {

        public async Task<List<Season>> GetList(SeasonParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Season>> GetList()
        {
            var list = await BaseRepository().FindList<Season>();
            return list.ToList();
        }

        public async Task<Season> GetEntity(int seasonId)
        {
            var list = await BaseRepository().FindEntity<Season>(x => x.Seasonid == seasonId);
            return list;
        }


        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Season>(ids);
        }

        public async Task SaveForm(Season entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Seasonid == 0)
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



        private Expression<Func<Season, bool>> ListFilter(SeasonParam param)
        {
            var expression = ExtensionLinq.True<Season>();
            if (param != null)
            {
                if (param.Seasonid > 0)
                {
                    expression = expression.And(t => t.Seasonid == param.Seasonid);
                }

            }
            return expression;
        }

    }
}
