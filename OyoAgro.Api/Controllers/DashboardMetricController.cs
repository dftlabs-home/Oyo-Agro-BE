using Microsoft.AspNetCore.Mvc;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DashboardMetricController : ControllerBase
    {
        private readonly IDashboardMetricsService _metricsService;
        public DashboardMetricController(IDashboardMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        [HttpGet("GetDashboardMetrics")]
        public async Task<IActionResult> GetList()
        {
            var response = await _metricsService.GetList();


            return Ok(new { success = true, Data = response });

        }


         [HttpGet("GetDashboardMetricsByParam")]
        public async Task<IActionResult> GetList([FromForm] DashboardMetricsParam param)
        {
            var response = await _metricsService.GetMetricsAsync(param);


            return Ok(new { success = true, Data = response });

        }

    }
}
