using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSpider.DataService
{
    class MySqlConnectionHelper
    {
        public static MySqlConnection MysqlConn()
        {
            string connStr = "Data Source=localhost;Database=watchecology;User Id=root;charset=utf8mb4;Password=11111;port=3306;SslMode=None;";
            var connection = new MySqlConnection(connStr);
            return connection;
        }
    }
}
