using Dapper;
using NewsSpider.DataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSpider.Action
{
    public class PreAction
    {
        /// <summary>
        /// 启动爬虫前清空数据库的新闻表
        /// </summary>
        public static void DeleteAllNewsData()
        {
            var conn = MySqlConnectionHelper.MysqlConn();
            conn.ExecuteAsync("truncate animalnews;truncate animalnewsdetail");
        }
    }
}
