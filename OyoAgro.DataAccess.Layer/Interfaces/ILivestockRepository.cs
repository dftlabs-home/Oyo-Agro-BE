using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface ILivestockRepository
    {
        Task<List<Livestock>> GetList(LivestockParam param);
        Task<List<Livestock>> GetList();
        Task<Livestock> GetEntity(int livestockId);
        Task DeleteForm(int ids);
        Task SaveForm(Livestock entity);
    }
}
