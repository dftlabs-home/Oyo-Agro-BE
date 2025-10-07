using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IUserRepository
    {
        Task<Useraccount> GetUserByUserName(string username);
        Task<Useraccount> GetUserByToken(string token);
        //Task<Useraccount> LoginOff(int userId);
        Task SaveForm(Useraccount entity);
        Task UpdateUser(Useraccount entity);
        Task<Useraccount> GetUserById(int UserId);
        Task<List<Userprofile>> GetList();
    }
}
        