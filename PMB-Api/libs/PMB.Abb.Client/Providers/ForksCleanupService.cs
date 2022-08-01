using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PMB.Utilities;

namespace PMB.Abb.Client.Providers
{
    public class ForksCleanupService: BackgroundScheduler
    {
        private readonly AbbProviderOptions _options;
        
        public ForksCleanupService(IOptions<AbbProviderOptions> options, IServiceProvider provider) 
            : base(options.Value.CleanupCron, provider)
        {
            _options = options.Value;
        }

        protected override Task Process(IServiceScope scope)
        {
            var provider = scope.ServiceProvider.GetRequiredService<IAbbForksProvider>();

            var hashes = new List<string>();
            
            foreach (var (key, fork) in provider.BlockingForks)
            {
                if (DateTimeOffset.Now.ToLocalTime() - fork.AbbDto.CreatedAt.ToLocalTime() > _options.CleanupLifetime)
                {
                    hashes.Add(key);
                }
            }
            
            hashes.ForEach(x => provider.BlockingForks.Remove(x, out _));

            return Task.CompletedTask;
        }
    }
}