using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
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
using TSATGD.Classes;
using TSATGD.ADO;
using System.Windows.Forms;

namespace TSATGD.Pages
{
    /// <summary>
    /// Logique d'interaction pour CreatePlayer.xaml
    /// </summary>
    public partial class CreatePlayer : Page
    {
        private MainWindow mainWindow;
        Joueur joueur = new Joueur();
        string pathLicence ="";
        string pathCertif="";
        string pathLogiciel = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        string[] pathsplit;
        public CreatePlayer(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            List<Classement> c = new List<Classement>();
            c = AdoClassement.getAll();
            cb_classement.ItemsSource = c;
            cb_type.ItemsSource = MainWindow.typeJoueurs;
        }

        bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
            
        }

        private void btn_add_certificat_Click(object sender, RoutedEventArgs e) //Quand le bouton "Ajouter le certificat médical" est cliqué
        {
            pathsplit = pathLogiciel.Split(new string[] {"PROJET-TSATGD"}, StringSplitOptions.None);
            string path = "";
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            FileInfo fi = new FileInfo(pathLogiciel + @"\Azure.Core.dll"); //On lui met une valeur inutile car la valeur est assginé dans un if donc il a besoin d'une valeur par défaut
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = System.IO.Path.GetFullPath(openFileDialog.FileName);
                fi = new FileInfo(path);
                fi.CopyTo(pathLogiciel+ @"\CertifMedicales\" + fi.Name, true);
            }
            joueur.Certification = pathsplit[1]+ @"\CertifMedicales\" + fi.Name;  //Pour pouvoir l'ajouter à une instance de la classe joueur pour ensuite envoyer en base
        }

        private void btn_add_license_Click(object sender, RoutedEventArgs e) //Quand le bouton "Ajouter la license" est cliqué 
        {
            string path = "";
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            FileInfo fi = new FileInfo(pathLogiciel + @"\Azure.Core.dll"); //On lui met une valeur inutile ...
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = System.IO.Path.GetFullPath(openFileDialog.FileName);
                fi = new FileInfo(path);
                fi.CopyTo(pathLogiciel + @"\Licences\" + fi.Name, true);
            }
            joueur.Licence = pathsplit[1] + @"\Licences\" + fi.Name; //Pour pouvoir l'ajouter à une instance...
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e) //Quand le bouton "Retour" est cliqué
        {
            mainWindow.Content = new PlayersList(mainWindow);
        }

        private void btn_creer_Click(object sender, RoutedEventArgs e) //Quand le bouton "Créer" est cliqué
        {
            if (tbx_nom.Text == "" || tbx_nom.Text == " ")
            {
                tbx_nom.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if(tbx_prenom.Text == "" || tbx_prenom.Text == " ")
            {
                tbx_prenom.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (tbx_mail.Text == "" || tbx_mail.Text == " " || IsValidEmail(tbx_mail.Text) == false)
            {
                tbx_mail.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (tbx_naissance.Text == "" || tbx_naissance.Text == " ")
            {
                tbx_naissance.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                //TypeJoueur type = MainWindow.typeJoueurs.Where(x => x.IdType == 1).First();
                if (tgl_seniorplus.IsOn == true)
                {
                    //                  NomJoueur     PrenomJoueur               AnneeNaissance               Licence   CertifiMed, IsPaye Senior+        ClassementJoueur                MailJoueur
                    joueur = new Joueur(tbx_nom.Text, tbx_prenom.Text, Convert.ToInt32(tbx_naissance.Text), pathLicence, pathCertif, false, true,(Classement)cb_classement.SelectedItem, tbx_mail.Text, (TypeJoueur)cb_type.SelectedItem);
                    TypeJoueur t = (TypeJoueur)cb_type.SelectedItem;
                    t.AddJoueur(joueur);
                }
                else
                {
                    joueur = new Joueur(tbx_nom.Text, tbx_prenom.Text, Convert.ToInt32(tbx_naissance.Text), pathLicence, pathCertif, false, false,(Classement)cb_classement.SelectedItem, tbx_mail.Text, (TypeJoueur)cb_type.SelectedItem);
                    TypeJoueur t = (TypeJoueur)cb_type.SelectedItem;
                    t.AddJoueur(joueur);
                }
                AdoJoueur.create(joueur);
                Joueur j = AdoJoueur.getDernierJoueur();
                MainWindow.joueurs.Add(j);
                tbx_nom.BorderBrush = new SolidColorBrush(Colors.LightGray);tbx_nom.Text = string.Empty;
                tbx_prenom.BorderBrush = new SolidColorBrush(Colors.LightGray);tbx_prenom.Text = string.Empty;
                tbx_mail.BorderBrush = new SolidColorBrush(Colors.LightGray);tbx_mail.Text = string.Empty;
                tbx_naissance.BorderBrush = new SolidColorBrush(Colors.LightGray);tbx_naissance.Text = string.Empty;
                cb_classement.SelectedItem = null;
            }
        }

        private void tbx_naissance_LostFocus(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            if (date.Year - Convert.ToInt32(tbx_naissance.Text) >= 35)
            {
                tgl_seniorplus.IsOn = true;
            }
            else
            {
                tgl_seniorplus.IsOn = false;
            }
        }
    }
}
