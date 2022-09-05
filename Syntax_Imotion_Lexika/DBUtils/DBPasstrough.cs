using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;



namespace Syntax_Imotion_Lexika.DBUtils
{
    
    static class DBPasstrough
    {
        
        #region WriteClientDB
        /// <summary>
        /// Wird zum Schreiben von Kundendaten in die lokale Datenbank verwendet.
        /// </summary>
        /// <param name="client"></param>
        public static bool WriteClientDB(DBItems.Client client)
        {
            try
            {
                var connectString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectString.ConnectionString);

                string dataString = $"INSERT INTO kontakte (ID, Name, Datum, Anliegen) VALUES ('{client.ID}', '{client.Name}', '{client.Date.ToString("yyyy-MM-dd HH:mm:ss")}','{client.ReasonForCall}')";
                Console.WriteLine(dataString);
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                var rowsAff = command.ExecuteNonQuery();
                Console.WriteLine("Es wurden +" +rowsAff+ " beschrieben.");
                connection.Close();
                return true;
            }
            catch(MySql.Data.MySqlClient.MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " +e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " +e.Code);
                Console.WriteLine("Fehlername ist : " +e.Message);
                return false;
            }
        }
        #endregion

        #region WriteProblemDB
        /// <summary>
        /// Wird zum Schreiben von Problemen mit angehängten Lösungen verwendet.
        /// </summary>
        /// <param name="problemCase"></param>
        public static bool WriteProblemDB(DBItems.ProblemCase problemCase)
        {
            try
            {
                var connectionString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectionString.ConnectionString);

                string dataString = $"INSERT INTO problems(Problemo, Solution, Autor) VALUES('{problemCase.Description}', '{problemCase.Solution}', '{problemCase.Author}')";
                Console.WriteLine(dataString);
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                var rowsAff = command.ExecuteNonQuery();
                Console.WriteLine($"Es wurden {rowsAff} beschrieben");
                connection.Close();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return false;
            }
        }
        #endregion

        #region ReadClientDB
        /// <summary>
        /// Liest den letzten Anruf mit dem Kunden aus der Datenbank aus
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DBItems.Client ReadClientsDB(string id, string name)
        {
            DBObjectParser parser = new DBObjectParser();
            try
            {
                List<DBItems.Client> clients = new List<DBItems.Client>();
                List<DBItems.Client> clientList = new List<DBItems.Client>();
                string dataReturn = string.Empty;
                var connectionString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectionString.ConnectionString);

                string dataString = "SELECT * FROM kontakte;";
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                MySqlDataReader reader;
                reader = command.ExecuteReader(); // Die Connection scheint zu stehen - jedoch ist die Syntax für die Anfrage falsch :D 



                while (reader.Read())
                {
                    var idX = reader.GetString(0);
                    var nameX = reader.GetString(1);
                    DateTime dateX = reader.GetDateTime(2);
                    var reasonX = reader.GetString(3);

                    clients.Add(new DBItems.Client(idX, nameX, dateX, reasonX));
                }
                
                connection.Close();

                // Hier muss ich ansetzen für die FilterName und FilterID
                // WIP
                // Wenn in der ID nichts steht sucht er im Namen
                if (!id.Equals(String.Empty))
                {
                    var resTuple = parser.GetTypeOfValue(id);

                    var resultX =
                    from item in clients
                    where item.ID.Equals(resTuple.Item2 as string)
                    select item;
                    clientList = resultX.ToList();
                }
                // Wenn im Namen auch nichts steht muss ich mir noch was überlegen
                if (!name.Equals(String.Empty))
                {
                    var resTuple = parser.GetTypeOfValue(name);

                    var resultX =
                    from item in clients
                    where item.Name.Contains(resTuple.Item2 as string)
                    select item;
                    clientList = resultX.ToList();
                }
                //WIP


                //var result =
                //    from item in clients
                //    where item.ID.Equals(id)
                //    select item;

                var resultClientList = clientList;

                if (resultClientList.Count < 1)
                {
                    return new DBItems.Client("CLIENT_NOT_FOUND");
                }
                // Hier Ansetzen : Innerhalb der UI sollte man ein ListVIewItem einbauen welches die letzten 3 Anrufe zeigt.
                var resultClient = resultClientList[resultClientList.Count -1];
                return resultClient;
            }
            catch(MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return null;
            }
        }

        public static List<DBItems.Client> ReadClientListDB(string id, string name)
        {
            DBObjectParser parser = new DBObjectParser();
            List<DBItems.Client> clients = new List<DBItems.Client>();
            List<DBItems.Client> clientList = new List<DBItems.Client>();

            string dataReturn = string.Empty;
            var connectionString = DBConfigs.stringBuilder;
            var connection = new MySqlConnection(connectionString.ConnectionString);

            string dataString = "SELECT * FROM kontakte;";
            MySqlCommand command = new MySqlCommand(dataString, connection);

            connection.Open();
            MySqlDataReader reader;
            reader = command.ExecuteReader(); // Die Connection scheint zu stehen - jedoch ist die Syntax für die Anfrage falsch :D 


            
            while (reader.Read())
            {
                var idX = reader.GetString(0);
                var nameX = reader.GetString(1);
                DateTime dateX = reader.GetDateTime(2);
                var reasonX = reader.GetString(3);

                clients.Add(new DBItems.Client(idX, nameX, dateX, reasonX));
            }

            connection.Close();



            // WIP
            if (!id.Equals(String.Empty))
            {
                var resTuple = parser.GetTypeOfValue(id);

                var resultX =
                from item in clients
                where item.ID.Equals(resTuple.Item2 as string)
                select item;
                clientList = resultX.ToList();
            }
            // Wenn im Namen auch nichts steht muss ich mir noch was überlegen
            if (!name.Equals(String.Empty))
            {
                var resTuple = parser.GetTypeOfValue(name);

                var resultX =
                from item in clients
                where item.Name.Contains(resTuple.Item2 as string)
                select item;
                clientList = resultX.ToList();
            }
            // WIP
            //var result =
            //    from item in clients
            //    where item.ID.Equals(id)
            //    select item;

            var resultClientList = clientList.ToList();
            return resultClientList;
        }
        #endregion // Funktion ist noch WIP
    }
    //private void FilterByName(Tuple<int, string> value)
    //{

    //}
    //private void FilterById(Tuple<int, string> value)
    //{

    //}
    
}
