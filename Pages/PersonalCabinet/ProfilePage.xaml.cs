﻿using System.Windows;
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
            messageBox.ShowDialog();
        }

        private void MailChange(object sender, RoutedEventArgs e)
        {
            messageBox = new CustomMessageBox("Изменение почты", "", MessageBoxImage.Asterisk);
            messageBox.ShowDialog();
        }
        private void PhoneChange(object sender, RoutedEventArgs e)
        {
            messageBox = new CustomMessageBox("Изменение номера", "", MessageBoxImage.Asterisk);
            messageBox.ShowDialog();
        }

        private void PasswordChange(object sender, RoutedEventArgs e)
        {
            messageBox = new CustomMessageBox("Изменение пароля", "", MessageBoxImage.Asterisk);
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

        private void RotatingIconButton_Click(object sender, RoutedEventArgs e)
        {
            _t = new TopupBalance();
            _t.Show();  
        }
    }
}
