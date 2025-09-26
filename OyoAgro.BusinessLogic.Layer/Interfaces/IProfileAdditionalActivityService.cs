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
    public interface IProfileAdditionalActivityService
    {
        Task<TData<Profileadditionalactivity>> SaveEntity(ProfileadditionalactivityParam profileactivity);
        Task<TData<Profileadditionalactivity>> GetList(ProfileadditionalactivityParam param);
        Task<TData<Profileadditionalactivity>> GetList();
        Task<TData<Profileadditionalactivity>> GetEntity(int profileAdditionalActivityId);
        Task<TData> DeleteEntity(int profileAdditionalActivityId);


    }
}
