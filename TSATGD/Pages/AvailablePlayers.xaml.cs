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
    /// Logique d'interaction pour AvailablePlayers.xaml
    /// </summary>
    public partial class AvailablePlayers : Page
    {
        private MainWindow mainWindow;
        private Journee day;
        private List<JoueurAndDispo> players;
        public AvailablePlayers(MainWindow _mainWindow, Journee _day)
        {
            InitializeComponent();
            this.mainWindow = _mainWindow;
            this.day = _day;
            lbl_date.Content = day.DateJournee.ToString("dddd dd MMMM");

            players = new List<JoueurAndDispo>();

            foreach(Joueur j in MainWindow.joueurs)
            {
                bool responded = false;
                bool isDispo = false;

                foreach (var keyValue in this.day.isHere)
                {
                    if (keyValue.Key.IdJoueur == j.IdJoueur)
                    {
                        responded = true;
                        isDispo = keyValue.Value;
                        break;
                    }
                }

                players.Add(new JoueurAndDispo(j.IdJoueur, j.NomJoueur, j.PrenomJoueur, j.ClassementJoueur.LibelleClassement, j.IsPaye, isDispo, responded));
            }

            dg_players.Items.Clear();
            dg_players.ItemsSource = this.players;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e) // Quand le bouton "Retour" est cliqué
        {
            mainWindow.Content = new DaysList(mainWindow);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e) // Quand le bouton "Enregistrer" est cliqué
        {
            // OUI JE SAIS C'EST PAS DUTOUT OPTI MAIS ÇA FONCTIONNE DONC BALEC
            foreach (Journee j in MainWindow.journees)
            {
                if (j.IdJournee == this.day.IdJournee)
                {

                    // On enregistre les changement de repondu
                    foreach(JoueurAndDispo joueurAndDispo in this.players)
                    {
                        bool isExist = false;
                        Joueur joueur = MainWindow.joueurs.First(x => x.IdJoueur == Convert.ToInt32(joueurAndDispo.Id));

                        foreach (KeyValuePair<Joueur, bool> keyValue in this.day.isHere)
                        {
                            if (keyValue.Key.IdJoueur == joueurAndDispo.Id)
                            {
                                isExist = true;
                            }
                        }

                        if (joueurAndDispo.Responded)
                        {
                            
                            if(!isExist)
                            {
                                foreach(Journee journee in MainWindow.journees)
                                {
                                    if(journee.IdJournee == this.day.IdJournee)
                                    {  
                                        journee.isHere.Add(joueur, true);
                                    }
                                }
                                AdoJournee.addPlayerDisponibilityForOneDay(this.day.IdJournee, joueurAndDispo.Id, joueurAndDispo.Responded);
                            }
                        } else if(joueurAndDispo.Responded == false && isExist == true)
                        {
                            foreach(Journee journee in MainWindow.journees)
                            {
                                if(journee.IdJournee == this.day.IdJournee)
                                {
                                    journee.isHere.Remove(joueur);
                                }
                            }
                            AdoJournee.deletePlayerDisponibilityForOneDay(this.day.IdJournee, joueurAndDispo.Id);
                        }
                    }

                    // On save les changements de isDispo
                    foreach(KeyValuePair<Joueur, bool> joueurDispo in j.isHere.ToList())
                    {
                        KeyValuePair<Joueur, bool> joueurDispoTemp = joueurDispo;
                        JoueurAndDispo joueurAndDispo = this.players.First(x => x.Id == Convert.ToInt32(joueurDispo.Key.IdJoueur));

                        if(joueurDispo.Value != joueurAndDispo.IsDispo)
                        {
                            Journee journee = MainWindow.journees.First(x => x.IdJournee == Convert.ToInt32(this.day.IdJournee));
                            journee.isHere[joueurDispoTemp.Key] = joueurAndDispo.IsDispo;
                            AdoJournee.updatePlayerIsHereForOneDay(this.day.IdJournee, joueurDispo.Key.IdJoueur, joueurAndDispo.IsDispo);
                        }
                    }
                    break;
                }
            }
        }

        private void chx_isDispo_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chx = (CheckBox)sender;
            var id = ((CheckBox)sender).Tag;
            bool value = false;
            
            if(chx.IsChecked == true)
            {
                value = true;
            } else
            {
                value = false;
            }

            JoueurAndDispo temp = this.players.First(x => x.Id == Convert.ToInt32(id));

            temp.IsDispo = value;

        }

        private void chx_isResponded_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chx = (CheckBox)sender;
            var id = ((CheckBox)sender).Tag;
            bool value = false;

            if (chx.IsChecked == true)
            {
                value = true;
            }
            else
            {
                value = false;
            }

            JoueurAndDispo temp = this.players.First(x => x.Id == Convert.ToInt32(id));

            temp.Responded = value;

        }
    }

    class JoueurAndDispo
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Classement { get; set; }
        public bool IsPaye { get; set; }
        public bool IsDispo { get; set; }
        public bool Responded { get; set; }

        public JoueurAndDispo(int _id, string _nom, string _prenom, string _classement, bool _isPaye, bool _isDispo, bool _responded)
        {
            this.Id = _id;
            this.Nom = _nom;
            this.Prenom = _prenom;
            this.Classement = _classement;
            this.IsPaye = _isPaye;
            this.IsDispo = _isDispo;
            this.Responded = _responded;
        }
    }
}
