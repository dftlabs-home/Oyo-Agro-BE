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
    public interface IFarmTypeService
    {
        Task<TData<Farmtype>> SaveEntity(FarmTypeParam param);
        Task<TData<List<Farmtype>>> GetList(FarmTypeParam param);
        Task<TData<List<Farmtype>>> GetList();
        Task<TData<Farmtype>> GetEntity(int farmTypeId);
        Task<TData<Farmtype>> DeleteEntity(int farmTypeId);
    }
}
