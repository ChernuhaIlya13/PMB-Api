using System.Linq;
using System.Threading;
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
    [Route("api/v1/forks")]
    public class TestForksController: Controller
    {
        private readonly ForksService _forksService;

        public TestForksController(ForksService forksService)
        {
            _forksService = forksService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetForksByQueryResponse), 200)]
        public async Task<IActionResult> GetByQuery([FromBody] GetForksByQueryRequest request, CancellationToken token)
        {
            var result = await _forksService.GetForksByQuery(new GetForksByQueryModel
            {
                Bookmakers = request.Bookmakers,
                Sports = request.Sports,
                BetTypes = request.BetTypes,
                CridIds = request.CridIds
            });

            return Ok(new GetForksByQueryResponse
            {
                TotalCount = result?.Length ?? 0,
                Forks = result?.Select(x => x.Convert()).ToArray()
            });
        }
    }
}