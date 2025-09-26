using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;
using OyoAgro.DataAccess.Layer.Models.ViewModels;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IProfileActivityParentRepository
    {
        Task SaveForm(Profileactivityparent entity);
        Task DeleteForm(string ids);
        Task<List<Profileactivityparent>> GetList(ProfileActivityParentParam param);
        Task<List<Profileactivityparent>> GetList();
        Task<Profileactivityparent> GetEntity(int activityParentId);
        Task<List<Profileactivityparent>> GetListParentId(int ParentId);
        Task<List<Profileactivityparent>> GetListByActivity(int activityParentId);
        Task<List<Profileactivityparent>> GetPageList(ProfileActivityParentParam param, Pagination pagination);
    }
}
