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

    public class CropRegistryController : ControllerBase
    {
        private readonly ICropRegistryService _cropRegistryService;
        public CropRegistryController(ICropRegistryService cropRegistryService)
        {
            _cropRegistryService = cropRegistryService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CropRegistryParam model)
        {
            var response = await _cropRegistryService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetCropRegistries")]
        public async Task<IActionResult> GetList()
        {
            var response = await _cropRegistryService.GetList();


            return Ok(new { success = true, Data = response });

        }

        

        [HttpPost("getCropRegistriesbyParam")]
        public async Task<IActionResult> GetList([FromForm] CropRegistryParam param)
        {
            var response = await _cropRegistryService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteCropRegistry/{registryId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int registryId)
        {
            var response = await _cropRegistryService.DeleteEntity(registryId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getCropRegistry/{registryId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int registryId)
        {
            var response = await _cropRegistryService.GetEntity(registryId);

            return Ok(new { success = true, Data = response });


        }

    }
}
