using Microsoft.AspNetCore.Mvc;
using OyoAgro.Api.Authorizations;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]

    public class CropController : ControllerBase
    {

        private readonly ICropService _cropService;
        public CropController(ICropService cropService)
        {
            _cropService = cropService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CropParam model)
        {
            var response = await _cropService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetCrops")]
        public async Task<IActionResult> GetList()
        {
            var response = await _cropService.GetList();


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("GetCropsByParam")]
        public async Task<IActionResult> GetList([FromForm] CropParam param)
        {
            var response = await _cropService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteCrop/{cropId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int cropId)
        {
            var response = await _cropService.DeleteEntity(cropId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getCrop/{cropId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int cropId)
        {
            var response = await _cropService.GetEntity(cropId);

            return Ok(new { success = true, Data = response });


        }

    }
}
