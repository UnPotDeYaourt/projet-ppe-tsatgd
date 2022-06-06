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
    /// Logique d'interaction pour CreateTeam.xaml
    /// </summary>
    public partial class CreateTeam : Page
    {
        private MainWindow mainWindow;

        public CreateTeam(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e) // Quand le bouton "Retour" est cliqué
        {
            mainWindow.Content = new TeamsList(mainWindow);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e) // Quand le bouton "Enregister" est cliqué
        {

        }
    }
}
