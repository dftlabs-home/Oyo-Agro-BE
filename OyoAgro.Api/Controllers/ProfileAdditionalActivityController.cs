using Microsoft.AspNetCore.Mvc;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfileAdditionalActivityController : ControllerBase
    {
        private readonly IProfileAdditionalActivityService _profileAdditionalActivityService;
        public ProfileAdditionalActivityController(IProfileAdditionalActivityService profileAdditionalActivityService)
        {
            _profileAdditionalActivityService = profileAdditionalActivityService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProfileadditionalactivityParam model)
        {
            var response = await _profileAdditionalActivityService.SaveEntity(model);

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
            var response = await _profileAdditionalActivityService.GetList();

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
        public async Task<IActionResult> GetList([FromForm] ProfileadditionalactivityParam param)
        {
            var response = await _profileAdditionalActivityService.GetList(param);

            if (response.Tag == 1)
            {
                return Ok(new { success = true, Data = response });

            }
            else
            {
                return Ok(new { success = false, Data = response });
            }
        }


        [HttpGet("deleteEntity/{profileAdditionalActivityId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int profileAdditionalActivityId)
        {
            var response = await _profileAdditionalActivityService.DeleteEntity(profileAdditionalActivityId);

            if (response.Tag == 1)
            {
                return Ok(new { success = true, Data = response });

            }
            else
            {
                return Ok(new { success = false, Data = response });
            }
        }

         [HttpGet("getEntity/{additionalActivityId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int profileAdditionalActivityId)
        {
            var response = await _profileAdditionalActivityService.GetEntity(profileAdditionalActivityId);

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
