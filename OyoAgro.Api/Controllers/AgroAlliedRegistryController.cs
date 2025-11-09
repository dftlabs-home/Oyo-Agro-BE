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
    public class AgroAlliedRegistryController : ControllerBase
    {
            private readonly IAgroAlliedRegistryService _agroAlliedRegistryService;
            public AgroAlliedRegistryController(IAgroAlliedRegistryService agroAlliedRegistryService)
            {
                _agroAlliedRegistryService = agroAlliedRegistryService;
            }

            [HttpPost("Create")]
            public async Task<IActionResult> Create([FromBody] AgroAlliedRegistryParam model)
            {
                var response = await _agroAlliedRegistryService.SaveEntity(model);


                return Ok(new { success = true, Data = response });

            }

            [HttpGet("GetAgroAlliedRegistries")]
            public async Task<IActionResult> GetList()
            {
                var response = await _agroAlliedRegistryService.GetList();


                return Ok(new { success = true, Data = response });

            }



            [HttpPost("getAgroAlliedRegistrybyParam")]
            public async Task<IActionResult> GetList([FromForm] AgroAlliedRegistryParam param)
            {
                var response = await _agroAlliedRegistryService.GetList(param);

                return Ok(new { success = true, Data = response });

            }
      


            [HttpGet("deleteAgroAlliedRegistry/{registryId}")]
            public async Task<IActionResult> DeleteEntity([FromRoute] int registryId)
            {
                var response = await _agroAlliedRegistryService.DeleteEntity(registryId);


                return Ok(new { success = true, Data = response });

            }
       

            [HttpGet("getAgroAlliedRegistry/{registryId}")]
            public async Task<IActionResult> GetEntity([FromRoute] int registryId)
            {
                var response = await _agroAlliedRegistryService.GetEntity(registryId);

                return Ok(new { success = true, Data = response });


            }
         [HttpGet("getAgroAlliedRegistry/{farmId}")]
            public async Task<IActionResult> GetListbyFarmId([FromRoute] int farmId)
            {
                var response = await _agroAlliedRegistryService.GetListByFarm(farmId);

                return Ok(new { success = true, Data = response });


            }

        }
    }
