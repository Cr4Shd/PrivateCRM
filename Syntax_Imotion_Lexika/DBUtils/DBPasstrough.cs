using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Syntax_Imotion_Lexika.DBItems;
using Windows.Devices.PointOfService;

namespace Syntax_Imotion_Lexika.DBUtils
{
    /// <summary>
    /// Das ist die Hauptklasse zum Zugriff und der Bearbeitung der Hauptabellen welche sich auf einer lokalen DB befinden. 
    
    /// </summary>
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

                string dataString = $"INSERT INTO kontakte (ID, Name, Datum, Anliegen, AnrufID, Erledigt) VALUES " +
                    $"('{client.ID}', '{client.Name}', '{client.Date.ToString("yyyy-MM-dd HH:mm:ss")}','{client.ReasonForCall}','{client.Unq_Call_ID}','{client.Case_Closed}')";
                Console.WriteLine(dataString);
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                var rowsAff = command.ExecuteNonQuery();
                Console.WriteLine("Es wurden +" + rowsAff + " beschrieben.");
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

                string dataString = $"INSERT INTO problems(Problemo, Solution, Autor, KeyWord, Code) VALUES('{problemCase.Description.ToLower()}', '{problemCase.Solution}', '{problemCase.Author}', '{problemCase.IdWord.ToLower()}', '{problemCase.Code}')";
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
                reader = command.ExecuteReader(); 


                while (reader.Read())
                {
                    var idX = reader.GetString(0);
                    var nameX = reader.GetString(1);
                    DateTime dateX = reader.GetDateTime(2);
                    var reasonX = reader.GetString(3);
                    var test = reader.IsDBNull(4);
                    //var iDX = reader.GetString(4);

                    //var callInternIdX = reader.GetString(4);
                    //var IsSolved = reader.GetInt32(5);

                    clients.Add(new DBItems.Client(idX, nameX, dateX, reasonX /*callInternIdX, IsSolved*/));
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
                var resultClient = resultClientList[resultClientList.Count - 1];
                return resultClient;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return null;
            }
        }
        #endregion

        #region ReadClientLIst
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
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                var idX = reader.GetString(0);
                var nameX = reader.GetString(1);
                DateTime dateX = reader.GetDateTime(2);
                var reasonX = reader.GetString(3);
                //var uIdX = reader.GetString(4);
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
            // var result =
            //    from item in clients
            //    where item.ID.Equals(id)
            //    select item;

            var resultClientList = clientList.ToList();
            return resultClientList;
        }
        #endregion // Funktion ist noch WIP

        #region ReadProblemDB
        
        public static DBItems.ProblemCase ReadProblemsDB(string prob)
        {
            DBObjectParser parser = new DBObjectParser();
            try
            {
                List<DBItems.ProblemCase> cases = new List<DBItems.ProblemCase>();
                List<DBItems.ProblemCase> casesList = new List<DBItems.ProblemCase>();
                string dataReturn = string.Empty;
                var connectionString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectionString.ConnectionString);

                string dataString = "SELECT * FROM problems;";
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                MySqlDataReader reader;
                reader = command.ExecuteReader(); 


                while (reader.Read())
                {
                    var idword = reader.GetString(3);
                    var desrc = reader.GetString(0);
                    var solut = reader.GetString(1);
                    var auth = reader.GetString(2);



                    cases.Add(new DBItems.ProblemCase(idword, desrc, solut, auth));
                }

                connection.Close();

                var bufferList =
                    from item in cases
                    where item.Description.Contains(prob)
                    select item;

                casesList = bufferList.ToList();

                if (casesList.Count < 1)
                {
                    return new DBItems.ProblemCase("PROBLEM_NOT_FOUND");
                }

                return casesList[0];

            }
            catch (MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return null;
            }


        }
        #endregion

        #region ReadProblemList
        public static List<DBItems.ProblemCase> ReadDBProblemList(string prob)
        {
            try
            {
                List<DBItems.ProblemCase> cases = new List<DBItems.ProblemCase>();
                List<DBItems.ProblemCase> casesList = new List<DBItems.ProblemCase>();
                string dataReturn = string.Empty;
                var connectionString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectionString.ConnectionString);

                string dataString = "SELECT * FROM problems;";
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                MySqlDataReader reader;
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    var idword = reader.GetString(3);
                    var desrc = reader.GetString(0);
                    var solut = reader.GetString(1);
                    var auth = reader.GetString(2);
                    cases.Add(new DBItems.ProblemCase(idword, desrc, solut, auth));
                }

                connection.Close();

                var bufferList =
                    from item in cases
                    where item.IdWord.Contains(prob)
                    select item;

                casesList = bufferList.ToList();

                
                return casesList;

            }
            catch (MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return null;
            }
        }
        #endregion

        #region WriteTicketDB

        /// <summary>
        /// Diese Methode wird verwendet um Tickets in die DB einzupflegen.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static bool WriteTicket(DBItems.Ticket ticket)
        {
            try
            {
                var connectionString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectionString.ConnectionString);

                string dataString = $"INSERT INTO tickets (Titel, Datum, Text, Autor, ID, Solved, KundenNr) VALUES ('{ticket.Title}','{ticket.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")}','{ticket.TicketText}','{ticket.Author}','{ticket.ID}','{ticket.Solved}', '{ticket.ClientID}')";
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                var rowsAff = command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch(MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return false;
            }
        }

        #endregion

        #region ReadTicketDB
        public static List<DBItems.Ticket> ReadTicketDB()
        {
            List<DBItems.Ticket> ticketlist = new List<DBItems.Ticket>();
            try
            {
                string dataReturn = string.Empty;
                var connectionString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectionString.ConnectionString);

                string dataString = "SELECT * FROM tickets;";
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                MySqlDataReader reader;
                reader = command.ExecuteReader(); // Die Connection scheint zu stehen - jedoch ist die Syntax für die Anfrage falsch :D 


                while (reader.Read())
                {
                    var title = reader.GetString(0);
                    DateTime date = reader.GetDateTime(1);
                    var text = reader.GetString(2);
                    var autor = reader.GetString(3);
                    Guid id;
                    Guid.TryParse(reader.GetString(4), out id);
                    var solved = reader.GetInt32(5);
                    var cId = reader.GetString(6);

                    ticketlist.Add(new DBItems.Ticket(title, date, text, autor, id, solved, cId));
                }

                connection.Close();
                return ticketlist;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return null;
            }

        }
        #endregion

        #region ReadTicketDBSolved
        public static List<DBItems.Ticket> ReadTicketDBSolved()
        {
            List<DBItems.Ticket> ticketlist = new List<DBItems.Ticket>();
            List<DBItems.Ticket> SolvTicketList = new List<DBItems.Ticket>();
            try
            {
                string dataReturn = string.Empty;
                var connectionString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectionString.ConnectionString);

                string dataString = "SELECT * FROM tickets;";
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                MySqlDataReader reader;
                reader = command.ExecuteReader(); // Die Connection scheint zu stehen - jedoch ist die Syntax für die Anfrage falsch :D 


                while (reader.Read())
                {
                    var title = reader.GetString(0);
                    DateTime date = reader.GetDateTime(1);
                    var text = reader.GetString(2);
                    var autor = reader.GetString(3);
                    Guid id;
                    Guid.TryParse(reader.GetString(4), out id);
                    var solved = reader.GetInt32(5);
                    var cId = reader.GetString(6);

                    ticketlist.Add(new DBItems.Ticket(title, date, text, autor, id, solved, cId));
                }

                connection.Close();

                var bufferlist =
                    from itemX in ticketlist
                    where itemX.Solved == 1
                    select itemX;
                SolvTicketList = bufferlist.ToList();

                return SolvTicketList;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return null;
            }
        }
        #endregion

        #region ReadTicketDBUnsolved
        public static List<DBItems.Ticket> ReadTicketDBUnsolved()
        {
            List<DBItems.Ticket> ticketlist = new List<DBItems.Ticket>();
            List<DBItems.Ticket> SolvTicketList = new List<DBItems.Ticket>();
            try
            {
                string dataReturn = string.Empty;
                var connectionString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectionString.ConnectionString);

                string dataString = "SELECT * FROM tickets;";
                MySqlCommand command = new MySqlCommand(dataString, connection);

                connection.Open();
                MySqlDataReader reader;
                reader = command.ExecuteReader(); // Die Connection scheint zu stehen - jedoch ist die Syntax für die Anfrage falsch :D 


                while (reader.Read())
                {
                    var title = reader.GetString(0);
                    DateTime date = reader.GetDateTime(1);
                    var text = reader.GetString(2);
                    var autor = reader.GetString(3);
                    Guid id;
                    Guid.TryParse(reader.GetString(4), out id);
                    var solved = reader.GetInt32(5);
                    var cId = reader.GetString(6);

                    ticketlist.Add(new DBItems.Ticket(title, date, text, autor, id, solved, cId));
                }

                connection.Close();

                var bufferlist =
                    from itemX in ticketlist
                    where itemX.Solved == 0
                    select itemX;
                SolvTicketList = bufferlist.ToList();

                return SolvTicketList;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return null;
            }
        }
        #endregion

        #region UpdateTicketDB
        /// <summary>
        ///  Diese Methode ist dazu gedacht den Status eines Tickets von "offen" auf "erledigt" gesettzt wird.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static bool UpdateTicket(string par0)
        {
            try
            {
                var connectionString = DBConfigs.stringBuilder;
                var connection = new MySqlConnection(connectionString.ConnectionString);
                string dataString = $"UPDATE tickets SET Solved = 1 WHERE ID ='{par0}'";
                MySqlCommand command = new MySqlCommand(dataString, connection);
                connection.Open();
                var rowsAff = command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Stacktrace ist : " + e.StackTrace.ToString());
                Console.WriteLine("Fehlercode ist : " + e.Code);
                Console.WriteLine("Fehlername ist : " + e.Message);
                return false;
            }
        }
        #endregion

    }
}
