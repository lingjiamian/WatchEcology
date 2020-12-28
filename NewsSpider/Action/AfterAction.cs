using Dapper;
using NewsSpider.DataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSpider.Action
{
    public class AfterAction
    {
        public static void DeleteRepeatUrl()
        {
            var conn = MySqlConnectionHelper.MysqlConn();
            
            conn.ExecuteAsync("deleteRepeatUrl", commandType: System.Data.CommandType.StoredProcedure);
        }

        public static void DeleteNullText()
        {
            string sqlStr = "delete from animalnewsdetail where Text is NULL";
            var conn = MySqlConnectionHelper.MysqlConn();
            conn.ExecuteAsync(sqlStr);
        }
    }
}
