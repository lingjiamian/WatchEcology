using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Downloader;
using DotnetSpider.Extraction;
using NewsSpider.DataService;
using NewsSpider.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace NewsSpider.Spiders
{
    public class SougouAnimalNewsSpider
    {
        List<string> urlList = new List<string>();

        public SougouAnimalNewsSpider(int pageCount)
        {
            for (int i = 1; i <= pageCount; i++)
            {
                string url = "http://news.sogou.com/news?mode=2&manual=&query=site%3Asohu.com+%B6%AF%CE%EF&time=0&sort=1&page=" + i + "&w=03009900&dr=1&_asf=news.sogou.com&_ast=1525866240";
                urlList.Add(url);
            }
        }

        public void RunSpider()
        {
            var spider = Spider.Create(new SougouAnimalNewsProcessor()).AddRequests(urlList).AddPipeline(new SougouAnimalNewsPipeline());
            spider.ThreadNum = 4;
            spider.Run();
        }
    }


    class SougouAnimalNewsProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {
            var results = page.Selectable().XPath(".//div[@class='vrwrap']").Nodes();
            List<AnimalNews> newsList = new List<AnimalNews>();
            foreach (var result in results)
            {
                var caption = result.XPath(".//caption").GetValue(ValueOption.InnerText);
                if (caption != null)
                {
                    continue;
                }
                AnimalNews news = new AnimalNews
                {
                    Title = result.XPath(".//h3[@class='vrTitle']/a").GetValue(ValueOption.InnerText).Replace("&rdquo;", "").Replace("&ldquo;", "").Replace("&mdash;", ""),
                    Url = result.XPath(".//h3[@class='vrTitle']/a/@href").GetValue(ValueOption.InnerText)
                };
                if (newsList.Exists(n => n.Url == news.Url))
                {
                    continue;
                }
                string imgUrl = result.XPath(".//a[@class='news-pic']/img/@src").GetValue();
                string newsFrom = result.XPath(".//p[@class='news-from']").GetValue(ValueOption.InnerText);
                string dateStr = newsFrom.Substring(newsFrom.IndexOf(';') + 1);
                DateTime pubDate = new DateTime();
                if (DateTime.TryParse(dateStr, out pubDate))
                {
                    news.PubDate = pubDate;
                }
                else
                {
                    news.PubDate = HandlerDate(dateStr);
                }

                if (imgUrl != null)
                {
                    if (imgUrl != page.TargetUrl)
                    {
                        news.ImgUrl = imgUrl.Replace("amp;", "");
                    }
                }

                news.Source = "搜狐网";
                newsList.Add(news);
            }

            page.AddResultItem("newsList", newsList);
        }

        private static DateTime HandlerDate(string dateStr)
        {
            DateTime pubDate = DateTime.Now;
            if (dateStr.Contains("小时前"))
            {
                if (int.TryParse(dateStr.Replace("小时前", ""), out int hoursAgo))
                {
                    pubDate = pubDate.AddHours(-hoursAgo);
                }
            }
            else
            {
                if (int.TryParse(dateStr.Replace("分钟前", ""), out int minAgo))
                {
                    pubDate = pubDate.AddMinutes(-minAgo);
                }
            }
            return pubDate;
        }
    }

    class SougouAnimalNewsPipeline : BasePipeline
    {
        //public override void Process(IEnumerable<ResultItems> resultItems, ISpider spider)
        //{
        //    foreach (var result in resultItems)
        //    {
        //        List<News> newsList = result.Results["newsList"] as List<News>;
        //        ANewsData.InsertANewsList(newsList);
        //    }
        //}

        public override void Process(IList<ResultItems> resultItems, dynamic sender = null)
        {
            List<AnimalNews> newsList = new List<AnimalNews>();
            foreach (var result in resultItems)
            {
                foreach(var news in result)
                {
                    newsList = (List<AnimalNews>)news.Value;
                }
                AnimalNewsService.InsertANewsList(newsList);
            }
        }
    }
}

