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
    internal sealed class RemoveKeyCommandHandler: IRequestHandler<RemoveKeyCommand, Option<RemoveKeyResult>>
    {
        private readonly KeyRepository _keyRepository;
        private readonly IMediator _mediator;

        public RemoveKeyCommandHandler(KeyRepository keyRepository, IMediator mediator)
        {
            _keyRepository = keyRepository;
            _mediator = mediator;
        }

        public async Task<Option<RemoveKeyResult>> Handle(RemoveKeyCommand request, CancellationToken cancellationToken)
        {
            var key = await _mediator.Send(new KeyQuery(request.Login, request.Key), cancellationToken);

            if (key.IsOk())
            {
                try
                {
                    await _keyRepository.DeleteKey(new DeleteKeyModel
                    {
                        Key = key.Body.Key,
                        Login = key.Body.Login
                    }, cancellationToken);

                    return new RemoveKeyResult(true).ToOption();
                }
                catch (Exception)
                {
                    return ErrorCode.InternalServerError.ToOption<RemoveKeyResult>();
                }
            }

            return ErrorCode.KeyNotFound.ToOption<RemoveKeyResult>();
        }
    }
}