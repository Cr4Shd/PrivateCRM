using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax_Imotion_Lexika.DBItems
{
    /// <summary>
    /// Diese Klasse dient zum Speichern eines Kunden
    /// </summary>
    public class Client
    {
        public string ID{ get; set; }
        public string Name{ get; set; }
        public DateTime Date { get; set; }
        public string ReasonForCall { get; set; }

        public enum Knktr
        {
            Secunet = 1,
            Rise = 2,
        }
        public enum Terminal
        {
            Orga = 1,
            Cherry = 2,
            CherryKeyB = 3
        }
        public Knktr Konnektor { get; set; }
        public Terminal Term { get; set; }
        public Client(string id, string name, string reasonForCall)
        {
            this.ID = id;
            this.Name = name;
            Date = DateTime.Now;
            ReasonForCall = reasonForCall;
        }
        public Client(string id, string name, Knktr konn, Terminal term)
        {
            this.ID = id;
            this.Name = name;
            this.Konnektor = konn;
            this.Term = term;
            Date = DateTime.Now;
            
        }
        public Client(string id, string name, DateTime date, string reason)
        {
            this.ID = id;
            this.Name = name;
            this.Date = date;
            this.ReasonForCall = reason;
        }
        public Client(string id)
        {
            this.ID = id;
        }
        public Client(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
