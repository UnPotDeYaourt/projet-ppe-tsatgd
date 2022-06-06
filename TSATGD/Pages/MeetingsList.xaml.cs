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
    /// Logique d'interaction pour MeetingsList.xaml
    /// </summary>
    public partial class MeetingsList : Page
    {
        private MainWindow mainWindow;
        Journee journee;
        List<Rencontre> rencontres;
        public MeetingsList(Journee _journee, MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            this.journee = _journee;
            this.mainWindow = _mainWindow;
            lbl_date.Content = journee.DateJournee.ToString("dddd dd MMMM");
            this.rencontres = new List<Rencontre>();
            foreach (Rencontre r in MainWindow.rencontres)
            {
                if(r.Journee.IdJournee == this.journee.IdJournee)
                {
                    this.rencontres.Add(r);
                }
            }
            dg_rencontre.Items.Clear();
            dg_rencontre.ItemsSource = this.rencontres;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new CreateMeeting(this.mainWindow, journee);
        }

        private void btn_view_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            Rencontre r = this.rencontres.First(x => x.IdRencontre == Convert.ToInt32(id));
            Journee j = MainWindow.journees.First(x => x.IdJournee == Convert.ToInt32(this.journee.IdJournee));
            mainWindow.Content = new TeamDay(this.mainWindow, r, j);
        }
    }
}
