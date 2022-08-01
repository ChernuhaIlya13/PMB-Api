using PMB.Models.Messages;

namespace PMB.Dal.Bll.Hubs.Helpers
{
    public record UserCacheValue(
        string ConnectionId,
        bool IsActive, 
        ForksFilterMessage Filters = null, 
        long? LastForkShipped = null,
        bool AwaitingFork = false);

    public static class UserCacheExtensions
    {
        public static bool IsActiveUser(this UserCacheValue user) =>
            user != null && user.IsActive && user.ConnectionId != null;

        public static bool IsAwaitingFork(this UserCacheValue user) =>
            user.IsActiveUser() && user.AwaitingFork;
    }
}