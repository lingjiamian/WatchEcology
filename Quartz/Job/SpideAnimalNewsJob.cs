using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchEcology.Quartz.Service;

namespace WatchEcology.Quartz.Job
{
    public class SpideAnimalNewsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            SpideAnimalNewsService spideAnimalNewsService = new SpideAnimalNewsService();
            return spideAnimalNewsService.RunAnimalNewsSpider();
        }
    }
}
