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
    public interface ILiveStockRegistryService
    {
        Task<TData<Livestockregistry>> DeleteEntity(int registryId);
        Task<TData<Livestockregistry>> GetEntity(int registryId);
        Task<TData<List<Livestockregistry>>> GetList();
        Task<TData<List<Livestockregistry>>> GetList(LiveStockRegistryParam param);
        Task<TData<Livestockregistry>> SaveEntity(LiveStockRegistryParam param);
    }
}
