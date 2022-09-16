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
    public sealed partial class Problem_Details : Page
    {
        public Problem_Details()
        {
            this.InitializeComponent();
        }
        // Was hier passiert : Innerhalb der Search_Problem page ist eine Listview, dessen ItemClick aktiviert ist. Das Event des Klicks navigiert auf die Extra Seite, obwohl ich diese vielleicht garnicht brauche.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = e.Parameter as DBItems.ProblemCase;
            Solution_Box.Text = parameter.Solution;
            Title_Box.Text = parameter.Description;
        }
        

        private void MainMenu_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Back_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search_Problem));
        }
    }
}
