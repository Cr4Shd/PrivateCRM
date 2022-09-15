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
using System.Runtime.InteropServices;

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
            DisableSafeButton();
            
            
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            var prob_title_text = Problem_Title.Text;
            var solution_description_text = Problem_Description.Text;
            var prob_autor = Problem_Autor.Text;
            var keyword = CheckRdButtn();
            var code = code_bx.Text;

            if (code.Equals(String.Empty))
            {
                ProblemCase prob_case = new ProblemCase(keyword, prob_title_text, solution_description_text, prob_autor);
                var result = DBUtils.DBPasstrough.WriteProblemDB(prob_case);

                if (result)
                {
                    Result_Block.Text = "Problem wurde erfolgreich gespeichert!";
                    ClearBoxes();
                }
                else
                {
                    Result_Block.Text = "Problem wurde nicht gespeichert!";
                }
            }
            else
            {
                ProblemCase prob_case = new ProblemCase(keyword, prob_title_text, solution_description_text, prob_autor, code);
                var result = DBUtils.DBPasstrough.WriteProblemDB(prob_case);

                if (result)
                {
                    Result_Block.Text = "Problem wurde erfolgreich gespeichert!";
                    ClearBoxes();
                }
                else
                {
                    Result_Block.Text = "Problem wurde nicht gespeichert!";
                }
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
            knktr_rd_btn.IsChecked = false;
            term_rd_btn.IsChecked = false;
            Btn_Save.IsEnabled = false;
            code_bx.Text = String.Empty;
        }
        private string CheckRdButtn()
        {
            if (term_rd_btn.IsChecked == true)
            {
                return "kartenterminal";
            }
            if (knktr_rd_btn.IsChecked == true)
            {
                return "konnektor";
            }
            if (uncat_rd_btn.IsEnabled == true)
            {
                return "nocat";
            }
            return "NoDev";
        }

        private void knktr_rd_btn_Checked(object sender, RoutedEventArgs e)
        {
            EnableSafeButton();
        }

        private void term_rd_btn_Checked(object sender, RoutedEventArgs e)
        {
            EnableSafeButton();
        }
        private void EnableSafeButton()
        {
            Btn_Save.IsEnabled = true;
        }
        private void DisableSafeButton()
        {
            Btn_Save.IsEnabled = false;
        }

        private void uncat_rd_btn_Checked(object sender, RoutedEventArgs e)
        {
            EnableSafeButton();
        }
    }
}
