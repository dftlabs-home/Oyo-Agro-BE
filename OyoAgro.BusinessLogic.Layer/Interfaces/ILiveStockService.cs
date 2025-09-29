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
    public interface ILiveStockService
    {
        Task<TData<Livestock>> DeleteEntity(int livestockId);
        Task<TData<Livestock>> GetEntity(int livestockId);
        Task<TData<List<Livestock>>> GetList(LivestockParam param);
        Task<TData<List<Livestock>>> GetList();
        Task<TData<Livestock>> SaveEntity(LivestockParam param);
    }
}
