using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IPrimaryProductRepository
    {
        Task<List<PrimaryProduct>> GetList();
        Task<PrimaryProduct> GetEntity(int primaryProductId);
        Task DeleteForm(int ids);
        Task SaveForm(PrimaryProduct entity);
    }
}
