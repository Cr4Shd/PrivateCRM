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

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace Syntax_Imotion_Lexika
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Create_Problem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InsertProblem));
        }

        private void Create_Client_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClientContact));
        }

        private void Search_Client_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchClient));
        }

        private void Database_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DatabaseTools));
        }
        private void Test_Event(object sedner, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search_Problem));
        }
    }
}
