using System;
using System.Linq;

namespace PMB.Abb.Models
{
    public static class Consts
    {
        public const string AbbForks = nameof(AbbForks);
        
        public const string AbbHubEndpoint = "/abb-hub";

        public const string ArbHash = "arb_hash";

        
        public static class Default
        {
            public const string CleanupCron = "*/30 * * * * *";

            public static readonly TimeSpan CleanupLifetime = TimeSpan.FromSeconds(60);
            
            public static readonly TimeSpan CloseTimeout = TimeSpan.FromSeconds(1);

            public static readonly TimeSpan[] RetryTimeouts =
                new[] {2, 4, 6}.Select(x => TimeSpan.FromSeconds(x)).ToArray();
        }
        
        public static class ExceptionMessages
        {
            public const string TokenMustBeProvided = "Token must be provided";

            public const string AbbOptionsMustBeProvided = "AbbProviderOptions must be provided";

            public const string HubUrlMustBeProvided = "Hub url must be provided";
        }
    }
}