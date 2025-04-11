using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BusNetworkSystem.Custom;

namespace BusNetworkSystem.Pages.PersonalCabinet
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public MainWindow mainWindow;
        CustomMessageBox messageBox;
        TopupBalance _t;

        public ProfilePage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;

            DisplayText();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void DisplayText()
        {
            UserNameTB.Text = "Имя пользователя: placeholder_name";
            UserEmailTB.Text = "Электронный адрес: placeholder_email";
            UserPhoneNumTB.Text = "Номер телефона: placeholde_number";
        }

        private void NameChange(object sender, RoutedEventArgs e)
        {
            messageBox = new CustomMessageBox("Изменение имени", "", MessageBoxImage.Asterisk);
            Opacity = 0.4;
            messageBox.ShowDialog();
            Opacity = 1;
        }

        private void MailChange(object sender, RoutedEventArgs e)
        {
            messageBox = new CustomMessageBox("Изменение почты", "", MessageBoxImage.Asterisk);
            Opacity = 0.4;
            messageBox.ShowDialog();
            Opacity = 1;
        }
        private void PhoneChange(object sender, RoutedEventArgs e)
        {
            messageBox = new CustomMessageBox("Изменение номера", "", MessageBoxImage.Asterisk);
            Opacity = 0.4;
            messageBox.ShowDialog();
            Opacity = 1;
        }

        private void PasswordChange(object sender, RoutedEventArgs e)
        {
            var changeWindow = new PasswordChange();
            Opacity = 0.4;
            changeWindow.ShowDialog();
            Opacity = 1;
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

        private void RotatingIconButton_Click(object sender, RoutedEventArgs e)
        {
            _t = new TopupBalance();
            this.Opacity = 0.4;
            _t.ShowDialog();
            this.Opacity = 1;
        }
    }
}
