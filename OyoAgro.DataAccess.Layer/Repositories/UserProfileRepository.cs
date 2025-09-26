using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class UserProfileRepository : DataRepository, IUserProfileRepository
    {
        //private readonly AppDbContext _context;

        //public UserProfileRepository(AppDbContext Context)
        //{
        //    _context = Context;
        //}

        public async Task SaveForm(Userprofile entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Userprofileid == 0)
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




        public async Task<Userprofile> GetUserProfile(int UserId)
        {
            return await BaseRepository().FindEntity<Userprofile>(x => x.Userid == UserId);
        }

          public async Task<Userprofile> GetUserByPhone(string Mobile)
        {
            return await BaseRepository().FindEntity<Userprofile>(x => x.Phonenumber == Mobile);
        }

       

        //public async Task<Userprofile> GetUserProfile(int UserId)
        //{
        //    try
        //    {
        //        var User = await _context.Userprofiles.Where(x => x.Userid == UserId).FirstOrDefaultAsync();
        //        return User;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}


    }
}
