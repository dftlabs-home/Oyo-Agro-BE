using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IBusinessTypeRepository
    {
        Task<List<BusinessType>> GetList();
        Task<BusinessType> GetEntity(int businessTypeId);
        Task DeleteForm(int ids);
        Task SaveForm(BusinessType entity);
    }
}
