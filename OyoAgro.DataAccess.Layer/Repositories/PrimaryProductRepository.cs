using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class PrimaryProductRepository : DataRepository, IPrimaryProductRepository
    {
        public async Task<List<PrimaryProduct>> GetList()
        {
            var list = await BaseRepository().FindList<PrimaryProduct>();
            return list.ToList();
        }


        public async Task<PrimaryProduct> GetEntity(int primaryProductId)
        {
            var list = await BaseRepository().FindEntity<PrimaryProduct>(x => x.PrimaryProductTypeId == primaryProductId);
            return list;
        }





        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<PrimaryProduct>(x => x.PrimaryProductTypeId == ids);
        }

        public async Task SaveForm(PrimaryProduct entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.PrimaryProductTypeId == 0)
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
