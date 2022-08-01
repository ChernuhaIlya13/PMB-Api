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
    internal sealed class KeyQueryHandler: IRequestHandler<KeyQuery, Option<KeyResult>>
    {
        private readonly KeyRepository _keyRepository;
        
        public KeyQueryHandler(KeyRepository keyRepository)
        {
            _keyRepository = keyRepository;
        }

        public async Task<Option<KeyResult>> Handle(KeyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var keys = await _keyRepository.Select(new SelectKeyQueryModel
                {
                    Key = request.Key,
                    Login = request.Login
                });

                if (keys?.Any() == true)
                {
                    var keyDal = keys.First();
                    return new KeyResult(keyDal.Login, keyDal.Key, keyDal.KeyExpirationTime, keyDal.FreezeTime).ToOption();
                }

                return ErrorCode.KeyNotFound.ToOption<KeyResult>();
            }
            catch (Exception)
            {
                return ErrorCode.InternalServerError.ToOption<KeyResult>();
            }
        }
    }
}