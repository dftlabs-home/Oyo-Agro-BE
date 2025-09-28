using Microsoft.AspNetCore.Mvc;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfileActivityParentController : ControllerBase
    {
        private readonly IProfileActivityParentService _profileActivityParentService;
        public ProfileActivityParentController(IProfileActivityParentService profileActivityParentService)
        {
            _profileActivityParentService = profileActivityParentService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProfileActivityParentParam model)
        {
            var response = await _profileActivityParentService.SaveEntity(model);

            if (response.Tag == 1)
            {
                return Ok(new { success = true, Data = response });

            }
            else
            {
                return Ok(new { success = false, Data = response });
            }
        }

         [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var response = await _profileActivityParentService.GetList();

            if (response.Tag == 1)
            {
                return Ok(new { success = true, Data = response });

            }
            else
            {
                return Ok(new { success = false, Data = response });
            }
        }

         [HttpPost("GetListByParam")]
        public async Task<IActionResult> GetList([FromForm] ProfileActivityParentParam param)
        {
            var response = await _profileActivityParentService.GetList(param);

            if (response.Tag == 1)
            {
                return Ok(new { success = true, Data = response });

            }
            else
            {
                return Ok(new { success = false, Data = response });
            }
        }


         [HttpGet("getEntity/{parentId}")]
        public async Task<IActionResult> GetEntity( [FromRoute] int parentId)
        {
            var response = await _profileActivityParentService.GetEntity(parentId);

            if (response.Tag == 1)
            {
                return Ok(new { success = true, Data = response });

            }
            else
            {
                return Ok(new { success = false, Data = response });
            }
        }



    }
}
