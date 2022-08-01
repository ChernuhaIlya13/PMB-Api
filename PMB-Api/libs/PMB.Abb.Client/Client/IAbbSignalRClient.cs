using System;
using System.Threading;
using System.Threading.Tasks;

namespace PMB.Abb.Client.Client
{
    public interface IAbbSignalRClient: IDisposable
    {
        Task Start(string jwtToken, CancellationToken token);
    }
}