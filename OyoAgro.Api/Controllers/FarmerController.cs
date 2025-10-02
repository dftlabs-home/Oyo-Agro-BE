using Microsoft.AspNetCore.Mvc;
using OyoAgro.Api.Authorizations;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize]
    public class FarmerController : ControllerBase
    {
        private readonly IFarmerSevice _farmerSevice;
        public FarmerController(IFarmerSevice farmerSevice)
        {
            _farmerSevice = farmerSevice;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] FarmerParam model)
        {
            var token = string.Empty;
            var header = (string)HttpContext.Request.Headers["Authorization"]!;
            if (header != null) token = header.Substring(7);
            var user = await new DataRepository().GetUserByToken(token);
            model.UserId = user.UserId;
            var response = await _farmerSevice.SaveEntity(model);

            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetFarmers")]
        public async Task<IActionResult> GetList()
        {
            var response = await _farmerSevice.GetList();


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("GetFarmersByParam")]
        public async Task<IActionResult> GetList([FromForm] FarmerParam param)
        {
            var response = await _farmerSevice.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteFarmer/{farmerId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int regionId)
        {
            var response = await _farmerSevice.DeleteEntity(regionId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getFarmer/{farmerId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int farmerId)
        {
            var response = await _farmerSevice.GetEntity(farmerId);

            return Ok(new { success = true, Data = response });


        }

    }
}
