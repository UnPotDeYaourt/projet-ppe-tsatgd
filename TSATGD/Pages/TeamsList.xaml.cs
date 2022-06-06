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
    /// Logique d'interaction pour TeamsList.xaml
    /// </summary>
    public partial class TeamsList : Page
    {
        private MainWindow mainWindow;

        public TeamsList(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            List<Equipe> equipe;
            equipe = AdoEquipe.getAll();
            dg_equipe.ItemsSource = equipe;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new CreateTeam(mainWindow);
        }

        private void btn_view_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
