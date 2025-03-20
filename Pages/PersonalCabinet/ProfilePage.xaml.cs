using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BusNetworkSystem.Pages.PersonalCabinet
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public MainWindow mainWindow;
        Custom.CustomMessageBox messageBox;

        public ProfilePage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void NameChange(object sender, RoutedEventArgs e)
        {
            messageBox = new Custom.CustomMessageBox("Изменение имени", "", MessageBoxImage.Asterisk);
            messageBox.ShowDialog();
        }

        private void MailChange(object sender, RoutedEventArgs e)
        {
            messageBox = new Custom.CustomMessageBox("Изменение почты", "", MessageBoxImage.Asterisk);
            messageBox.ShowDialog();
        }
        private void PhoneChange(object sender, RoutedEventArgs e)
        {
            messageBox = new Custom.CustomMessageBox("Изменение номера", "", MessageBoxImage.Asterisk);
            messageBox.ShowDialog();
        }

        private void PasswordChange(object sender, RoutedEventArgs e)
        {
            messageBox = new Custom.CustomMessageBox("Изменение пароля", "", MessageBoxImage.Asterisk);
            messageBox.ShowDialog();
        }
        private void TogglePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                // Переключаем на TextBox
                PasswordTextBox.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                // Переключаем на PasswordBox
                PasswordBox.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
            }
        }
    }
}
