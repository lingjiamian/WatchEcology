using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchEcology.Models;

namespace WatchEcology.Controllers
{
    [Produces("application/json")]
    [Route("api/QuestionData")]
    public class QuestionDataController : Controller
    {
        private readonly WatchecologyContext context;

        public QuestionDataController(WatchecologyContext watchecologyContext)
        {
            context = watchecologyContext;
        }

        public JsonResult GetData()
        {

            var list = context.Question.Include(q => q.Option).ToList();
            int i = 0;
            foreach (var q in list)
            {
                foreach (var op in q.Option)
                {
                    if (i == 0)
                    {
                        q.OptionA = op.OptionContent;
                    }
                    else if (i == 1)
                    {
                        q.OptionB = op.OptionContent;
                    }
                    else if (i == 2)
                    {
                        q.OptionC = op.OptionContent;
                    }
                    else if (i == 3)
                    {
                        q.OptionD = op.OptionContent;
                    }
                    i++;
                }
                i = 0;
            }
            context.SaveChanges();
            var data = list.Select(q => new { q.QuestionContent, q.Answer, q.BgKlg, Option = HandleOption(q.Option) });
            var obj = new { data };
            return Json(obj);
        }

        private Dictionary<string, string> HandleOption(ICollection<Option> Option)
        {
            int i = 0;
            //string[] strArr = new string[Option.Count];
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var option in Option)
            {
                string key = "option";
                if (i == 0)
                {
                    key += "A";
                }
                else if (i == 1)
                {
                    key += "B";
                }
                else if (i == 2)
                {
                    key += "C";
                }
                else
                {
                    key += "D";
                }
                i++;
                dict.Add(key, option.OptionContent);
            }
            return dict;
        }
    }
}