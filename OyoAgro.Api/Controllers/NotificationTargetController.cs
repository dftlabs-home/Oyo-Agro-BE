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
    public class NotificationTargetController : ControllerBase
    {
        private readonly INotificationTargetService _notificationTargetService;
        public NotificationTargetController(INotificationTargetService notificationTargetService)
        {
            _notificationTargetService = notificationTargetService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] NotificationTargetParam model)
        {
            var response = await _notificationTargetService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getNotificationTargets")]
        public async Task<IActionResult> GetList()
        {
            var response = await _notificationTargetService.GetList();


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("GetTargetByParams")]
        public async Task<IActionResult> GetList([FromForm] NotificationTargetParam param)
        {
            var response = await _notificationTargetService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteTarget/{targetid}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int targetId)
        {
            var response = await _notificationTargetService.DeleteEntity(targetId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getTarget/{targetId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int targetId)
        {
            var response = await _notificationTargetService.GetEntity(targetId);

            return Ok(new { success = true, Data = response });


        }

    }
}
