using System;
using MediatR;

namespace PMB.Admin.Domain
{
    public record CreateKeyCommand(string Login, DateTimeOffset KeyExpirationTime) : IRequest<Option<KeyResult>>;

    public record FreezeKeyCommand(string Login, string Key) : IRequest<Option<KeyResult>>;

    public record UnfreezeKeyCommand(string Login, string Key) : IRequest<Option<KeyResult>>;

    public record ChangeKeyLifetimeCommand(string Login, string Key, DateTimeOffset NewKeyExpiration) : IRequest<Option<KeyResult>>;

    public record RemoveKeyCommand(string Login, string Key) : IRequest<Option<RemoveKeyResult>>;
}