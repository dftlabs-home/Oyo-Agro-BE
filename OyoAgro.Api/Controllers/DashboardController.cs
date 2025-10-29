using Microsoft.AspNetCore.Mvc;
using OyoAgro.BusinessLogic.Layer.Interfaces;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Get global dashboard counts
        /// </summary>
        /// <returns>Global dashboard counts</returns>
        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalCounts()
        {
            try
            {
                var counts = await _dashboardService.GetGlobalCountsAsync();
                return Ok(new { success = true, data = counts });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get user-specific dashboard counts
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>User-specific dashboard counts</returns>
        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> GetUserCounts(int userId)
        //{
        //    try
        //    {
        //        var counts = await _dashboardService.GetUserCountsAsync(userId);
        //        return Ok(new { success = true, data = counts });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { success = false, message = ex.Message });
        //    }
        //}

        /// <summary>
        /// Get farmer-specific dashboard counts
        /// </summary>
        /// <param name="farmerId">Farmer ID</param>
        /// <returns>Farmer-specific dashboard counts</returns>
        //[HttpGet("farmer/{farmerId}")]
        //public async Task<IActionResult> GetFarmerCounts(int farmerId)
        //{
        //    try
        //    {
        //        var counts = await _dashboardService.GetFarmerCountsAsync(farmerId);
        //        return Ok(new { success = true, data = counts });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { success = false, message = ex.Message });
        //    }
        //}

        /// <summary>
        /// Get farm-specific dashboard counts
        /// </summary>
        /// <param name="farmId">Farm ID</param>
        /// <returns>Farm-specific dashboard counts</returns>
        //[HttpGet("farm/{farmId}")]
        //public async Task<IActionResult> GetFarmCounts(int farmId)
        //{
        //    try
        //    {
        //        var counts = await _dashboardService.GetFarmCountsAsync(farmId);
        //        return Ok(new { success = true, data = counts });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { success = false, message = ex.Message });
        //    }
        //}
    }
}
