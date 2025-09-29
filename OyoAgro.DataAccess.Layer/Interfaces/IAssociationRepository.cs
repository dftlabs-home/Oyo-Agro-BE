using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IAssociationRepository
    {
        Task<List<Association>> GetList(AssociationParam param);
        Task<List<Association>> GetList();
        Task<List<Association>> GetListbyAssId(int associationId);
        Task<Association> GetEntity(int associationId);
        Task DeleteForm(int ids);
        Task SaveForm(Association entity);
    }
}
