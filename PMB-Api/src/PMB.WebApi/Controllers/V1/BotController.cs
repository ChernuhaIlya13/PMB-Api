using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Bll.Services;
using PMB.Models.V1.Requests;
using PMB.Models.V1.Responses;
using PMB.WebApi.Extensions;

namespace PMB.WebApi.Controllers.V1
{
    [ApiController]
    [Route("api/v1/bot")]
    public class BotController: Controller
    {
        private readonly KeyService _service;

        public BotController(KeyService service)
        {
            _service = service;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] BotLoginRequest request)
        {
            var identity = await _service.GetBotIdentity(new SelectKeyModel
            {
                Key = request.Key
            });
            
            if (identity == null)
            {
                return BadRequest("Invalid key.");
            }
            
            var response = new BotLoginResponse
            {
                Token = identity.GenerateToken(),
            };
 
            return Ok(response);
        }
    }
}