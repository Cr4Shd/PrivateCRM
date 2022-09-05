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
using MySql.Data.MySqlClient;
using Syntax_Imotion_Lexika.DBUtils;
using System.Diagnostics;
using Windows.System;
// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Syntax_Imotion_Lexika
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class DatabaseTools : Page
    {
        public DatabaseTools()
        {
            this.InitializeComponent();
        }

        private void Check_Database_Click(object sender, RoutedEventArgs e)
        {
            var testBool = DBControler.CheckDataBase();
            if (testBool)
            {
                Connection_State.Text = "Datenbank ist vorhanden!";
            }
            else
            {
                Connection_State.Text = "Datenbank ist nicht vorhanden!";
            }
        }

        private void Create_Database_Click(object sender, RoutedEventArgs e)
        {
            var resultBool = DBControler.CreateDataBase();
            if (resultBool)
            {
                Connection_State.Text = "Datenbank wurde erstellt!";
            }
            else
            {
                Connection_State.Text = "Datenbank konnte nicht erstellt werden!";
            }
        }


        // Wird nicht verwendet.
        //private void Check_SQL_Service_Click(object sender, RoutedEventArgs e)
        //{

        //    Process[] process = Process.GetProcessesByName("mysqld");
        //    if (process.Length == 0)
        //    {
        //        Connection_State.Text = "Es wurde kein MySQL Dienst gefunden!";
        //    }
        //    else
        //    {
        //        Connection_State.Text = "Es wurde ein MySQL Dienst gefunden!";
        //    }
        //}
    }
}
