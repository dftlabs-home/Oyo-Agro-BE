using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IUserRegionRepository
    {
        Task<List<Userregion>> GetList(UserRegionParam param);
        Task<List<Userregion>> GetList();
        Task<Userregion> GetEntity(int userRegionId);
        Task<Userregion> GetEntityByUserId(int userId);
        Task<Userregion> GetEntityByRegionId(int regionId);
        Task DeleteForm(int ids);
        Task SaveForm(Userregion entity);
    }
}
