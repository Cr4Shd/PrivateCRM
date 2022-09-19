using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Syntax_Imotion_Lexika.DBItems
{
    class Ticket
    {
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public string TicketText { get; set; }
        public string Author { get; set; }
        public string ID { get; set; }
        public int Solved { get; set; }

        /// <summary>
        /// Leerer Konstruktor für Testweise Erstellung und generelles erstellen
        /// </summary>
        public Ticket()
        {

        }
        /// <summary>
        /// Das ist der Konstruktor für eine Neuerstellung 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="createTime"></param>
        /// <param name="ticketText"></param>
        /// <param name="author"></param>
        public Ticket(string title, DateTime createTime, string ticketText, string author, Guid id, int solved)
        {
            this.Title = title;
            this.CreationTime = createTime;
            this.TicketText = ticketText;
            this.Author = author;
            this.ID = ID.ToString();
            this.Solved = solved;
        }
    }
}
