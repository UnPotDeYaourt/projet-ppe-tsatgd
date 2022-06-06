using System;
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

namespace TSATGD.Pages
{
    /// <summary>
    /// Logique d'interaction pour TeamDay.xaml
    /// </summary>
    public partial class TeamDay : Page
    {
        private MainWindow mainWindow;
        private Journee day;
        private Rencontre rencontre;
        public List<Joueur> availablePlayer;
        public List<Joueur> players;
        public List<Joueur> equipe;
        public TeamDay(MainWindow _mainWindow, Rencontre _rencontre, Journee _day)
        {
            InitializeComponent();
            this.mainWindow = _mainWindow;
            this.day = _day;
            this.rencontre = _rencontre;
            lbl_date.Content = day.DateJournee.ToString("dddd dd MMMM");
            this.availablePlayer = new List<Joueur>();

            Console.WriteLine("ID Equipe:" + this.rencontre.IdRencontre);

            foreach(KeyValuePair<Joueur, bool> playerAndDispo in this.day.isHere)
            {
                //Console.WriteLine(playerAndDispo.Key.NomJoueur);
                if (playerAndDispo.Value == true)
                {
                    this.availablePlayer.Add(playerAndDispo.Key);
                    
                }
            }

            this.players = availablePlayer;
            this.equipe = new List<Joueur>();
            Rencontre ren = this.day.Recontres.First(x => x.IdRencontre == Convert.ToInt32(this.rencontre.IdRencontre));
            foreach(Joueur j in ren.Joueurs)
            {
                Console.WriteLine(j.NomJoueur);
                players.Remove(j);
                equipe.Add(j);
            }

            dg_playersList.Items.Clear();
            dg_playersList.ItemsSource = this.availablePlayer;

            dg_equipe.Items.Clear();
            dg_equipe.ItemsSource = this.equipe;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e) // Quand le bouton "Enregistrer" est cliqué
        {

        }

        private void btn_back_Click(object sender, RoutedEventArgs e) // Quand le bouton "Retour" est cliqué
        {
            mainWindow.Content = new MeetingsList(day, mainWindow);
        }

        private void btn_equipe_Click(object sender, RoutedEventArgs e)
        {
            Joueur selectedPlayer = (Joueur)dg_playersList.SelectedItem;
            this.players.Remove(selectedPlayer);
            this.equipe.Add(selectedPlayer);
            refreshDataGrid();
            
        }

        private void btn_list(object sender, RoutedEventArgs e)
        {
            Joueur selectedPlayer = (Joueur)dg_equipe.SelectedItem;
            this.equipe.Remove(selectedPlayer);
            this.players.Add(selectedPlayer);
            refreshDataGrid();
        }

        private void refreshDataGrid()
        {
            dg_playersList.ItemsSource = null;
            dg_playersList.ItemsSource = this.players;

            dg_equipe.ItemsSource = null;
            dg_equipe.ItemsSource = this.equipe;
        }
    }
}
