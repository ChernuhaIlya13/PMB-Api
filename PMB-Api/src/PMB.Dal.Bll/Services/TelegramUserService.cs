using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PMB.Dal.Bll.Dtos.TelegramUser;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;
using PMB.Dal.Repositories;

namespace PMB.Dal.Bll.Services
{
    public class TelegramUserService
    {
        private readonly TelegramUserRepository _telegramUserRepository;

        public TelegramUserService(TelegramUserRepository telegramUserRepository)
        {
            _telegramUserRepository = telegramUserRepository;
        }

        public async Task<TelegramUserDto> Create(TelegramUserDto dto, CancellationToken token)
        {
            var result = await _telegramUserRepository.Insert(new TelegramUserDal
            {
                BotKey = dto.BotKey,
                BotSettings = string.IsNullOrEmpty(dto.BotSettings) ? "''" : dto.BotSettings,
                IsActive = dto.IsActive,
                TelegramChatId = dto.TelegramChatId
            }, token);

            return new TelegramUserDto
            {
                BotKey = result.BotKey,
                BotSettings = result.BotSettings,
                IsActive = result.IsActive,
                TelegramChatId = result.TelegramChatId
            };
        }

        public async Task<TelegramUserDto[]> GetTelegramUsers(GetTelegramUsersModel model, CancellationToken token)
        {
            var result = await _telegramUserRepository.GetTelegramUsersQuery(new GetTelegramUsersQueryModel
            {
                BotKeys = model.BotKeys,
                IsActive = model.IsActive
            }, token);

            return result?.Select(x => new TelegramUserDto
            {
                BotKey = x.BotKey,
                BotSettings = x.BotSettings,
                IsActive = x.IsActive,
                TelegramChatId = x.TelegramChatId
            }).ToArray();
        }
    }
}