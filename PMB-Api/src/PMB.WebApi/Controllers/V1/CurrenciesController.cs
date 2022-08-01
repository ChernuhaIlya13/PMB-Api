using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMB.Dal.Bll.Services;
using PMB.Models.V1.Requests;
using PMB.Models.V1.Responses;

namespace PMB.WebApi.Controllers.V1
{
    [ApiController]
    [Route("api/v1/currencies")]
    public class CurrenciesController: Controller
    {
        private readonly CurrencyRatesService _service;
        
        public CurrenciesController(CurrencyRatesService service)
        {
            _service = service;
        }

        [HttpPost("rates")]
        public async Task<ActionResult<CurrencyRatesResponse>> Get([FromBody]CurrencyRatesRequest request, CancellationToken token)
        {
            var result = await _service.GetRates(request.Date, token);

            return Ok(result);
        }
    }
}