using MediatR;

namespace PMB.Admin.Domain
{
    public record AllUsersQuery(string SearchQuery) : IRequest<Option<AllUsersQueryResult>>;

    public record KeyQuery(string Login, string Key) : IRequest<Option<KeyResult>>;

    public record KeysQuery(string Login) : IRequest<Option<KeysQueryResult>>;
}