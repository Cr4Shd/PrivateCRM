using Syntax_Imotion_Lexika.DBItems;
using Syntax_Imotion_Lexika.DBUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
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
    public sealed partial class ShowTickets : Page
    {
        public ShowTickets()
        {
            this.InitializeComponent();
        }

        private void Get_Tickets_Btn_Click(object sender, RoutedEventArgs e)
        {
            List<DBItems.Ticket> ticketList = new List<DBItems.Ticket>();
            ticketList = DBPasstrough.ReadTicketDB();

            TicketListView.ItemsSource = ticketList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private string SetSolvedStatus(Ticket ticket)
        {
            if (ticket.Solved == 1)
            {
                return "Ja";
            }
            else
            {
                return "Nein";
            }
        }

        private void TicketListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var param = e.ClickedItem as DBItems.Ticket;
            Frame.Navigate(typeof (ChangeTicketInformations), param);
            
        }

        private void Unslvd_Btn_Click(object sender, RoutedEventArgs e)
        {
            List<DBItems.Ticket> ticketList = new List<DBItems.Ticket>();
            ticketList = DBPasstrough.ReadTicketDBUnsolved();

            TicketListView.ItemsSource = ticketList;
        }

        private void Slvd_Btn_Click(object sender, RoutedEventArgs e)
        {
            List<DBItems.Ticket> ticketList = new List<DBItems.Ticket>();
            ticketList = DBPasstrough.ReadTicketDBSolved();

            TicketListView.ItemsSource = ticketList;
        }
    }
}
