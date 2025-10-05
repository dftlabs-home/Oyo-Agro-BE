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
    public interface IFarmerSevice
    {
        Task<TData<Farmer>> SaveEntity(FarmerParam param);
        Task<TData<List<Farmer>>> GetList(FarmerParam param);
        Task<TData<List<Farmer>>> GetList();
        Task<TData<Farmer>> DeleteEntity(int farmerId);
        Task<TData<Farmer>> GetEntity(int farmerId);
        Task<TData<List<Farmer>>> GetListByUserId(int userId);
    }
}
