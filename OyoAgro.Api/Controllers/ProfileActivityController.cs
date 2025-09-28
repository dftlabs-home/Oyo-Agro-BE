using Microsoft.AspNetCore.Mvc;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfileActivityController : ControllerBase
    {
        private readonly IProfileActivityService _profileActivityService;
        public ProfileActivityController(IProfileActivityService profileActivityService)
        {
            _profileActivityService = profileActivityService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProfileActivityParam model)
        {
            var response = await _profileActivityService.SaveEntity(model);

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
            var response = await _profileActivityService.GetList();

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
        public async Task<IActionResult> GetList([FromForm] ProfileActivityParam param)
        {
            var response = await _profileActivityService.GetList(param);

            if (response.Tag == 1)
            {
                return Ok(new { success = true, Data = response });

            }
            else
            {
                return Ok(new { success = false, Data = response });
            }
        }


        [HttpGet("getEntity/{activityId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int activityId)
        {
            var response = await _profileActivityService.GetEntity(activityId);

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
