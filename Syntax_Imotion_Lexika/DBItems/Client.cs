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
        public int Case_Closed { get; set; }
        public Guid Unq_Call_ID { get; set; }

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
        /// <summary>
        /// Das ist der StandartKonstruktor für die erste Erstellung eines Clients
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="reasonForCall"></param>
        public Client(string id, string name, string reasonForCall)
        {
            this.ID = id;
            this.Name = name.ToLower();
            Date = DateTime.Now;
            ReasonForCall = reasonForCall;
            Case_Closed = 1;
            Unq_Call_ID = Guid.NewGuid();
        }
        /// <summary>
        /// Das ist ein ungenutzter Ctor für die Erstellung mit Terminal, Konnektor und anderen Daten
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="konn"></param>
        /// <param name="term"></param>
        public Client(string id, string name, Knktr konn, Terminal term)
        {
            this.ID = id;
            this.Name = name;
            this.Konnektor = konn;
            this.Term = term;
            Date = DateTime.Now;
            Case_Closed = 0;
        }
        /// <summary>
        /// Das ist der Ctor für die Suche in der Datenbank
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <param name="reason"></param>
        public Client(string id, string name, DateTime date, string reason)
        {
            this.ID = id;
            this.Name = name;
            this.Date = date;
            this.ReasonForCall = reason;
            Case_Closed = 0;
        }
        public Client(string id)
        {
            this.ID = id;
            Case_Closed = 0;
        }
        public Client(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            Case_Closed = 0;
        }
        public Client(string id, string name, DateTime date, string reason, string callID, int caseSolved)
        {
            this.ID = id;
            this.Name = name;
            this.Date = date;
            this.ReasonForCall = reason;
            this.Unq_Call_ID = Guid.Parse(callID);
            this.Case_Closed = caseSolved;
        }
        /// <summary>
        /// Testkonstruktor für das neue Feature um in der neuen PopUppage, welche aufgerufen wird wenn man ein item innerhalb der listview klickt einen anruf selektieren zu können.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <param name="reason"></param>
        /// <param name="uId"></param>
        public Client(string id, string name, DateTime date, string reason, string uId)
        {
            this.ID = id;
            this.Name = name;
            this.Date = date;
            this.ReasonForCall = reason;
            this.Unq_Call_ID = Guid.Parse(uId);
            Case_Closed = 0;
        }
    }
}
