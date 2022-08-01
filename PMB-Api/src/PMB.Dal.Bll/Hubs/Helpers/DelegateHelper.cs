using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PMB.Dal.Bll.Mappers;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;

namespace PMB.Dal.Bll.Hubs.Helpers
{
    public static class DelegateHelper
    {
        public static Func<V1ForkDal, bool> ForkFilterCondition(UserCacheValue user)
        {
            var settings = user.Filters?.Convert();
            var allBookmakers = settings?.Bookmakers.Select(b => b.Replace("Ru","")).ToArray();
            
            return f =>
            {
                return allBookmakers!.Any(book => f.FirstBet.Bookmaker.Contains(book,StringComparison.OrdinalIgnoreCase)) &&
                       allBookmakers.Any(book => f.SecondBet.Bookmaker.Contains(book,StringComparison.OrdinalIgnoreCase)) &&
                       settings!.TimeOfLife.Start <= f.Lifetime && settings.TimeOfLife.Finish >= f.Lifetime &&
                       settings.Profit.Start <= f.Profit && settings.Profit.Finish >= f.Profit &&
                       settings.Coefficient.Start <= f.FirstBet.Coefficient &&
                       settings.Coefficient.Start <= f.SecondBet.Coefficient &&
                       settings.Coefficient.Finish >= f.FirstBet.Coefficient &&
                       settings.Coefficient.Finish >= f.SecondBet.Coefficient &&
                       f.Id != user.LastForkShipped;
            };
        }

        public static async Task WithIdentity(this HubCallerContext context, Func<string, Task> func)
        {
            if (context.User?.Identity?.Name != null)
            {
                await func(context.User.Identity.Name);
            }
        }
    }
}