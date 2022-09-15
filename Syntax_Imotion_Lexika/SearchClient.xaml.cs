using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Syntax_Imotion_Lexika
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class SearchClient : Page
    {
        //public ObservableCollection<DBItems.Client> ClientList { get;}
        //    = new ObservableCollection<DBItems.Client>();
        public SearchClient()
        {
            this.InitializeComponent();

        }
        //WIP
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Hier muss ich beachten das wenn ich einen leeren String übergeben bekommen, z.b für die ID/Name, 
            var id = Client_ID_Enter.Text;
            var name = Client_Name_Enter.Text;

            DBItems.Client client = DBUtils.DBPasstrough.ReadClientsDB(id, name);
            
            if (client.ID.Equals("CLIENT_NOT_FOUND"))
            {
                Client_ID.Text = "Klient nicht gefunden";
                Client_Name.Text = string.Empty;
                Date_LastCall.Text = string.Empty;
                Reason_Call.Text = string.Empty;
                
                return;
            }
            else
            {
                
                Client_ID.Text = client.ID;
                Client_Name.Text = client.Name;
                Date_LastCall.Text = client.Date.ToString();
                Reason_Call.Text = client.ReasonForCall;
                GenerateObservable(id, name);
            }

        }
        public void GenerateObservable(string id,string name)
        {
            List<DBItems.Client> clientList = new List<DBItems.Client>();
            clientList = DBUtils.DBPasstrough.ReadClientListDB(id,name);

            
            ClientListView.ItemsSource = clientList;
        }

        private void MainMenu_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Create_Call_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate (typeof(ClientContact));
        }

        private void Call_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            try
            {
                
                var item = ClientListView.SelectedItem;
                var cliento = (DBItems.Client)item;
                if (cliento != null)
                {
                    ////WIP! :D
                    Console.WriteLine("hold");
                    string recieveInformation = $"Kunde : {cliento.Name}\n ID : {cliento.ID}\n Datum des letzten Anrufs : {cliento.Date}\n Grund des Anrufs : {cliento.ReasonForCall}";
                    MessageDialog msgDialog = new MessageDialog(recieveInformation);
                    msgDialog.ShowAsync();
                }
                else
                {
                    return;
                }
            }
            catch(Exception)
            {

            }
        }

        // Ok hier bekomme ich durch die EventArgs den Namen mit, bzw die ID, und damit kann ich theoretisch dann nach dem Client suchen.
        // Ich bekomm aber dadurch noch nicht die uid mit, welche ich theoretisch bräuchte um nach dem spezifischen call zu suchen .
        private void ClientListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as DBItems.Client;
            Frame.Navigate(typeof(ClientCallDetails), item);
            
            var x = e;
        }
    }
}
