using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Interfaces
{
    public interface IUserService
    {
        Task<TData<string>> CheckLogin(string userName, string password);
        Task<TData<Useraccount>> GetUserByToken(string token);
        Task<TData<Useraccount>> Logout(int userId);
        string GetPasswordSalt();
        Task<TData<string>> SaveForm(UserParam entity);
        Task<TData<List<Userprofile>>> GetList();
    }
}
