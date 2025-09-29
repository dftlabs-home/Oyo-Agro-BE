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
    public interface ICropRegistryService
    {
        Task<TData<Cropregistry>> SaveEntity(CropRegistryParam param);
        Task<TData<List<Cropregistry>>> GetList(CropRegistryParam param);
        Task<TData<List<Cropregistry>>> GetList();
        Task<TData<Cropregistry>> GetEntity(int cropRegistryId);
        Task<TData<Cropregistry>> DeleteEntity(int cropRegistryId);

    }
}
