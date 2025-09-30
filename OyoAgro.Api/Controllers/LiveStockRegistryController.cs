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


    public class LiveStockRegistryController : ControllerBase
    {
        private readonly ILiveStockRegistryService _liveStockRegistryService;
        public LiveStockRegistryController(ILiveStockRegistryService liveStockRegistryService)
        {
            _liveStockRegistryService= liveStockRegistryService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] LiveStockRegistryParam model)
        {
            var response = await _liveStockRegistryService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetLiveStockRegistries")]
        public async Task<IActionResult> GetList()
        {
            var response = await _liveStockRegistryService.GetList();


            return Ok(new { success = true, Data = response });

        }



        [HttpPost("getLiveStockRegistriesbyParam")]
        public async Task<IActionResult> GetList([FromForm] LiveStockRegistryParam param)
        {
            var response = await _liveStockRegistryService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteLiveStockRegistry/{registryId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int registryId)
        {
            var response = await _liveStockRegistryService.DeleteEntity(registryId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getLiveStockRegistry/{registryId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int registryId)
        {
            var response = await _liveStockRegistryService.GetEntity(registryId);

            return Ok(new { success = true, Data = response });


        }

    }
}
