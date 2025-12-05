using Microsoft.AspNetCore.Mvc;
using OyoAgro.Api.Authorizations;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize]

    public class PrimaryProductController : ControllerBase
    {
        private readonly IPrimaryProductService _primaryProductService;
        public PrimaryProductController(IPrimaryProductService primaryProductService)
        {
            _primaryProductService = primaryProductService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PrimaryProductParam model)
        {
            var response = await _primaryProductService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] PrimaryProductParam model)
        {
            var response = await _primaryProductService.UpdateEntity(model);


            return Ok(new { success = true, Data = response });

        }



        [HttpGet("GetPrimaryProducts")]
        public async Task<IActionResult> GetList()
        {
            var response = await _primaryProductService.GetList();


            return Ok(new { success = true, Data = response });

        }

       
        [HttpGet("deletePrimaryProduct/{Id}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int Id)
        {
            var response = await _primaryProductService.DeleteEntity(Id);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getPrimaryProduct/{Id}")]
        public async Task<IActionResult> GetEntity([FromRoute] int Id)
        {
            var response = await _primaryProductService.GetEntity(Id);

            return Ok(new { success = true, Data = response });


        }

    }
}
