using Microsoft.AspNetCore.Mvc;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FarmTypeController : ControllerBase
    {
        private readonly IFarmTypeService _farmTypeService;
        public FarmTypeController(IFarmTypeService farmTypeService)
        {
            _farmTypeService = farmTypeService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] FarmTypeParam model)
        {
            var response = await _farmTypeService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetFarmTypes")]
        public async Task<IActionResult> GetList()
        {
            var response = await _farmTypeService.GetList();


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getFarmType/{farmTypeId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int farmTypeId)
        {
            var response = await _farmTypeService.GetEntity(farmTypeId);

            return Ok(new { success = true, Data = response });


        }

        [HttpPost("GetFarmTypesByParam")]
        public async Task<IActionResult> GetList([FromForm] FarmTypeParam param)
        {
            var response = await _farmTypeService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteFarmType/{farmTypeId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int farmTypeId)
        {
            var response = await _farmTypeService.DeleteEntity(farmTypeId);


            return Ok(new { success = true, Data = response });

        }


    }
}
