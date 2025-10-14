using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;
using OyoAgro.DataAccess.Layer.Models.ViewModels;

namespace OyoAgro.BusinessLogic.Layer.Interfaces
{
    public interface IFarmService
    {
        Task<TData<Farm>> SaveEntity(FarmParam param);
        Task<TData<List<Farm>>> GetList(FarmParam param);
        Task<TData<List<Farm>>> GetList();
        Task<TData<List<Farm>>> GetListByFarmerId(int farmerId);
        Task<TData<FarmViewModel>> GetEntity(int farmId);
        Task<TData<Farm>> DeleteEntity(int farmId);


    }
}
