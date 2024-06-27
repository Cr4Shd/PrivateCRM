using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Syntax_Imotion_Lexika.DBUtils
{
    
    class DBObjectParser
    {
        public object Parameter { get; set; }
        public string RString { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        DateTime RDate { get; set; }

        public DBObjectParser(object t)
        {
            Parameter = t;
            GetTypeOfValue(t);
        }
        public DBObjectParser()
        {

        }
        public Tuple<int,object> GetTypeOfValue(object t)
        {
            if (t is System.String)
            {
                return CheckValueString(t as string);
            }
            //if (t is System.DateTime)
            //{
            //    return (DateTime)t;
            //}
            return null;
        }
        // Ich brauche nun noch einen Indikator für die anrufende Methode ob es sich um eine ID oder einen Namen handelt.
        // Die Aufr. Methode muss auswerten 
        public Tuple<int, object> CheckValueString(string value)
        {
            if (value.StartsWith("3") || value.StartsWith("2"))
            {
                return Tuple.Create(1, value as object);
            }
            else
            {
                return Tuple.Create(0, value as object);
            }
        }
        //public Tuple<int, DateTime> CheckDateValue
        //{
        //    return null;
        //}
    }
}
