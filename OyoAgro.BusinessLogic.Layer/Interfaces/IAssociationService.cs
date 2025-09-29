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
    public interface IAssociationService
    {
        Task<TData<Association>> SaveEntity(AssociationParam param);
        Task<TData<List<Association>>> GetList(AssociationParam param);
        Task<TData<List<Association>>> GetList();
        Task<TData<Association>> GetEntity(int associationId);
        Task<TData<Association>> DeleteEntity(int associationId);
    }
}
