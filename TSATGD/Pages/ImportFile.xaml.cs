using System;
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
    /// Logique d'interaction pour ImportFile.xaml
    /// </summary>
    public partial class ImportFile : Page
    {
        private MainWindow mainWindow;

        public ImportFile(MainWindow _mainWindow)
        {
            InitializeComponent();
            this.mainWindow = _mainWindow;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e) // Quand le bouton "Retour" est cliqué 
        {
            mainWindow.Content = new PlayersList(mainWindow);
        }

        private void panel_ImportFileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
        }
    }
}
