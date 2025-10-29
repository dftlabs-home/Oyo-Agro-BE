using System;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Interfaces;

namespace OyoAgro.BusinessLogic.Layer.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardCountsDto> GetGlobalCountsAsync();
        //Task<DashboardCountsDto> GetUserCountsAsync(int userId);
        //Task<DashboardCountsDto> GetFarmerCountsAsync(int farmerId);
        //Task<DashboardCountsDto> GetFarmCountsAsync(int farmId);
    }

    public class DashboardCountsDto
    {
        public int TotalFarmers { get; set; }
        public int TotalFarms { get; set; }
        public int TotalCropRegistrations { get; set; }
        public int TotalLivestockRegistrations { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
