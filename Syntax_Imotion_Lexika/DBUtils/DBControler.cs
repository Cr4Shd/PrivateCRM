using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;


namespace Syntax_Imotion_Lexika.DBUtils
{
    
    static class DBControler
    {
        
        public static bool CheckDataBase()
        {
            try
            {
                var connectString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectString.ConnectionString);

                connection.Open();
                connection.Close();
                
                return true;
            }
            catch (MySqlException)
            {
                return false;
                
            }
        }
        public static bool CreateDataBase()
        {
            var connectString = DBConfigs.stringBuilder;
            var connection = new MySqlConnection(connectString.ConnectionString);
            var commandString = "CREATE DATABASE IF NOT EXISTS `testos`;";
            MySqlCommand command = new MySqlCommand(commandString, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
        
    }
}
