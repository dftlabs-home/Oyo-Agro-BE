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
    public interface ISeasonServices
    {
        Task<TData<Season>> SaveEntity(SeasonParam param);
        Task<TData<Season>> UpdateEntity(SeasonParam param);
        Task<TData<List<Season>>> GetList(SeasonParam param);
        Task<TData<List<Season>>> GetList();
        Task<TData<Season>> GetEntity(int seasonId);
        Task<TData<Season>> DeleteEntity(int seasonId);


    }
}
