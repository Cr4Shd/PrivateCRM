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
using Syntax_Imotion_Lexika.DBItems;
using System.Threading;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Syntax_Imotion_Lexika
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class InsertProblem : Page
    {
        public InsertProblem()
        {
            
            this.InitializeComponent();
            
            
            
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            var prob_title_text = Problem_Title.Text;
            var solution_description_text = Problem_Description.Text;
            var prob_autor = Problem_Autor.Text;
            
            ProblemCase prob_case = new ProblemCase(prob_title_text, solution_description_text, prob_autor);
            var result = DBUtils.DBPasstrough.WriteProblemDB(prob_case);

            if (result)
            {
                Result_Block.Text = "Kunde wurde erfolgreich gespeichert!";
                ClearBoxes();
            }
            else
            {
                Result_Block.Text = "Kunde wurde nicht gespeichert!";
            }
        }

        private void Return_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Syntax_Imotion_Lexika.MainPage));
        }
        private void ClearBoxes()
        {
            Problem_Title.Text = String.Empty;
            Problem_Description.Text = String.Empty;
            Problem_Autor.Text = String.Empty;
        }
    }
}
