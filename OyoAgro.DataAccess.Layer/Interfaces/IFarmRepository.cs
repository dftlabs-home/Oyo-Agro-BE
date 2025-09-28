using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IFarmRepository
    {
        Task<List<Farm>> GetList(FarmParam param);
        Task<List<Farm>> GetList();
        Task<Farm> GetEntity(int farmId);
        Task DeleteForm(int ids);
        Task SaveForm(Farm entity);
        Task<List<Farm>> GetListbyFarmerId(int farmerId);
    }
}
