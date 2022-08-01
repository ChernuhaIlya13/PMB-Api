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
    internal sealed class UnfreezeKeyCommandHandler: IRequestHandler<UnfreezeKeyCommand, Option<KeyResult>>
    {
        private readonly KeyRepository _keyRepository;
        private readonly IMediator _mediator;
        
        public UnfreezeKeyCommandHandler(KeyRepository keyRepository, IMediator mediator)
        {
            _keyRepository = keyRepository;
            _mediator = mediator;
        }

        public async Task<Option<KeyResult>> Handle(UnfreezeKeyCommand request, CancellationToken cancellationToken)
        {
            var key = await _mediator.Send(new KeyQuery(request.Login, request.Key), cancellationToken);

            if (key?.IsOk() == true)
            {
                try
                {
                    if (key.Body?.FreezeTime > DateTimeOffset.MinValue)
                    {
                        var newExpTime = key.Body.KeyExpirationTime.Add(DateTimeOffset.Now.Subtract(key.Body.FreezeTime.Value));
                        
                        await _keyRepository.UnFreezeKey(new UnFreezeKeyModel
                        {
                            Key = request.Key,
                            Login = request.Login,
                            KeyExpirationTime = newExpTime
                                
                        });

                        return key.Body.Copy(newExpTime.ToOption(), null).ToOption();
                    }

                    return ErrorCode.NoFrozenKey.ToOption<KeyResult>();
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