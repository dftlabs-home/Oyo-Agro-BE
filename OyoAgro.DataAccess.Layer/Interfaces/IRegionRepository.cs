using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetList(RegionParam param);
        Task<List<Region>> GetList();
        Task<Region> GetEntity(int regionId);
        Task<List<Region>> GetListbyId(int regionId);
        Task DeleteForm(int ids);
        Task SaveForm(Region entity);

    }
}
