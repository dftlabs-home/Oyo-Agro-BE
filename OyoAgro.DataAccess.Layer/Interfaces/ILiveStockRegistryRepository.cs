using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface ILiveStockRegistryRepository
    {
        Task SaveForm(Livestockregistry entity);
        Task DeleteForm(int ids);
        Task<List<Livestockregistry>> GetList(LiveStockRegistryParam param);
        Task<List<Livestockregistry>> GetList();
        Task<Livestockregistry> GetEntity(int RegistryId);


    }
}
