using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface ILgaRepository
    {
        Task<List<Lga>> GetList(LgaParam param);
        Task<List<Lga>> GetList();
        Task<Lga> GetEntity(int lgaId);
        Task<List<Lga>> GetListLgaId(int lgaId);
        Task<List<Lga>> GetListByRegionId(int regionId);
        Task DeleteForm(int ids);
        Task SaveForm(Lga entity);
    }
}
