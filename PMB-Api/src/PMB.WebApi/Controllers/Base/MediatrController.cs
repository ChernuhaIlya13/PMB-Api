using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMB.Admin.Domain;

namespace PMB.WebApi.Controllers.Base
{
    [ApiController]
    public class MediatrController: Controller
    {
        protected readonly IMediator _mediator;

        protected MediatrController(IMediator mediator) => _mediator = mediator;

        protected async Task<ActionResult<Option<TResponse>>> Do<TResponse>(IRequest<Option<TResponse>> request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);

            return result.IsOk() ? Ok(result) : BadRequest(result);
        }
    }
}