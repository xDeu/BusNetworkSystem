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

namespace BusNetworkSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для BusesPage.xaml
    /// </summary>
    public partial class BusesPage : Page
    {
        public MainWindow mainWindow;
        public BusesPage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.main.GoBack();
        }
    }
}
