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
    public interface ILgaServices
    {
        Task<TData<Lga>> SaveEntity(LgaParam param);
        Task<TData<List<Lga>>> GetList(LgaParam param);
        Task<TData<List<Lga>>> GetList();
        Task<TData<Lga>> GetEntity(int lgaId);
        Task<TData<Lga>> DeleteEntity(int lgaId);
        Task<TData<List<Lga>>> GetEntityByRegionId(int regionId);
    }
}
