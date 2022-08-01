using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using PMB.Admin.Domain;
using PMB.Dal.Models;
using PMB.Dal.Repositories;

namespace PMB.Admin.Application.Commands.Handlers
{
    [UsedImplicitly]
    internal sealed class ChangeKeyLifetimeCommandHandler: IRequestHandler<ChangeKeyLifetimeCommand, Option<KeyResult>>
    {
        private readonly KeyRepository _keyRepository;
        private readonly IMediator _mediator;
        
        public ChangeKeyLifetimeCommandHandler(KeyRepository keyRepository, IMediator mediator)
        {
            _keyRepository = keyRepository;
            _mediator = mediator;
        }

        public async Task<Option<KeyResult>> Handle(ChangeKeyLifetimeCommand request, CancellationToken cancellationToken)
        {
            var key = await _mediator.Send(new KeyQuery(request.Login, request.Key), cancellationToken);

            if (key.IsOk())
            {
                try
                {
                    await _keyRepository.UpdateKeyExpirationTime(new UpdateKeyExpirationTimeModel
                    {
                        Key = request.Key,
                        Login = request.Login,
                        KeyExpirationTime = request.NewKeyExpiration
                    });

                    return key.Body.Copy(request.NewKeyExpiration.ToOption(), null).ToOption();
                }
                catch (Exception)
                {
                    return ErrorCode.InternalServerError.ToOption<KeyResult>();
                }
            }

            return ErrorCode.KeyNotFound.ToOption<KeyResult>();
        }
    }
}