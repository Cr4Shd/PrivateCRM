using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax_Imotion_Lexika.DBItems
{
    public class ProblemCase
    {
        public string Description{ get; set; }
        public string Solution { get; set; }
        public string Author { get; set; }

        public ProblemCase(string descr, string solu, string author)
        {
            this.Description = descr;
            this.Solution = solu;
            this.Author = author;
        }
    }
}
