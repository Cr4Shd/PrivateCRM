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
    /// Diese Seite ist als POPUP gedacht wenn man details zu einem Anruf angezeigt bekommen möchte. Zusätzlich dazu soll man später noch Anrufinfos bekommen können.
    /// TODO : Eine Möglichkeit finden die Anruf ID aus der Datenbank zu bekommen. Momentan gibt er mir nur nullIDS zurück obwohl innerhalb der Datenbank eine ID vorhanden ist.
    /// TODO : Innerhalb der SearchClient das MessageFenster wieder herausnehmen. 
    /// </summary>
    
    public sealed partial class ClientCallDetails : Page
    {
        public ClientCallDetails()
        {
            this.InitializeComponent();
        }
        
        /// <summary>
        /// Diese Funktion wird aufgerufen wenn auf die Seite navigiert wird.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = e.Parameter as DBItems.Client;
            var name = parameter.Name;
            var id = parameter.ID;

            Client_ID.Text = id;
            Client_Name.Text = name;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchClient));
        }
    }
}
