using NewsSpider.Action;
using NewsSpider.DataService;
using NewsSpider.Spiders;
using System;

namespace NewsSpider
{
    public class RunNewsSpider
    {
        public static void Run()
        {
            PreAction.DeleteAllNewsData();
            SougouAnimalNewsSpider sougouAnimalNewsSpider = new SougouAnimalNewsSpider(20);
            sougouAnimalNewsSpider.RunSpider();

            AnimalNewsService.DeleteRepeatUrl();

            SougouAnimalNewsDetailSpider sougouAnimalNewsDetailSpider = new SougouAnimalNewsDetailSpider(AnimalNewsService.GetAllUrls());
            sougouAnimalNewsDetailSpider.RunSpider();

            AnimalNewsDetailService.DeleteNullText();
        }
    }
}
