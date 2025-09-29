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
    public class CropRepository : DataRepository, ICropRepository
    {
        public async Task<List<Crop>> GetList(CropParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Crop>> GetList()
        {
            var list = await BaseRepository().FindList<Crop>();
            return list.ToList();
        }
     

        public async Task<Crop> GetEntity(int croptypeId)
        {
            var list = await BaseRepository().FindEntity<Crop>(x => x.Croptypeid == croptypeId);
            return list;
        }





        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Crop>(ids);
        }

        public async Task SaveForm(Crop entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Croptypeid == 0)
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



        private Expression<Func<Crop, bool>> ListFilter(CropParam param)
        {
            var expression = ExtensionLinq.True<Crop>();
            if (param != null)
            {
                if (param.Croptypeid > 0)
                {
                    expression = expression.And(t => t.Croptypeid == param.Croptypeid);
                }



            }
            return expression;
        }

    }
}
