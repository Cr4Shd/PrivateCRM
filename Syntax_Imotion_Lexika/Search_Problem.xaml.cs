using Syntax_Imotion_Lexika.DBUtils;
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
    public sealed partial class Search_Problem : Page
    {
        public Search_Problem()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var problem = Problem_TxtBx.Text;
            var resultP = DBPasstrough.ReadProblemsDB(problem);

            if (resultP.IdWord.Equals("PROBLEM_NOT_FOUND"))
            {
                Result_Block.Text = "Kein Problem gefunden!";
            }
            else
            {
                GenerateObservable(problem);
            }

        }

        private void GenerateObservable(string keyword)
        {
            List<DBItems.ProblemCase> list = new List<DBItems.ProblemCase>();
            list = DBPasstrough.ReadDBProblemList(keyword);
            ProblemListView.ItemsSource = list;
        }
        private void Back_To_Main_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void ProblemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as DBItems.ProblemCase;
            Frame.Navigate(typeof(Problem_Details), item);
        }
    }
}
