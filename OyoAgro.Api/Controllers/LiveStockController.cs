using Microsoft.AspNetCore.Mvc;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    public class LiveStockController : ControllerBase
    {
        private readonly ILiveStockService _liveStockService;
        public LiveStockController(ILiveStockService liveStockRegistryService)
        {
            _liveStockService = liveStockRegistryService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] LivestockParam model)
        {
            var response = await _liveStockService.SaveEntity(model);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("GetLiveStocks")]
        public async Task<IActionResult> GetList()
        {
            var response = await _liveStockService.GetList();


            return Ok(new { success = true, Data = response });

        }



        [HttpPost("getLiveStockbyParam")]
        public async Task<IActionResult> GetList([FromForm] LivestockParam param)
        {
            var response = await _liveStockService.GetList(param);

            return Ok(new { success = true, Data = response });

        }


        [HttpGet("deleteLiveStock/{livestockId}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int livestockId)
        {
            var response = await _liveStockService.DeleteEntity(livestockId);


            return Ok(new { success = true, Data = response });

        }

        [HttpGet("getLiveStock/{livestockId}")]
        public async Task<IActionResult> GetEntity([FromRoute] int livestockId)
        {
            var response = await _liveStockService.GetEntity(livestockId);

            return Ok(new { success = true, Data = response });


        }
    }
}
