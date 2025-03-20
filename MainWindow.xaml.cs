using BusNetworkSystem.Pages;
using System.Windows;

namespace BusNetworkSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            main.Navigate(new LoginAndRegisPage(this));
        }
    }
}
