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
    public interface IAgroAlliedRegistryService
    {
        Task<TData<AgroAlliedRegistry>> SaveEntity(AgroAlliedRegistryParam param);
        Task<TData<List<AgroAlliedRegistry>>> GetList(AgroAlliedRegistryParam param);
        Task<TData<List<AgroAlliedRegistry>>> GetList();
        Task<TData<AgroAlliedRegistry>> GetEntity(int registryId);
        Task<TData<AgroAlliedRegistry>> DeleteEntity(int registryId);
        Task<TData<List<AgroAlliedRegistry>>> GetListByFarm(long FarmId);

    }
}
