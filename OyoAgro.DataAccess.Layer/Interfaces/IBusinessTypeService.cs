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
    public interface IBusinessTypeService
    {
        Task<TData<BusinessType>> SaveEntity(BusinessTypeParam param);
        Task<TData<BusinessType>> UpdateEntity(BusinessTypeParam param);
        Task<TData<List<BusinessType>>> GetList();
        Task<TData<BusinessType>> GetEntity(int businessTypeId);
        Task<TData<BusinessType>> DeleteEntity(int id);
    }
        
}
