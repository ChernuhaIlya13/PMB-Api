using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Subjects;
using PMB.Abb.Models.Models;

namespace PMB.Abb.Client.Providers
{
    public interface IAbbForksProvider : IDisposable
    {
        Subject<bool> ForksReceived { get; }

        IReadOnlyCollection<AbbFork> Forks { get; }

        internal ConcurrentDictionary<string, AbbFork> BlockingForks { get; }
        
        internal Subject<AbbFork> AbbForks { get; }

        AbbFork Pop(Func<AbbFork, bool> predicate);
    }
}