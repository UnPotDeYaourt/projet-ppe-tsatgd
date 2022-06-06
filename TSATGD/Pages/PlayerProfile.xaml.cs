using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using TSATGD.Classes;
using TSATGD.ADO;
using System.Windows.Forms;

namespace TSATGD.Pages
{
    /// <summary>
    /// Logique d'interaction pour PlayerProfile.xaml
    /// </summary>
    public partial class PlayerProfile : Page
    {
        private MainWindow mainWindow;
        string[] pathsplit;
        string pathLogiciel;
        string pathCertif;
        string pathLicence;
        Joueur j;
        public PlayerProfile(MainWindow _mainWindow, Joueur jou)
        {
            InitializeComponent();
            this.mainWindow = _mainWindow;
            lbl_name.Content = jou.NomJoueur + "\n" + jou.PrenomJoueur;
            string nomJ = jou.NomJoueur;
            string prenomJ = jou.PrenomJoueur;
            string initiales = "";
            initiales = initiales + nomJ[0] + prenomJ[0];
            lbl_initial.Content = initiales;
            if (jou.IsSeniorPlus == true)
            {
                lbl_senior.Content = "Senior +";
            }
            else
            {
                lbl_senior.Content = "Senior";
            }

            if (jou.IsPaye == true)
            {
                im_paye.Visibility = Visibility.Visible;
                im_paspaye.Visibility = Visibility.Hidden;
            }
            else if (jou.IsPaye == false)
            {
                im_paspaye.Visibility = Visibility.Visible;
                im_paye.Visibility = Visibility.Hidden;
            }
            lbl_mail.Content= jou.MailJoueur;
            lbl_birthdate.Content = jou.AnneeNaissance;
            List<Joueur> joueur;
            joueur = AdoJoueur.getAll();
            dg_joueurs.Items.Clear();
            dg_joueurs.ItemsSource = joueur;


            //lbl_initial.DataContext;
            this.j = jou;
            pathLogiciel = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            pathsplit = pathLogiciel.Split(new string[] { "PROJET-TSATGD" }, StringSplitOptions.None);           
        }

        private void btn_license_Click(object sender, RoutedEventArgs e) // Quand le bouton "Afficher la license" est cliqué
        {
            string pathLicense = MainWindow.pathsplit[0] + @"\PROJET-TSATGD" + j.Licence;
            pathLicense = pathLicense.Replace(@"\\\\", @"\\");
            System.Diagnostics.Process.Start("file:///" + pathLicense);
        }

        private void btn_certificat_Click(object sender, RoutedEventArgs e) // Quand le bouton "Afficher le certificat médical" est cliqué
        {
            string pathCertif = MainWindow.pathsplit[0] + @"\PROJET-TSATGD" + j.Certification;
            pathCertif = pathCertif.Replace(@"\\\\", @"\\");
            System.Diagnostics.Process.Start("file:///" + pathCertif);
        }

        private void btn_back_Click(object sender, RoutedEventArgs e) // Quand le bouton "Retour" est cliqué
        {
            mainWindow.Content = new PlayersList(mainWindow);
        }

        private void btn_license_add_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            FileInfo fi = new FileInfo(pathLogiciel + @"\Azure.Core.dll"); //On lui met une valeur inutile ...
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = System.IO.Path.GetFullPath(openFileDialog.FileName);
                fi = new FileInfo(path);
                fi.CopyTo(pathLogiciel + @"\Licences\" + "licence_" + j.NomJoueur+ "_" +j.PrenomJoueur , true);
            }
            j.Licence = pathsplit[1] + @"\Licences\" + "licence_" + j.NomJoueur + "_" + j.PrenomJoueur; //Pour pouvoir l'ajouter à une instance...
            AdoJoueur.ajouterLicence(j);
        }

        private void btn_certificat_add_Click(object sender, RoutedEventArgs e)
        {
            pathsplit = pathLogiciel.Split(new string[] { "PROJET-TSATGD" }, StringSplitOptions.None);
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            FileInfo fi = new FileInfo(pathLogiciel + @"\Azure.Core.dll"); //On lui met une valeur inutile car la valeur est assginé dans un if donc il a besoin d'une valeur par défaut
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = System.IO.Path.GetFullPath(openFileDialog.FileName);
                fi = new FileInfo(path);
                fi.CopyTo(pathLogiciel + @"\CertifMedicales\"+ "certificat_medicale_" + j.NomJoueur + "_" + j.PrenomJoueur, true);
            }
            j.Certification = pathsplit[1] + @"\CertifMedicales\" + "certificat_medicale_" + j.NomJoueur + "_" + j.PrenomJoueur;  //Pour pouvoir l'ajouter à une instance de la classe joueur pour ensuite envoyer en base
            AdoJoueur.ajouterCertif(j);
        }
    }
}
