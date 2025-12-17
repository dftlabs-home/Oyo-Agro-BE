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

    public class LgaController : ControllerBase
    {
        private readonly ILgaServices _lgaServices;
        public LgaController(ILgaServices lgaServices)
        {
            _lgaServices = lgaServices;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] LgaParam model)
        {
            var response = await _lgaServices.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetLgas")]
        public async Task<IActionResult> GetList()
        {
            var response = await _lgaServices.GetList();


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("GetLgasByParam")]
        public async Task<IActionResult> GetList([FromForm] LgaParam param)
        {
            var response = await _lgaServices.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteLga/{lgaId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int lgaId)
        {
            var response = await _lgaServices.DeleteEntity(lgaId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getLga/{lgaId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int lgaId)
        {
            var response = await _lgaServices.GetEntity(lgaId);

            return Ok(new { success = true, Data = response });


        }
        [HttpGet("getLgasByRegionId/{regionId}")]
        public async Task<IActionResult> GetListByRegionId([FromRoute] int regionId)
        {
            var response = await _lgaServices.GetEntityByRegionId(regionId);

            return Ok(new { success = true, Data = response });


        }

    }
}
