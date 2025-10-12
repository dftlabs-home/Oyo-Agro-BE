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
    public class SeasonController : ControllerBase
    {

        private readonly ISeasonServices _seasonServices;
        public SeasonController(ISeasonServices seasonServices)
        {
            _seasonServices = seasonServices;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] SeasonParam model)
        {
            var response = await _seasonServices.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] SeasonParam model)
        {
            var response = await _seasonServices.UpdateEntity(model);


            return Ok(new { success = true, Data = response });

        }



        [HttpGet("GetSeasons")]
        public async Task<IActionResult> GetList()
        {
            var response = await _seasonServices.GetList();


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("GetSeasonsByParam")]
        public async Task<IActionResult> GetList([FromForm] SeasonParam param)
        {
            var response = await _seasonServices.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteSeason/{seasonId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int seasonId)
        {
            var response = await _seasonServices.DeleteEntity(seasonId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getSeason/{seasonId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int seasonId)
        {
            var response = await _seasonServices.GetEntity(seasonId);

            return Ok(new { success = true, Data = response });


        }


    }
}
