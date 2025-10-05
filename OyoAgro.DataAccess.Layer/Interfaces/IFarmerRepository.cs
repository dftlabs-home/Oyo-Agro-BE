using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IFarmerRepository
    {
        Task<List<Farmer>> GetList(FarmerParam param);
        Task<List<Farmer>> GetList();
        Task<Farmer> GetEntity(int farmerId);
        Task DeleteForm(int ids);
        Task SaveForm(Farmer entity);
        Task<List<Farmer>> GetEntitybyUserId(int userId);
        Task<Farmer> GetEntitybyEmail(string email);
        Task<Farmer> GetEntitybyPhonel(string phone);
    }
}
