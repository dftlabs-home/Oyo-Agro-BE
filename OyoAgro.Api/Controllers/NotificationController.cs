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
    public class NotificationController : ControllerBase
    {

        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] NotificationParam model)
        {
            var response = await _notificationService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetNotifications")]
        public async Task<IActionResult> GetList()
        {
            var response = await _notificationService.GetList();


            return Ok(new { success = true, Data = response });

        }

        [HttpPost("GetNotificationsByParam")]
        public async Task<IActionResult> GetList([FromForm] NotificationParam param)
        {
            var response = await _notificationService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteNotification/{notificationId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int notificationId)
        {
            var response = await _notificationService.DeleteEntity(notificationId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getNotification/{notificationId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int notificationId)
        {
            var response = await _notificationService.GetEntity(notificationId);

            return Ok(new { success = true, Data = response });


        }

    }
}
