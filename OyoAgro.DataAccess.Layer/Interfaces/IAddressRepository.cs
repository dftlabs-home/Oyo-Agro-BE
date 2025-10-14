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
        Task<Address> GetEntitybyFarmId(int farmId);
        Task<Address> GetEntitybyFarmerId(int farmerId);
        Task<Address> GetEntitybyUserId(int userId);
        Task<Address> GetEntity(int addressId);
        Task DeleteForm(int ids);
        Task SaveForm(Address entity);


    }
}
