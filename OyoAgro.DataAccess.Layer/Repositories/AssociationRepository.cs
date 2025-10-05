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
    public class AssociationRepository : DataRepository, IAssociationRepository
    {
        public async Task<List<Association>> GetList(AssociationParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Association>> GetList()
        {
            var list = await BaseRepository().FindList<Association>();
            return list.ToList();
        }
        public async Task<List<Association>> GetListbyAssId(int associationId)
        {
            var list = await BaseRepository().FindList<Association>(x => x.Associationid== associationId);
            return list.ToList();
        }
      
        public async Task<Association> GetEntity(int associationId)
        {
            var list = await BaseRepository().FindEntity<Association>(x => x.Associationid == associationId);
            return list;
        }





        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Association>(x=> x.Associationid == ids);
        }

        public async Task SaveForm(Association entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Associationid == 0)
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



        private Expression<Func<Association, bool>> ListFilter(AssociationParam param)
        {
            var expression = ExtensionLinq.True<Association>();
            if (param != null)
            {
                if (param.Associationid > 0)
                {
                    expression = expression.And(t => t.Associationid == param.Associationid);
                }

            

            }
            return expression;
        }

    }
}
