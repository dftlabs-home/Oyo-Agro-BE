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
    public interface IProfileAdditionalActivityRepository
    {
        Task SaveForm(Profileadditionalactivity entity);
        Task<List<Profileadditionalactivity>> GetList(ProfileadditionalactivityParam param);
        Task DeleteForm(string ids);
        Task<List<Profileadditionalactivity>> GetList();
        Task<List<Profileadditionalactivity>> GetListByUser(int userId);
        Task<List<Profileadditionalactivity>> GetListByActivity(int activityId);
        Task<List<Profileadditionalactivity>> GetPageList(ProfileadditionalactivityParam param, Pagination pagination);
    }
}
