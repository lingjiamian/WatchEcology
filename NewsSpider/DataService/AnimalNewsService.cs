using Dapper;
using MySql.Data.MySqlClient;
using NewsSpider.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NewsSpider.DataService
{
    /// <summary>
    /// 处理新闻数据
    /// </summary>
    public class AnimalNewsService
    {
        /// <summary>
        /// 插入数据库
        /// </summary>
        /// <param name="newsDetails">新闻列表</param>
        public static void InsertANewsList(ICollection<AnimalNews> newsDetails)
        {
            string sqlStr = "insert into animalnews values (@Id,@Title,@Url,@ImgUrl,@Source,@PubDate)";
            MySqlConnection dbConnection = MySqlConnectionHelper.MysqlConn();
            dbConnection.Execute(sqlStr, newsDetails);
        }

        public static void ExecuteSql(string strSql)
        {
            MySqlConnection mySqlConnection = MySqlConnectionHelper.MysqlConn();
            mySqlConnection.ExecuteAsync(strSql);
        }

        /// <summary>
        /// 执行删除重复Url存储过程
        /// </summary>
        public static void DeleteRepeatUrl()
        {
            var conn = MySqlConnectionHelper.MysqlConn();
            conn.ExecuteAsync("deleteRepeatUrl", commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// 返回所有Url
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllUrls()
        {
            string sqlStr = "select Url from animalnews";
            MySqlConnection dbConnection = MySqlConnectionHelper.MysqlConn();
            return dbConnection.Query<string>(sqlStr).AsList();
        }

        /// <summary>
        /// 删除重复的Url    
        /// </summary>
        /// <param name="url">Url</param>
        //public static void DeleteUrl(string url)
        //{
        //    string sqlStr = "delete from animalnews where Url = @url";
        //    MySqlConnection mySqlConnection = MySqlHelper.MysqlConn();
        //    mySqlConnection.Execute(sqlStr, new { url });
        //}
    }
}
