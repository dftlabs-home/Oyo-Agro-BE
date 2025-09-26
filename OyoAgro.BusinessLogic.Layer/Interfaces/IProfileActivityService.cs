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
    public interface IProfileActivityService
    {
        Task<TData<Profileactivity>> SaveEntity(ProfileActivityParam profileactivity);
        Task<TData<Profileactivity>> GetList(ProfileActivityParam param);
        Task<TData<Profileactivity>> GetList();
        Task<TData<Profileactivity>> GetEntity(int activityId);
    }
}
