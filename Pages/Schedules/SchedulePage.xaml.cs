using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace BusNetworkSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public MainWindow mainWindow;
        private DispatcherTimer _timer;
        Custom.CustomMessageBox messageBox;

        public SchedulePage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString("M") + ", " + DateTime.Now.ToString("T");
        }

        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            if (MenuTransform.X == -310)
            {
                // Открываем меню
                Storyboard slideIn = (Storyboard)FindResource("MenuSlideIn");
                slideIn.Begin(MenuGrid);
            }
            else
            {
                // Закрываем меню
                Storyboard slideOut = (Storyboard)FindResource("MenuSlideOut");
                slideOut.Begin(MenuGrid);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Скрываем PlaceholderText, если в TextBox есть текст
            PlaceholderText.Visibility = string.IsNullOrEmpty(SearchTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void GoToProfile(object sender, RoutedEventArgs e)
        {
            mainWindow.main.Navigate(new PersonalCabinet.ProfilePage(mainWindow));
            ToggleMenu(sender, e);
        }

        private void GoToBuses(object sender, RoutedEventArgs e)
        {
            mainWindow.main.Navigate(new Buses.BusesPage(mainWindow));
            ToggleMenu(sender, e);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            messageBox = new Custom.CustomMessageBox("Вы точно хотите выйти?", "Подтверждение", MessageBoxImage.Question);
            bool? result = messageBox.ShowDialog();

            if (result == true)
            {
                mainWindow.main.Navigate(new LoginAndRegisPage(mainWindow));
            }
        }
    }
}
