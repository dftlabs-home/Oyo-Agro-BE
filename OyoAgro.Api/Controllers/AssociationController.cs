using Microsoft.AspNetCore.Mvc;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class AssociationController : ControllerBase
    {

        private readonly IAssociationService _associationService;
        public AssociationController(IAssociationService assService)
        {
            _associationService = assService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AssociationParam model)
        {
            var response = await _associationService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetAssociations")]
        public async Task<IActionResult> GetList()
        {
            var response = await _associationService.GetList();


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("GetAssociaitonsByParam")]
        public async Task<IActionResult> GetList([FromForm] AssociationParam param)
        {
            var response = await _associationService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteAssociation/{associationId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int assId)
        {
            var response = await _associationService.DeleteEntity(assId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getAssociation/{associationId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int assId)
        {
            var response = await _associationService.GetEntity(assId);

            return Ok(new { success = true, Data = response });


        }

    }
}
