using System;

namespace PMB.Admin.Domain
{
    public record KeyResult(string Login, string Key, DateTimeOffset KeyExpirationTime, DateTimeOffset? FreezeTime);
    
    public record UserQueryResult(string Login, long KeysCount, KeyResult[] Keys);

    public record AllUsersQueryResult(UserQueryResult[] Users);
    
    public record KeysQueryResult(KeyResult[] Keys);

    public record RemoveKeyResult(bool Deleted);
    
    public static class KeyResultExtensions
    {
        public static KeyResult Copy(this KeyResult result, Option<DateTimeOffset> keyExpirationTime,
            Option<DateTimeOffset?> freezeTime)
        {
            return result with
            {
                KeyExpirationTime = keyExpirationTime?.Body ?? result.KeyExpirationTime,
                FreezeTime = freezeTime != null ? freezeTime.Body : result.FreezeTime
            };
        }
    }
}