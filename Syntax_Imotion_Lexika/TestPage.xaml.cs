using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syntax_Imotion_Lexika.DBUtils;
using MySql.Data.MySqlClient;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Syntax_Imotion_Lexika
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TestPage : Page
    {
        public TestPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DBObjectParser test = new DBObjectParser(DateTime.Now);
        }

        //TEST
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
    }
}
