using System;
using TSATGD.Classes;
using TSATGD.ADO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TSATGD.Pages
{
    /// <summary>
    /// Logique d'interaction pour CreateMeeting.xaml
    /// </summary>
    public partial class CreateMeeting : Page
    {
        private MainWindow mainWindow;
        private Journee journee;
        private Rencontre rencontre;


        public CreateMeeting(MainWindow _mainWindow, Journee _journee)
        {
            InitializeComponent();

            this.journee = _journee;

            mainWindow = _mainWindow;
        }
        
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new MeetingsList(journee, this.mainWindow);
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_adversaire.Text == "" || tbx_adversaire.Text == " ")
            {
                tbx_adversaire.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (tbx_lieu.Text == "" || tbx_lieu.Text == " ")
            {
                tbx_lieu.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                rencontre = new Rencontre(tbx_adversaire.Text, tbx_lieu.Text);
                tbx_adversaire.BorderBrush = new SolidColorBrush(Colors.LightGray); tbx_adversaire.Text = string.Empty;
                tbx_lieu.BorderBrush = new SolidColorBrush(Colors.LightGray); tbx_lieu.Text = string.Empty;
            }
        }
    }
}
