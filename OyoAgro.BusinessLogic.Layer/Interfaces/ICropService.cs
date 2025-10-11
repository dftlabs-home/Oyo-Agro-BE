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
    public interface ICropService
    {
        Task<TData<Crop>> SaveEntity(CropParam param);
        Task<TData<List<Crop>>> GetList(CropParam param);
        Task<TData<List<Crop>>> GetList();
        Task<TData<Crop>> GetEntity(int cropId);
        Task<TData<Crop>> DeleteEntity(int cropId);
        Task<TData<Crop>> UpdateEntity(CropParam param);

    }
}
