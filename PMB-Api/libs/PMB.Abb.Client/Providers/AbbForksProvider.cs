using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Microsoft.Extensions.Options;
using PMB.Abb.Models;
using PMB.Abb.Models.Models;

namespace PMB.Abb.Client.Providers
{
    internal sealed class AbbForksProvider: IAbbForksProvider
    {
        private static Func<IDictionary<string, object>, string> GetHashFunc => additionalProperties =>
            additionalProperties?.FirstOrDefault(p => 
                    p.Key?.Equals(Consts.ArbHash, StringComparison.OrdinalIgnoreCase) == true)
                .Value?.ToString();

        private readonly ConcurrentDictionary<string, AbbFork> _blockingForks;
        private readonly Subject<AbbFork> _abbForks;
        
        ConcurrentDictionary<string, AbbFork> IAbbForksProvider.BlockingForks => _blockingForks;
        Subject<AbbFork> IAbbForksProvider.AbbForks => _abbForks;
        
        public IReadOnlyCollection<AbbFork> Forks => _blockingForks.Values
            .OrderByDescending(x => x.AbbDto.CreatedAt).ToArray();

        public AbbFork Pop(Func<AbbFork, bool> predicate)
        {
            var candidate = predicate == null ? _blockingForks.Values.FirstOrDefault() : _blockingForks.Values.FirstOrDefault(predicate);
            if (candidate == null)
                return null;
            
            var hash = GetHashFunc(candidate.AbbDto.AdditionalProperties);
            _blockingForks.Remove(hash, out _);
            return candidate;
        }

        public Subject<bool> ForksReceived { get; }

        public AbbForksProvider(IOptions<AbbProviderOptions> options)
        {
            _abbForks = new Subject<AbbFork>();
            ForksReceived = new Subject<bool>();
            _blockingForks = new ConcurrentDictionary<string, AbbFork>();
            var lifetimeBorder = options.Value.CleanupLifetime;
            
            _abbForks.Subscribe(x =>
            {
                if (DateTimeOffset.Now.ToLocalTime() - x.AbbDto.CreatedAt.ToLocalTime() < lifetimeBorder)
                {
                    var hash = GetHashFunc.Invoke(x.AbbDto.AdditionalProperties);

                    var result = false;
                    if (!string.IsNullOrEmpty(hash))
                    {
                        if (_blockingForks.TryGetValue(hash, out _))
                        {
                            _ = _blockingForks.TryRemove(hash, out _);
                        }

                        result = _blockingForks.TryAdd(hash, x);
                    }

                    if (result)
                    {
                        ForksReceived.OnNext(true);
                    }
                }
            });
        }

        public void Dispose()
        {
            _abbForks?.Dispose();
            ForksReceived?.Dispose();
        }
    }
}