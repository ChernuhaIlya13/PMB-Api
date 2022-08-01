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
    internal sealed class FreezeKeyCommandHandler: IRequestHandler<FreezeKeyCommand, Option<KeyResult>>
    {
        private readonly KeyRepository _keyRepository;
        private readonly IMediator _mediator;
        
        public FreezeKeyCommandHandler(KeyRepository keyRepository, IMediator mediator)
        {
            _keyRepository = keyRepository;
            _mediator = mediator;
        }

        public async Task<Option<KeyResult>> Handle(FreezeKeyCommand request, CancellationToken cancellationToken)
        {
            var key = await _mediator.Send(new KeyQuery(request.Login, request.Key), cancellationToken);

            if (key?.IsOk() == true)
            {
                try
                {
                    var now = DateTimeOffset.Now as DateTimeOffset?;
                    if (key.Body.FreezeTime > DateTimeOffset.MinValue)
                    {
                        return ErrorCode.AlreadyFrozen.ToOption<KeyResult>();
                    }

                    await _keyRepository.FreezeKey(new FreezeKeyModel
                    {
                        Key = request.Key,
                        Login = request.Login,
                        FreezeTime = now.Value
                    });

                    return key.Body.Copy(null, now.ToOption()).ToOption();
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