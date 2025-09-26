using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Extensions;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;
using OyoAgro.DataAccess.Layer.Models.Params;
using OyoAgro.DataAccess.Layer.Models.ViewModels;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class LgaRepository : DataRepository, ILgaRepository
    {
        public async Task<List<Lga>> GetList(LgaParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Lga>> GetList()
        {
            var list = await BaseRepository().FindList<Lga>();
            return list.ToList();
        }

        public async Task<Lga> GetEntity(int lgaId)
        {
            var list = await BaseRepository().FindEntity<Lga>(x => x.Lgaid == lgaId);
            return list;
        }



        public async Task<List<Lga>> GetListLgaId(int lgaId)
        {
            var list = await BaseRepository().FindList<Lga>(x => x.Lgaid == lgaId);
            return list.ToList();
        }

        public async Task<List<Lga>> GetListByRegionId(int regionId)
        {
            var list = await BaseRepository().FindList<Lga>(x => x.Regionid == regionId);
            return list.ToList();
        }
       
        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Lga>(ids);
        }

        public async Task SaveForm(Lga entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Lgaid == 0)
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



        private Expression<Func<Lga, bool>> ListFilter(LgaParam param)
        {
            var expression = ExtensionLinq.True<Lga>();
            if (param != null)
            {
                if (param.Lgaid > 0)
                {
                    expression = expression.And(t => t.Lgaid == param.Lgaid);
                }

                  if (param.Regionid > 0)
                {
                    expression = expression.And(t => t.Lgaid == param.Regionid);
                }


            }
            return expression;
        }

    }
}
