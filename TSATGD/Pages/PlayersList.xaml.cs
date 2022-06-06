using System;
using TSATGD.Pages;
using TSATGD.ADO;
using TSATGD.Classes;
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

namespace TSATGD
{
    /// <summary>
    /// Interaction logic for PlayersList.xaml
    /// </summary>
    public partial class PlayersList : Page
    {
        private MainWindow mainWindow;

        public PlayersList(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            List<Joueur> joueur;
           joueur = AdoJoueur.getAll();
            dg_joueurs.Items.Clear();
           dg_joueurs.ItemsSource = joueur;

            tbx_attaquant.Text = Convert.ToString(MainWindow.typeJoueurs.Where(x => x.IdType == 1).First().PlayerList.Count);
        }

        private void btn_add_Click(object sender, RoutedEventArgs e) // Action lorsque le bouton "Ajouter" est cliquer
        {
            mainWindow.Content = new CreatePlayer(mainWindow);
        }

        private void btn_import_Click(object sender, RoutedEventArgs e) // Action lorsque le bouton "Importer des membres (Excel)" est cliquer
        {
            mainWindow.Content = new ImportFile(mainWindow);
        }

        private void btn_view_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            Joueur jou = MainWindow.joueurs.First(x => x.IdJoueur == Convert.ToInt32(id));
            mainWindow.Content = new PlayerProfile(mainWindow, jou);
        }
    }
}
