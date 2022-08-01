using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMB.Admin.Domain;
using PMB.Models.V1.Enums;
using PMB.WebApi.Controllers.Base;

namespace PMB.WebApi.Controllers.V1
{
    [ApiController]
    [Route("api/v1/admin")]
    [Authorize(Roles = nameof(Role.Admin))]
    public class AdminController: MediatrController
    {
        public AdminController(IMediator mediator) : base(mediator) { }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok();

        [HttpGet("users")]
        public Task<ActionResult<Option<AllUsersQueryResult>>> GetAllUsers([FromQuery] string searchQuery, CancellationToken token) => 
            Do(new AllUsersQuery(searchQuery), token);

        [HttpGet("keys")]
        public Task<ActionResult<Option<KeysQueryResult>>> GetKeys([FromQuery] string login, CancellationToken token) =>
            Do(new KeysQuery(login), token);

        [HttpGet("key")]
        public Task<ActionResult<Option<KeyResult>>> GetKeys([FromQuery] KeyQuery keyQuery, CancellationToken token) =>
            Do(keyQuery, token);

        [HttpPost("create-key")]
        public Task<ActionResult<Option<KeyResult>>> CreateKey([FromBody] CreateKeyCommand request, CancellationToken token) =>
            Do(request, token);
        
        [HttpPut("freeze-key")]
        public Task<ActionResult<Option<KeyResult>>> FreezeKey([FromBody] FreezeKeyCommand request, CancellationToken token) =>
            Do(request, token);

        [HttpPut("unfreeze-key")]
        public Task<ActionResult<Option<KeyResult>>> UnfreezeKey([FromBody] UnfreezeKeyCommand request, CancellationToken token) =>
            Do(request, token);

        [HttpPut("change-key-lifetime")]
        public Task<ActionResult<Option<KeyResult>>> ActionKey([FromBody] ChangeKeyLifetimeCommand request, CancellationToken token) =>
            Do(request, token);

        [HttpDelete("remove-key")]
        public Task<ActionResult<Option<RemoveKeyResult>>> RemoveKey([FromQuery] RemoveKeyCommand request, CancellationToken token) =>
            Do(request, token);
    }
}