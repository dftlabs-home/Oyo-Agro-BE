using Microsoft.AspNetCore.Mvc;
using OyoAgro.Api.Authorizations;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]

    public class BusinessTypeController : ControllerBase
    {
        private readonly IBusinessTypeService _businessTypeService;
        public BusinessTypeController(IBusinessTypeService businessTypeService)
        {
            _businessTypeService = businessTypeService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BusinessTypeParam model)
        {
            var response = await _businessTypeService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] BusinessTypeParam model)
        {
            var response = await _businessTypeService.UpdateEntity(model);


            return Ok(new { success = true, Data = response });

        }



        [HttpGet("GetBusinessTypes")]
        public async Task<IActionResult> GetList()
        {
            var response = await _businessTypeService.GetList();


            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deletebusinessType/{Id}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int Id)
        {
            var response = await _businessTypeService.DeleteEntity(Id);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getBusinessType/{Id}")]
        public async Task<IActionResult> GetEntity([FromRoute] int Id)
        {
            var response = await _businessTypeService.GetEntity(Id);

            return Ok(new { success = true, Data = response });


        }

    }
}
