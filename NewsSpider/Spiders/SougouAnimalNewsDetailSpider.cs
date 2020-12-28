using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Extraction;
using NewsSpider.DataService;
using NewsSpider.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace NewsSpider.Spiders
{

    public class SougouAnimalNewsDetailSpider
    {
        readonly List<string> urlList = new List<string>();
        public SougouAnimalNewsDetailSpider(List<string> urlList)
        {
            this.urlList = urlList;
        }

        public void RunSpider()
        {
            var spider = Spider.Create(new SougouAnimalNewsDetailProcessor()).AddPipeline(new SougouAnimalNewsDetailPipeline())
                .AddRequests(urlList);
            spider.ThreadNum = 4;
            spider.Run();
        }
    }

    class SougouAnimalNewsDetailProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {
            AnimalNewsDetail animalNewsDetail = new AnimalNewsDetail
            {
                Text = page.Selectable().XPath(".//div[@class='text']").GetValue(ValueOption.InnerHtml),
                Url = page.TargetUrl
            };
            page.AddResultItem("AnimalNewsDetail", animalNewsDetail);
           
        }
    }

    class SougouAnimalNewsDetailPipeline : BasePipeline
    {
        //public override void Process(IEnumerable<ResultItems> resultItems, ISpider spider)
        //{
        //    foreach (var result in resultItems)
        //    {
        //        AnimalNewsDetail AnimalNewsDetail = result.Results["AnimalNewsDetail"] as AnimalNewsDetail;
        //        AAnimalNewsDetailData.InsertAnimalNewsDetail(AnimalNewsDetail);
        //    }
        //}

        public override void Process(IList<ResultItems> resultItems, dynamic sender = null)
        {
           
            AnimalNewsDetail animalNewsDetail = new AnimalNewsDetail();
            foreach (var result in resultItems)
            {
                animalNewsDetail = result["AnimalNewsDetail"] as AnimalNewsDetail;
                
                AnimalNewsDetailService.InsertAnimalNewsDetail(animalNewsDetail);
            }
        }
    }
}

