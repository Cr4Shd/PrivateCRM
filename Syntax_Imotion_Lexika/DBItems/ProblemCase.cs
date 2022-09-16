using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax_Imotion_Lexika.DBItems
{
    public class ProblemCase
    {
        public string IdWord { get; set; }
        public string Description{ get; set; }
        public string Solution { get; set; }
        public string Author { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// Das ist der normale Konstruktor
        /// </summary>
        /// <param name="idword"></param>
        /// <param name="descr"></param>
        /// <param name="solu"></param>
        /// <param name="author"></param>
        public ProblemCase(string idword, string descr, string solu, string author)
        {
            this.IdWord = idword;
            this.Description = descr;
            this.Solution = solu;
            this.Author = author;
        }
        /// <summary>
        /// Das ist der Konstruktor für ein Problem mit Fehlercode
        /// </summary>
        /// <param name="idword"></param>
        /// <param name="descr"></param>
        /// <param name="solu"></param>
        /// <param name="author"></param>
        /// <param name="code"></param>
        public ProblemCase(string idword, string descr, string solu, string author, string code)
        {
            this.IdWord= idword;
            this.Description= descr;
            this.Solution = solu;
            this.Author = author;
            this.Code = code;
        }
        /// <summary>
        /// Das ist der Konstruktor für die wiedergabe eines Fehlercodes bei der Suche
        /// </summary>
        /// <param name="idword"></param>
        public ProblemCase(string idword)
        {
            IdWord = idword;
        }
    }
}
