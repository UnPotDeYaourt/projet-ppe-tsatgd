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
    /// Logique d'interaction pour DaysList.xaml
    /// </summary>
    public partial class DaysList : Page
    {
        private MainWindow mainWindow;
        

        public DaysList(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;

            ic_days.ItemsSource = MainWindow.journeesLite;
        }

        private void btn_show_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            Journee day = MainWindow.journeesLite.First(x => x.IdJournee == Convert.ToInt32(id));
            mainWindow.Content = new MeetingsList(day, this.mainWindow);
        }

        private void btn_available_player_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            Journee day = MainWindow.journees.First(x => x.IdJournee == Convert.ToInt32(id));
            mainWindow.Content = new AvailablePlayers(mainWindow, day);
        }
        
        private void btn_goto_CreateDay(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new CreateDay(mainWindow);
        }
    }
}
