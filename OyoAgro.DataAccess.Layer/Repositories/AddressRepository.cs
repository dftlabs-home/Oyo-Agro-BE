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
    public class AddressRepository : DataRepository, IAddressRepository
    {
        public async Task<List<Address>> GetList(AddressParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Address>> GetList()
        {
            var list = await BaseRepository().FindList<Address>();
            return list.ToList();
        }
        public async Task<List<Address>> GetListbyFarmId(int farmId)
        {
            var list = await BaseRepository().FindList<Address>(x => x.Farmid == farmId);
            return list.ToList();
        }
        public async Task<List<Address>> GetListbyFarmerId(int farmerId)
        {
            var list = await BaseRepository().FindList<Address>(x => x.Farmerid == farmerId);
            return list.ToList();
        }
        public async Task<List<Address>> GetListbyUserId(int userId)
        {
            var list = await BaseRepository().FindList<Address>(x => x.Userid == userId);
            return list.ToList();
        }

        public async Task<Address> GetEntity(int addressId)
        {
            var list = await BaseRepository().FindEntity<Address>(x => x.Addressid == addressId);
            return list;
        }





        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Address>(ids);
        }

        public async Task SaveForm(Address entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Addressid == 0)
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



        private Expression<Func<Address, bool>> ListFilter(AddressParam param)
        {
            var expression = ExtensionLinq.True<Address>();
            if (param != null)
            {
                if (param.Userid > 0)
                {
                    expression = expression.And(t => t.Userid == param.Userid);
                }

                if (param.Farmerid > 0)
                {
                    expression = expression.And(t => t.Farmerid == param.Farmerid);
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
