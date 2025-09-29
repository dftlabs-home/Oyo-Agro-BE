using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface ISeasonRepository
    {
        Task<List<Season>> GetList(SeasonParam param);
        Task<List<Season>> GetList();
        Task<Season> GetEntity(int seasonId);
        Task DeleteForm(int ids);
        Task SaveForm(Season entity);

    }
}
