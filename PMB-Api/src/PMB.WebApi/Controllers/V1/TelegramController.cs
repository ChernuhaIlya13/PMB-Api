// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using PMB.Integration.Telegram;
// using PMB.Integration.Telegram.Services;
// using Telegram.Bot.Types;
//
// namespace PMB.WebApi.Controllers.V1
// {
//     [Route("api/v1/telegram")]
//     public class TelegramController: Controller
//     {
//         private readonly BotHandler _botHandler;
//
//         public TelegramController(BotHandler botHandler)
//         {
//             _botHandler = botHandler;
//         }
//
//         [HttpPost("update")]
//         public async Task<IActionResult> SendMessage([FromBody] Update message)
//         {
//             if (message != null)
//             {
//                 await _botHandler.Handle(message);
//             }
//             
//             return Ok();
//         }
//     }
// }