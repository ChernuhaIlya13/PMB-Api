using System;
using PMB.Abb.Models;

namespace PMB.Abb.Client
{
    public class AbbProviderOptions
    {
        public string Url { get; set; }

        public TimeSpan[] RetryTimeouts { get; set; } = Consts.Default.RetryTimeouts;
        
        public TimeSpan CloseTimeout { get; set; } = Consts.Default.CloseTimeout;

        public string CleanupCron { get; set; } = Consts.Default.CleanupCron;
        
        public TimeSpan CleanupLifetime { get; set; } = Consts.Default.CleanupLifetime;
    }
}