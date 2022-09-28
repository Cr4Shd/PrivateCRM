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
using Syntax_Imotion_Lexika.DBUtils;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Syntax_Imotion_Lexika
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class DB_ConfigParameters : Page
    {
        public string Server { get; set; }
        public string DataBase { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public int ConnTimeOut { get; set; }
        public int Port { get; set; }
        public bool Pooling { get; set; }
        public DB_ConfigParameters()
        {
            this.InitializeComponent();
            this.GetDBParameters();
        }
        private void GetDBParameters()
        {
            Server = DBConfigs.stringBuilder.Server;
            DataBase = DBConfigs.stringBuilder.Database;
            UserID = DBConfigs.stringBuilder.UserID;
            Password = DBConfigs.stringBuilder.Password;
            ConnTimeOut = 10;
            Port = 3306;
            Pooling = false;
        }

        private void Btn_GetParam_Click(object sender, RoutedEventArgs e)
        {
            TxBx_Srv.Text = Server;
            TxBx_DB.Text = DataBase;
            TxBx_UID.Text = UserID;
            TxBx_PW.Text = Password;
            TxBx_ConTO.Text = ConnTimeOut.ToString();
            TxBX_Port.Text = Port.ToString();

        }
    }
}
