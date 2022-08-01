using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using PMB.Admin.Domain;
using PMB.Dal.Models;
using PMB.Dal.Repositories;

namespace PMB.Admin.Application.Queries.Handlers
{
    [UsedImplicitly]
    internal sealed class KeysQueryHandler: IRequestHandler<KeysQuery, Option<KeysQueryResult>>
    {
        private readonly KeyRepository _keyRepository;

        public KeysQueryHandler(KeyRepository keyRepository)
        {
            _keyRepository = keyRepository;
        }

        public async Task<Option<KeysQueryResult>> Handle(KeysQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var keys = await _keyRepository.Select(new SelectKeyQueryModel
                {
                    Login = request.Login
                });
            
                return new KeysQueryResult(
                    keys?.Select(x => new KeyResult(x.Login, x.Key, x.KeyExpirationTime, x.FreezeTime)).ToArray())
                    .ToOption();
            }
            catch (Exception)
            {
                return ErrorCode.InternalServerError.ToOption<KeysQueryResult>();
            }
        }
    }
}