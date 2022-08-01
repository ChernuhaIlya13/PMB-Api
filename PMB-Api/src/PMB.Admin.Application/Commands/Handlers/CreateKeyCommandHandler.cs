using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using PMB.Admin.Domain;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;
using PMB.Dal.Repositories;

namespace PMB.Admin.Application.Commands.Handlers
{
    [UsedImplicitly]
    internal sealed class CreateKeyCommandHandler: IRequestHandler<CreateKeyCommand, Option<KeyResult>>
    {
        private readonly KeyRepository _keyRepository;
        private readonly UserRepository _userRepository;
        
        public CreateKeyCommandHandler(KeyRepository keyRepository, UserRepository userRepository)
        {
            _keyRepository = keyRepository;
            _userRepository = userRepository;
        }

        [ItemCanBeNull]
        public async Task<Option<KeyResult>> Handle(CreateKeyCommand request, CancellationToken token)
        {
            try
            {
                var user = await _userRepository.GetByQuery(new SelectUserQueryModel
                {
                    Login = request.Login
                }, token);

                if (user != null)
                {
                    var result = await _keyRepository.Create(new KeyDal
                    {
                        Key = Guid.NewGuid().ToString("N"),
                        Login = request.Login,
                        KeyExpirationTime = request.KeyExpirationTime
                    });

                    return new KeyResult(result.Login, result.Key, result.KeyExpirationTime, result.FreezeTime).ToOption();
                }

                return ErrorCode.UserNotFound.ToOption<KeyResult>();
            }
            catch (Exception)
            {
                return ErrorCode.InternalServerError.ToOption<KeyResult>();
            }
        }
    }
}