using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using PMB.Admin.Domain;
using PMB.Dal.Models;
using PMB.Dal.Repositories;
using PMB.Models.V1.Enums;

namespace PMB.Admin.Application.Queries.Handlers
{
    [UsedImplicitly]
    internal sealed class AllUsersQueryHandler: IRequestHandler<AllUsersQuery, Option<AllUsersQueryResult>>
    {
        private readonly UserRepository _userRepository;

        public AllUsersQueryHandler(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Option<AllUsersQueryResult>> Handle(AllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepository.UserWithKeysQuery(new UserWithKeysQueryModel
                {
                    Roles = new[] {Role.PrimaryUser.ToString()}
                }, cancellationToken);
                
                if (!string.IsNullOrEmpty(request.SearchQuery))
                {
                    result = result?.Where(x =>
                        x.Login?.StartsWith(request.SearchQuery, StringComparison.InvariantCultureIgnoreCase) == true).ToArray();
                }
                
                if (result?.Any() != true)
                {
                    return new AllUsersQueryResult(Array.Empty<UserQueryResult>()).ToOption();
                }
                
                var userKeyGroups = result
                    .GroupBy(x => x.Login)
                    .Select(x =>
                        new UserQueryResult(x.Key, x.Count(k => !string.IsNullOrEmpty(k.Key)),
                            x.Where(k => !string.IsNullOrEmpty(k.Key)).Select(k => new KeyResult(k.Login, k.Key, k.KeyExpirationTime, k.FreezeTime))
                                .ToArray()))
                    .ToArray();

                return new AllUsersQueryResult(userKeyGroups).ToOption();
            }
            catch (Exception)
            {
                return ErrorCode.InternalServerError.ToOption<AllUsersQueryResult>();
            }
        }
    }
}