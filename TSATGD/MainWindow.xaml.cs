using System;
using System.Collections.Generic;
using TSATGD.Pages;
using TSATGD.Classes;
using TSATGD.ADO;
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
using MahApps.Metro.Controls;

namespace TSATGD
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static List<Classement> classements;
        public static List<TypeJoueur> typeJoueurs;
        public static List<Equipe> equipes;
        public static List<Joueur> joueurs;
        public static List<Journee> journeesLite;
        public static List<Journee> journees;
        public static List<Rencontre> rencontres;
        public static string[] pathsplit;
        public static string pathLogiciel;


        public MainWindow()
        {
            InitializeComponent();
            
            pathLogiciel = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            pathsplit = pathLogiciel.Split(new string[] {"\\PROJET-TSATGD" }, StringSplitOptions.None);

            classements = AdoClassement.getAll();

            typeJoueurs = AdoType.getAll();

            /*
            typeJoueurs.Add(new TypeJoueur(1, "Attaquant", "A"));
            typeJoueurs.Add(new TypeJoueur(2, "Defenseur", "D"));
            typeJoueurs.Add(new TypeJoueur(3, "Contre Attaquant", "CA"));
            typeJoueurs.Add(new TypeJoueur(4, "Volleyeur", "V"));
            */
            equipes = AdoEquipe.getAll();
            joueurs = AdoJoueur.getAll();

            this.Content = new PlayersList(this);
            
            journeesLite = AdoJournee.getAllLite();
            journees = AdoJournee.getAll();


            foreach (Journee j in journees)
            {
                j.isHere = AdoJournee.getPlayersIsHereForOneDay(j.IdJournee);
            }

            Console.WriteLine(journees[0].isHere.Count);

           
        }

        private void PlayersListButton(object sender, RoutedEventArgs e)
        {
            this.Content = new PlayersList(this);
        }

        private void DaysListButton(object sender, RoutedEventArgs e)
        {
            this.Content = new DaysList(this);
        }

        private void TeamsListButton(object sender, RoutedEventArgs e)
        {
            this.Content = new TeamsList(this);
        }

        private void CreditsButton(object sender, RoutedEventArgs e)
        {
            this.Content = new Credits();
        }
    }
}
