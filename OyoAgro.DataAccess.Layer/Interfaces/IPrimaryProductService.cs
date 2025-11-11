using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IPrimaryProductService
    {
        Task<TData<PrimaryProduct>> SaveEntity(PrimaryProductParam param);
        Task<TData<PrimaryProduct>> UpdateEntity(PrimaryProductParam param);
        Task<TData<List<PrimaryProduct>>> GetList();
        Task<TData<PrimaryProduct>> GetEntity(int primaryProductId);
        Task<TData<PrimaryProduct>> DeleteEntity(int id);
    }
}
