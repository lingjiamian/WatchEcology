using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Quartz;
using Quartz.Impl;
using WatchEcology.Quartz.Job;

namespace WatchEcology
{
    public class Program
    {
        private static  IScheduler _scheduler;

        public static void Main(string[] args)
        {
            StartScheduler();
            BuildWebHost(args).Build().Run();
        }

        private static void StartScheduler()
        {
            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.Start().Wait();

            var spideAnimalNewsJob = JobBuilder.Create<SpideAnimalNewsJob>()
                .WithIdentity("spideAnimalNewsJob")
                .Build();

            var spideAnimalNewsTrigger = TriggerBuilder.Create()
                .WithIdentity("spideAnimalNewsTrigger")
                .StartNow()
                .WithSimpleSchedule(s => s.WithIntervalInHours(3))
                .Build();

            _scheduler.ScheduleJob(spideAnimalNewsJob, spideAnimalNewsTrigger);

        }

        public static IWebHostBuilder BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel();
    }
}
