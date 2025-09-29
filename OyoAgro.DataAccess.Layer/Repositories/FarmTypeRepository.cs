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
    public class FarmTypeRepository : DataRepository, IFarmTypeRepository
    {
        public async Task<List<Farmtype>> GetList(FarmTypeParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Farmtype>> GetList()
        {
            var list = await BaseRepository().FindList<Farmtype>();
            return list.ToList();
        }
     
        public async Task<Farmtype> GetEntity(int farmTypeId)
        {
            var list = await BaseRepository().FindEntity<Farmtype>(x => x.Farmtypeid == farmTypeId);
            return list;
        }


        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Farmtype>(ids);
        }

        public async Task SaveForm(Farmtype entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Farmtypeid == 0)
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



        private Expression<Func<Farmtype, bool>> ListFilter(FarmTypeParam param)
        {
            var expression = ExtensionLinq.True<Farmtype>();
            if (param != null)
            {
                if (param.Farmtypeid > 0)
                {
                    expression = expression.And(t => t.Farmtypeid == param.Farmtypeid);
                }

            }
            return expression;
        }

    }
}
