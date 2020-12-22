using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchEcology.Models;
using Newtonsoft.Json;

namespace WatchEcology.Controllers
{
    [Produces("application/json")]
    [Route("api/NewsData")]
    public class NewsDataController : Controller
    {
        public readonly WatchecologyContext context;

        public NewsDataController(WatchecologyContext watchecologyContext)
        {
            context = watchecologyContext;
        }

        /// <summary>
        /// 根据页数返回新闻页
        /// </summary>
        /// <param name="page">页数</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetNewsData")]
        public IActionResult GetData(int page=1)
        {

            var list = context.Animalnews.OrderByDescending(a => a.PubDate).Skip((page-1)*20).Take(20).ToList();

            var data = list.Select(a => new { a.Title, a.Url, a.ImgUrl, a.Source, PubDate = DateStringFromNow(a.PubDate) });

            bool success = false;
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.MaxDepth = 10;
            if (data != null)
            {
                success = true;
            }
            var obj = new { data, success };
            return Json(obj, setting);
        }

        //[HttpGet]
        //[Route("GetData")]
        //public IActionResult GetJsonFile()
        //{
        //    var newsdetailList = context.Anewsdetail.ToList();
        //    var anewsList = context.Animalnews.ToList();
        //    var obj = new { newsdetailList, anewsList };
        //    return Json(obj);
        //}


        Dictionary<string, string>[] HasImg(string imgUrl)
        {
            Dictionary<string, string>[] Img = new Dictionary<string, string>[1];
            if (imgUrl != null)
            {
                Img[0] = new Dictionary<string, string>
                {
                    ["imgurl"] = imgUrl
                };
            }
            else
            {
                Img = null;
            }
            return Img;
        }

        
        //[HttpGet]
        //[Route("GetNewsDetail")]
        //public IActionResult GetNewsDetailList()
        //{
        //    var allDetail = context.Anewsdetail.Take(20).ToList() ;
        //    var obj = new {data = allDetail };
        //    return Json(obj);
        //}
        
         

        [HttpPost]
        [Route("GetNewsDetail")]
        public IActionResult GetNewsDetailByUrl([FromBody] string url)
        {
            var data = context.Anewsdetail.FirstOrDefault(news => news.Url == url);
            return Json(data);
        }

        /// <summary>
        /// 把日期格式转换成字符串格式
        /// </summary>
        /// <param name="dt">要转换的日期</param>
        /// <returns></returns>
        string DateStringFromNow(DateTime? dt)
        {
            TimeSpan timeSpan = DateTime.Now - dt.Value;

            if (timeSpan.Days > 0)
            {
                return string.Format($"{timeSpan.Days}天前");
            }
            else if (timeSpan.Hours > 0)
            {
                return string.Format($"{timeSpan.Hours}小时前");
            }
            else
            {
                return string.Format($"{timeSpan.Minutes}分钟前");
            }
        }
    }
}