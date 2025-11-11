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
    public class BusinessTypeRepository: DataRepository, IBusinessTypeRepository
    {
      

        public async Task<List<BusinessType>> GetList()
        {
            var list = await BaseRepository().FindList<BusinessType>();
            return list.ToList();
        }


        public async Task<BusinessType> GetEntity(int businessTypeId)
        {
            var list = await BaseRepository().FindEntity<BusinessType>(x => x.BusinessTypeId == businessTypeId);
            return list;
        }





        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<BusinessType>(x => x.BusinessTypeId == ids);
        }

        public async Task SaveForm(BusinessType entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.BusinessTypeId == 0)
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



     

    }
}
