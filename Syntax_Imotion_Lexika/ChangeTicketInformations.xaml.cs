using Syntax_Imotion_Lexika.DBUtils;
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

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Syntax_Imotion_Lexika
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ChangeTicketInformations : Page
    {
        public string ActualTicketId { get; set; }
        public ChangeTicketInformations()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = e.Parameter as DBItems.Ticket;
            Ticket_TextBlock.Text = parameter.TicketText;
            Titel_TextBlock.Text = parameter.Title;
            Autor_TextBlock.Text = parameter.Author;
            Client_ID_TextBlock.Text = parameter.ClientID;
            bool solv = CheckSolvedStatus(parameter.Solved);
            ActualTicketId = parameter.ID;

            if (solv)
            {
                Rd_Btn_Solved.IsChecked = true;
            }
            else
            {
                Rd_Btn_Unsolved.IsChecked = true;
            }
            Date_TextBlock.Text = parameter.CreationTime.ToString();

        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Rd_Btn_Solved.IsChecked == true)
            {
                DBPasstrough.UpdateTicket(ActualTicketId);
            }
            
        }
        private bool CheckSolvedStatus(int par0)
        {
            if (par0 == 0)
            {
                return false;
            }
            if(par0 == 1)
            {
                return true;
            }
            return false;
        }

        private void Return_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShowTickets));
        }
    }
}
