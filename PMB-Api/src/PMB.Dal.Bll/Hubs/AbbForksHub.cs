using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PMB.Dal.Bll.Hubs.Helpers;

namespace PMB.Dal.Bll.Hubs
{
    [Authorize]
    public class AbbForksHub: Hub
    {
        public override Task OnConnectedAsync() =>
            Context.WithIdentity(_ => base.OnConnectedAsync());
        
        public override Task OnDisconnectedAsync(Exception exception) => 
            Context.WithIdentity(_ => base.OnDisconnectedAsync(exception));
    }
}