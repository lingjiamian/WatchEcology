using Dapper;
using MySql.Data.MySqlClient;
using NewsSpider.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSpider.DataService
{
    public class AnimalNewsDetailService
    {
        /// <summary>
        /// 插入新闻列表
        /// </summary>
        /// <param name="newsDetail"></param>
        public static void InsertAnimalNewsDetail(AnimalNewsDetail newsDetail)
        {
            string sqlStr = "insert into animalnewsdetail values(@ID,@Text,@Url)";
            MySqlConnection mySqlConnection = MySqlConnectionHelper.MysqlConn();
            mySqlConnection.Execute(sqlStr, newsDetail);
        }

        public static void ExecuteSql(string strSql)
        {
            MySqlConnection mySqlConnection = MySqlConnectionHelper.MysqlConn();
            mySqlConnection.ExecuteAsync(strSql);
        }

        /// <summary>
        /// 删除文本为空的新闻详情
        /// </summary>
        public static void DeleteNullText()
        {
            string sqlStr = "delete from animalnewsdetail where Text is NULL";
            var conn = MySqlConnectionHelper.MysqlConn();
            conn.ExecuteAsync(sqlStr);
        }

        /// <summary>
        /// 获取新闻列表的全部url
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllUrls()
        {
            string sqlStr = "select Url from animalnewsdetail";
            MySqlConnection mySqlConnection = MySqlConnectionHelper.MysqlConn();
            return mySqlConnection.Query<string>(sqlStr).AsList();
        }
    }
}
