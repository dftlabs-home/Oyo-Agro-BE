using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OyoAgro.DataAccess.Layer.Helpers;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;
using OyoAgro.DataAccess.Layer.Repositories.Base;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class UserRepository : DataRepository, IUserRepository
    {
        public async Task SaveForm(Useraccount entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Userid == 0)
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }

                //Userprofile userEntity = new()
                //{
                //    Firstname = entity.Firstname,
                //    Lastname = entity.Lastname,
                //    Email = entity.Email,
                //    Phonenumber = entity.Phonenumber,
                //    Middlename = entity.Middlename,
                //    Gender = entity.Gender,
                //    Residentialaddressid = entity.Residentialaddressid,
                //   // Lgaid = entity.lgaid,
                //    Userid = entity.Userid,
                //    Roleid = 2
                //};
                //await userEntity.Create();
                //await db.Insert(userEntity);

                await db.CommitTrans();
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;
            }
        }



        public async Task<Useraccount> GetUserByUserName(string username)
        {
            return await BaseRepository().FindEntity<Useraccount>(x => x.Username == username);
        }
        

        public async Task<Useraccount> GetUserByToken(string token)
        {
            return await BaseRepository().FindEntity<Useraccount>(x => x.Apitoken == token && x.Isactive == true);
        }

          public async Task<Useraccount> GetUserById(int UserId)
        {
            return await BaseRepository().FindEntity<Useraccount>(x => x.Userid == UserId);
        }

        public async Task<List<Userprofile>> GetList()
        {
            var list = await BaseRepository().FindList<Userprofile>();
            return list.ToList();
        }



        //public async Task<Useraccount> GetUser(string username)
        //{
        //    try
        //    {
        //        var User = await _context.Useraccounts.Where(x => x.Username.Trim().ToLower() == username.Trim().ToLower()).FirstOrDefaultAsync();
        //        return User;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public async Task<Useraccount> GetUserByToken(string token)
        //{
        //    try
        //    {
        //        var User = await _context.Useraccounts.Where(x => x.ApiToken == token).FirstOrDefaultAsync();
        //        return User;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public async Task UpdateUser(Useraccount entity)
        {
            await BaseRepository().Update(entity);
        }
       

    }
}
