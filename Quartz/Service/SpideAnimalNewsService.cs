using NewsSpider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchEcology.Quartz.Service
{
    public class SpideAnimalNewsService
    {
        public  Task RunAnimalNewsSpider()
        {
            return Task.Run(() =>
            {
                RunNewsSpider.Run();
            });
        }
    }
}
