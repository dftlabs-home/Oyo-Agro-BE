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
    public interface IRegionService
    {
        Task<TData<Region>> SaveEntity(RegionParam param);
        Task<TData<List<Region>>> GetList(RegionParam param);
        Task<TData<List<Region>>> GetList();
        Task<TData<Region>> GetEntity(int regionId);
        Task<TData<Lga>> DeleteEntity(int regionId);
    }
}
