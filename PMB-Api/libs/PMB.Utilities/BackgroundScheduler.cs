using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace PMB.Utilities
{
    public abstract class BackgroundScheduler: BackgroundService
    {
        private readonly CrontabSchedule _cronSchedule;
        private DateTime _nextRun;
        private readonly IServiceProvider _provider; 
        
        protected BackgroundScheduler(string schedule, IServiceProvider provider)
        {
            _provider = provider;
            _cronSchedule = CrontabSchedule.Parse(schedule,new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _cronSchedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                if (now > _nextRun)
                {
                    using (var scope = _provider.CreateScope())
                    {
                        await Process(scope);
                    }
                    _nextRun = _cronSchedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay((int)_nextRun.Subtract(DateTime.Now).TotalMilliseconds, stoppingToken); 
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        protected abstract Task Process(IServiceScope scope);
    }
}