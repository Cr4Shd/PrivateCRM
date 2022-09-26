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
    public sealed partial class CreateTicket : Page
    {
        public CreateTicket()
        {
            this.InitializeComponent();
        }

        //public Ticket(string title, DateTime createTime, string ticketText, string author, Guid id, int solved)
        //{
        //    this.Title = title;
        //    this.CreationTime = createTime;
        //    this.TicketText = ticketText;
        //    this.Author = author;
        //    this.ID = ID.ToString();
        //    this.Solved = solved;
        //}

        private void Create_Ticket_Btn_Click(object sender, RoutedEventArgs e)
        {
            var title = Title_TextBox.Text;
            var autor = Autor_TextBox.Text;
            var text = Problem_TextBox.Text;
            var clientId = Client_ID_TextBox.Text;

            DBItems.Ticket ticket = new DBItems.Ticket(title, DateTime.Now, text, autor, Guid.NewGuid(), 0, clientId);
            DBPasstrough.WriteTicket(ticket);

        }
        public void ClearBoxes()
        {
            
        }

        private void Main_Menu_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
