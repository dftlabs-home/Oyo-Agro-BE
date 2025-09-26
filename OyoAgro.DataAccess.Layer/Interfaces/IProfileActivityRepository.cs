using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IProfileActivityRepository
    {
        Task<List<Profileactivity>> GetList(ProfileActivityParam param);
        Task<List<Profileactivity>> GetList();
        Task<List<Profileactivity>> GetListParentId(int ParentId);
        Task<List<Profileactivity>> GetListByActivity(int activityId);
        Task DeleteForm(int ids);
        Task SaveForm(Profileactivity entity);
        Task<Profileactivity> GetEntity(int activityId);
    }
}
