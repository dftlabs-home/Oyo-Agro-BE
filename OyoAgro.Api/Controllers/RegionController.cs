using Microsoft.AspNetCore.Mvc;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;
        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RegionParam model)
        {
            var response = await _regionService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetRegions")]
        public async Task<IActionResult> GetList()
        {
            var response = await _regionService.GetList();


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetRegionsByParam")]
        public async Task<IActionResult> GetList([FromForm] RegionParam param)
        {
            var response = await _regionService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteRegion/{regionId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int regionId)
        {
            var response = await _regionService.DeleteEntity(regionId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getRegion/{regionId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int regionId)
        {
            var response = await _regionService.GetEntity(regionId);

            return Ok(new { success = true, Data = response });


        }


    }
}
