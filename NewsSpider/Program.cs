using NewsSpider.Action;
using NewsSpider.DataService;
using NewsSpider.Spiders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSpider
{
    class Program
    {
        static void Main()
        {
            RunNewsSpider.Run();
        }


        static void Spide()
        {
            SougouAnimalNewsSpider newsSpider = new SougouAnimalNewsSpider(20);
            newsSpider.RunSpider();
            
            AfterAction.DeleteRepeatUrl();
            List<string> urls = AnimalNewsService.GetAllUrls();
            SougouAnimalNewsDetailSpider newsDetailSpider = new SougouAnimalNewsDetailSpider(urls);
            newsDetailSpider.RunSpider();

        }
    }
}
