using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface ICropRepository
    {
        Task<List<Crop>> GetList(CropParam param);
        Task<List<Crop>> GetList();
        Task<Crop> GetEntity(int croptypeId);   
        Task DeleteForm(int ids);
        Task SaveForm(Crop entity);
    }
}
