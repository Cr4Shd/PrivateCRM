using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ClientContact : Page
    {
        public bool AltDate { get; set; }
        public ClientContact()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!AltDate)
            {


                Result_Block.Text = String.Empty;
                var client_id = Client_ID.Text;
                var client_name = Client_Name.Text;
                var client_reason = Client_Reason.Text;
                var result = DBUtils.DBPasstrough.WriteClientDB(new DBItems.Client(client_id, client_name, client_reason));

                if (result)
                {

                    Result_Block.Text = "Kunde wurde erfolgreich gespeichert!";
                    
                }
                else
                {
                    Result_Block.Text = "Kunde wurde nicht gespeichert!";
                }
            }
            //WIP für alternatives Datum bzw. nachträgliches Anlegen
            else
            {
                Result_Block.Text = String.Empty;
                var client_id = Client_ID.Text;
                var client_name = Client_Name.Text;
                var client_reason = Client_Reason.Text;
                
                var result = DBUtils.DBPasstrough.WriteClientDB(new DBItems.Client(client_id, client_name, client_reason));
            }
        }

        private void Btn_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Syntax_Imotion_Lexika.MainPage));
        }
        private void ClearBoxes()
        {
            Client_ID.Text = String.Empty;
            Client_Name.Text = String.Empty;
            Client_Reason.Text = String.Empty;
            Result_Block.Text = String.Empty;
        }

        private void Clr_Btn_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AltDate = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AltDate = false;
        }

        
    }
}
