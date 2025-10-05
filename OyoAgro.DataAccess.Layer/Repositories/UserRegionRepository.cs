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
    public class UserRegionRepository : DataRepository, IUserRegionRepository
    {
        public async Task<List<Userregion>> GetList(UserRegionParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Userregion>> GetList()
        {
            var list = await BaseRepository().FindList<Userregion>();
            return list.ToList();
        }
      
        public async Task<Userregion> GetEntity(int userRegionId)
        {
            var list = await BaseRepository().FindEntity<Userregion>(x => x.Userregionid == userRegionId);
            return list;
        }

         public async Task<Userregion> GetEntityByUserId(int userId)
        {
            var list = await BaseRepository().FindEntity<Userregion>(x => x.Userid == userId);
            return list;
        }

         public async Task<Userregion> GetEntityByRegionId(int regionId)
        {
            var list = await BaseRepository().FindEntity<Userregion>(x => x.Regionid == regionId);
            return list;
        }





        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Userregion>(x=> x.Userregionid == ids);
        }

        public async Task SaveForm(Userregion entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Userregionid == 0)
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



        private Expression<Func<Userregion, bool>> ListFilter(UserRegionParam param)
        {
            var expression = ExtensionLinq.True<Userregion>();
            if (param != null)
            {
                if (param.Userregionid > 0)
                {
                    expression = expression.And(t => t.Userregionid == param.Userregionid);
                }

                if (param.Userid > 0)
                {
                    expression = expression.And(t => t.Userid == param.Userid);
                }

              
            }
            return expression;
        }


    }
}
