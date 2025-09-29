using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetList(AddressParam param);
        Task<List<Address>> GetList();
        Task<List<Address>> GetListbyFarmId(int farmId);
        Task<List<Address>> GetListbyFarmerId(int farmerId);
        Task<List<Address>> GetListbyUserId(int userId);
        Task<Address> GetEntity(int addressId);
        Task DeleteForm(int ids);
        Task SaveForm(Address entity);


    }
}
