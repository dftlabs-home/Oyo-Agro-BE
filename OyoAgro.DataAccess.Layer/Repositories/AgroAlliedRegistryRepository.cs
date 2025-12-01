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
    public class AgroAlliedRegistryRepository: DataRepository, IAgroAlliedRegistryRepository
    {
        public async Task<List<AgroAlliedRegistry>> GetList(AgroAlliedRegistryParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AgroAlliedRegistry>> GetList()
        {
            try
            {
                var list = await BaseRepository().FindList<AgroAlliedRegistry>();
                return list.ToList();
            }
            catch (Exception ex )
            {

                throw;
            }
        }

        public async Task<AgroAlliedRegistry> GetEntity(int RegistryId)
        {
            var list = await BaseRepository().FindEntity<AgroAlliedRegistry>(x => x.AgroAlliedRegistryid == RegistryId);
            return list;
        }
        public async Task<List<AgroAlliedRegistry>> GetEntitybyFarmId(long FarmId)
        {
            var list = await BaseRepository().FindList<AgroAlliedRegistry>(x => x.Farmid == FarmId);
            return list.ToList();
        }


        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<AgroAlliedRegistry>(x => x.AgroAlliedRegistryid == ids);
        }

        public async Task SaveForm(AgroAlliedRegistry entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.AgroAlliedRegistryid == 0)
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



        private Expression<Func<AgroAlliedRegistry, bool>> ListFilter(AgroAlliedRegistryParam param)
        {
            var expression = ExtensionLinq.True<AgroAlliedRegistry>();
            if (param != null)
            {
                if (param.AgroAlliedRegistryid > 0)
                {
                    expression = expression.And(t => t.AgroAlliedRegistryid == param.AgroAlliedRegistryid);
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
