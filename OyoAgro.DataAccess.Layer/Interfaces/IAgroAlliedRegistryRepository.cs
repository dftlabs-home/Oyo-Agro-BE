using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IAgroAlliedRegistryRepository
    {
        Task<List<AgroAlliedRegistry>> GetList(AgroAlliedRegistryParam param);
        Task<List<AgroAlliedRegistry>> GetList();
        Task<AgroAlliedRegistry> GetEntity(int RegistryId);
        Task DeleteForm(int ids);
        Task SaveForm(AgroAlliedRegistry entity);
        Task<List<AgroAlliedRegistry>> GetEntitybyFarmId(long FarmId);

    }
}
