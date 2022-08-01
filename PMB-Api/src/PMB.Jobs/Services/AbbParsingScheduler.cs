using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PMB.Dal.Bll;
using PMB.Integration.AllBestBets;
using PMB.Jobs.Mappers;
using PMB.Utilities;

namespace PMB.Jobs.Services
{
    public class AbbParsingScheduler: BackgroundScheduler
    {
        public AbbParsingScheduler(IServiceProvider provider) : base("*/3 * * * * *" , provider)
        {
        }

        protected override async Task Process(IServiceScope scope)
        {
            var forksProvider = scope.ServiceProvider.GetRequiredService<ForksProvider>();
            var client = scope.ServiceProvider.GetRequiredService<AbbApiClient>();

            var result = await client.SearchAbbBets(new ArbApiRequest
            {
                Per_page = 30,
                Search_filter = new List<long>
                {
                    465266
                },
                Access_token = "142ea624f81745ed80a883d6d91f71f7",
                Sort_by = ArbApiRequestSort_by.Age
            }, CancellationToken.None);

            var forks = result?.Convert();

            if (forks?.Length > 0)
            {
                forksProvider.Provide(forks.ToList());
                Console.WriteLine($"{forks.Length} forks fetched from AllBestBets");
            }
        }
    }
}