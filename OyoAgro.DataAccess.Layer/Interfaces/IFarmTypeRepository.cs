using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IFarmTypeRepository
    {
        Task<List<Farmtype>> GetList(FarmTypeParam param);
        Task<List<Farmtype>> GetList();
        Task<Farmtype> GetEntity(int farmTypeId);
        Task DeleteForm(int ids);
        Task SaveForm(Farmtype entity);
    }
}
