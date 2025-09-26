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
    public interface IProfileActivityParentService
    {
        Task<TData<Profileactivityparent>> SaveEntity(ProfileActivityParentParam profileactivity);
        Task<TData<Profileactivityparent>> GetList(ProfileActivityParentParam param);
        Task<TData<Profileactivityparent>> GetList();
        Task<TData<Profileactivityparent>> GetEntity(int parentId);
    }
}
