using Microsoft.AspNetCore.Mvc;
using OyoAgro.Api.Authorizations;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]

    public class FarmController : ControllerBase
    {
        private readonly IFarmService _farmService;
        public FarmController(IFarmService farmSevice)
        {
            _farmService = farmSevice;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] FarmParam model)
        {
            var response = await _farmService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }
           [HttpPost("Update")]
        public async Task<IActionResult> update([FromBody] FarmParam model)
        {
            var response = await _farmService.UpdateEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetFarms")]
        public async Task<IActionResult> GetList()
        {
            var response = await _farmService.GetList();


            return Ok(new { success = true, Data = response });

        }

          [HttpGet("GetFarmsByFarmerId/{farmerId}")]
        public async Task<IActionResult> GetList(int farmerId)
        {
            var response = await _farmService.GetListByFarmerId(farmerId);


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("GetFarmsByParam")]
        public async Task<IActionResult> GetList([FromForm] FarmParam param)
        {
            var response = await _farmService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteFarm/{farmId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int farmId)
        {
            var response = await _farmService.DeleteEntity(farmId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getFarm/{farmId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int farmId)
        {
            var response = await _farmService.GetEntity(farmId);

            return Ok(new { success = true, Data = response });


        }


    }
}
