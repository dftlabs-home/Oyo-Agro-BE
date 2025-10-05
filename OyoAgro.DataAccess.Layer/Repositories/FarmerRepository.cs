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
    public class FarmerRepository : DataRepository, IFarmerRepository
    {
        public async Task<List<Farmer>> GetList(FarmerParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Farmer>> GetList()
        {
            var list = await BaseRepository().FindList<Farmer>();
            return list.ToList();
        }

        public async Task<Farmer> GetEntity(int farmerId)
        {
            var list = await BaseRepository().FindEntity<Farmer>(x => x.Farmerid == farmerId);
            return list;
        }

        public async Task<Farmer> GetEntitybyEmail(string email)
        {
            var list = await BaseRepository().FindEntity<Farmer>(x => x.Email== email);
            return list;
        }


         public async Task<List<Farmer>> GetEntitybyUserId(int userId)
        {
            var result = await BaseRepository().FindList<Farmer>(x => x.UserId== userId);
            return result.ToList();
        }

        public async Task<Farmer> GetEntitybyPhonel(string phone)
        {
            var list = await BaseRepository().FindEntity<Farmer>(x => x.Phonenumber == phone);
            return list;
        }




        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Farmer>(x=> x.Farmerid == ids);
        }

        public async Task SaveForm(Farmer entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Farmerid == 0)
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



        private Expression<Func<Farmer, bool>> ListFilter(FarmerParam param)
        {
            var expression = ExtensionLinq.True<Farmer>();
            if (param != null)
            {
                if (param.Farmerid > 0)
                {
                    expression = expression.And(t => t.Farmerid == param.Farmerid);
                }



            }
            return expression;
        }

    }
}
