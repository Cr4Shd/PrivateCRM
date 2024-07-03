using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax_Imotion_Lexika.DBUtils
{
    static class DBConfigs
    {
        
        public static MySqlBaseConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder
        {
            Server = "127.0.0.1",
            Database = "************",
            UserID = "************",
            Password = "************",
            ConnectionTimeout = 10,
            Port = *****,
            Pooling = false
        };
    }
}
