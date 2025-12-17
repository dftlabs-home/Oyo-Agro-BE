using Microsoft.AspNetCore.Mvc;
using OyoAgro.Api.Authorizations;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]

    public class DashboardReportingController : ControllerBase
    {
        private readonly IDashboardReportingService _dashboardReportingService;
        public DashboardReportingController(IDashboardReportingService dashboardReportingService)
        {
            _dashboardReportingService = dashboardReportingService;
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetList()
        {
            var response = await _dashboardReportingService.GetDashboard();


            return Ok(new { success = true, Data = response });

        }



    }
}
