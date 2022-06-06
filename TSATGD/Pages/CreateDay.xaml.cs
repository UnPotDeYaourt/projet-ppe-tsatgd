using System;
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

namespace TSATGD.Pages
{
    /// <summary>
    /// Logique d'interaction pour CreateDay.xaml
    /// </summary>
    public partial class CreateDay : Page
    {
        private MainWindow mainWindow;

        public CreateDay(MainWindow _mainWindow)
        {
            InitializeComponent();
            this.mainWindow = _mainWindow;
            tgl_button_senior.IsChecked = true;
        }

        private void tgl_button_senior_Checked(object sender, RoutedEventArgs e) // Quand le bouton "Senior" est cliqué
        {
            tgl_button_senior_plus.IsChecked = false;
        }

        private void tgl_button_senior_plus_Checked(object sender, RoutedEventArgs e) // Quand le bouton "Senior +" est cliqué
        {
            tgl_button_senior.IsChecked = false;
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) // Quand la data est changée
        {
            lbl_date.Content = calendar.SelectedDate.Value.ToString("dd MMMM yyyy");
        }

        private void btn_back_Click(object sender, RoutedEventArgs e) // Quand le bouton "Retour" est cliqué
        {
            mainWindow.Content = new DaysList(mainWindow);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e) // Quand le bouton "Enregistrer" est cliqué
        {
            bool isPlus;

            if(tgl_button_senior_plus.IsChecked == true)
            {
                isPlus = true;
            } else
            {
                isPlus = false;
            }

            if(time_debut.SelectedDateTime == null)
            {
                time_debut.BorderBrush = new SolidColorBrush(Colors.Red);
            } else if(calendar.SelectedDate == null)
            {
                calendar.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                DateTime dt = new DateTime(calendar.SelectedDate.Value.Year, calendar.SelectedDate.Value.Month, calendar.SelectedDate.Value.Day, time_debut.SelectedDateTime.Value.Hour, time_debut.SelectedDateTime.Value.Minute, 00);
                Console.WriteLine(dt.ToString());
                Journee j = new Journee();
                j.DateJournee = dt;
                j.IsSeniorPlus = Convert.ToByte(isPlus);
                AdoJournee.create(j);

                mainWindow.Content = new DaysList(mainWindow);
            }
        }
    }
}
