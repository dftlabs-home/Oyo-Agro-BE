using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface ICropRegistryRepository
    {
        Task<List<Cropregistry>> GetList();
        Task<List<Cropregistry>> GetList(CropRegistryParam param);
        Task<Cropregistry> GetEntity(int cropRegistryId);
        Task DeleteForm(int ids);
        Task SaveForm(Cropregistry entity);

    }
}
