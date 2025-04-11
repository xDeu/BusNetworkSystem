using System;
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BusNetworkSystem.Pages.Schedules;

namespace BusNetworkSystem.Pages.LoginRegistration
{
    /// <summary>
    /// Логика взаимодействия для LoginAndRegisPage.xaml
    /// </summary>
    public partial class LoginAndRegisPage : Page
    {
        public MainWindow mainWindow;
        Custom.CustomMessageBox messageBox;

        public LoginAndRegisPage(MainWindow _mainWindow)
        {
            InitializeComponent();
            CenterStackPanel();
            mainWindow = _mainWindow;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = LoginPasswordBox.Password;

            #region Проверка на наличие логина и пароля
            if (string.IsNullOrWhiteSpace(login) && string.IsNullOrWhiteSpace(password))
            {
                messageBox = new Custom.CustomMessageBox("Пожалуйста, введите логин и пароль.", "Ошибка", MessageBoxImage.Warning);
                messageBox.ShowDialog();
                return;
            }
            if (string.IsNullOrWhiteSpace(login))
            {
                messageBox = new Custom.CustomMessageBox("Пожалуйста, введите логин.", "Ошибка", MessageBoxImage.Warning);
                messageBox.ShowDialog();
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                messageBox = new Custom.CustomMessageBox("Пожалуйста, введите пароль.", "Ошибка", MessageBoxImage.Warning);
                messageBox.ShowDialog();
                return;
            }
            #endregion

            // Проверка с фиксиорванными значениями;  Добавить проверку с БД
            if (login == "1")
            {
                if (password == "1")
                {
                    messageBox = new Custom.CustomMessageBox("Вход выполнен успешно!", "Успех", MessageBoxImage.Information);
                    messageBox.ShowDialog();
                    mainWindow.main.Navigate(new SchedulePage(mainWindow));
                }
                else 
                { 
                    messageBox = new Custom.CustomMessageBox("Неверный пароль.", "Ошибка", MessageBoxImage.Error);
                    messageBox.ShowDialog();
                }
            }
            else
            {
                messageBox = new Custom.CustomMessageBox("Неверный логин.", "Ошибка", MessageBoxImage.Error);
                messageBox.ShowDialog();
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            AnimatePanels(LoginPanel, RegisterPanel);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AnimatePanels(RegisterPanel, LoginPanel);
        }

        private void RegisterConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // ДОБАВИТЬ ЛОГИКУ РЕГИСТРАЦИИ
            AnimatePanels(RegisterPanel, LoginPanel);
        }
        #region Анимация панелей
        private void AnimatePanels(FrameworkElement outgoingPanel, FrameworkElement incomingPanel)
        {
            // Анимация для исходящего панела
            DoubleAnimation moveOutAnimation = new DoubleAnimation
            {
                From = 0,
                To = -500,
                Duration = TimeSpan.FromSeconds(0.5)
            };

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.1)
            };

            // Анимация для входящего панела
            DoubleAnimation moveInAnimation = new DoubleAnimation
            {
                From = -500,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5)
            };

            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.1)
            };

            outgoingPanel.BeginAnimation(Canvas.LeftProperty, moveOutAnimation);
            outgoingPanel.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);

            incomingPanel.Visibility = Visibility.Visible; // Показываем панель регистрации
            incomingPanel.BeginAnimation(Canvas.LeftProperty, moveInAnimation);
            incomingPanel.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);

            fadeOutAnimation.Completed += (s, e) => outgoingPanel.Visibility = Visibility.Collapsed;

        }
        #endregion
        #region Это обязательное поле
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.BorderBrush == Brushes.Red)
            {
                textBox.Clear();
                textBox.BorderBrush = Brushes.Black;
                GetErrorMessageTextBlock(textBox).Visibility = Visibility.Collapsed;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox && passwordBox.BorderBrush == Brushes.Red)
            {
                passwordBox.Clear();
                passwordBox.BorderBrush = Brushes.Black;
                ErrorMessage2.Visibility = Visibility.Collapsed;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BorderBrush = Brushes.Red;
                GetErrorMessageTextBlock(textBox).Visibility = Visibility.Visible;
                GetErrorMessageTextBlock(textBox).Text = "Это обязательное поле.";
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox && string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.BorderBrush = Brushes.Red;
                ErrorMessage2.Visibility = Visibility.Visible;
                ErrorMessage2.Text = "Это обязательное поле.";
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BorderBrush = Brushes.Black;
                GetErrorMessageTextBlock(textBox).Visibility = Visibility.Collapsed;
            }
        }

        private TextBlock GetErrorMessageTextBlock(TextBox textBox)
        {
            if (textBox == LoginTextBox)
                return ErrorMessage1;

            return null;
        }
        #endregion
        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;

            // Определяем, какой PasswordBox и TextBox связаны с этой кнопкой
            if (button == ShowLoginPasswordButton)
            {
                ToggleVisibility(LoginPasswordBox, LoginPasswordTextBox);
            }
            else if (button == ShowRegisterPasswordButton)
            {
                ToggleVisibility(RegisterPasswordBox, RegisterPasswordTextBox);
            }
            else if (button == ShowRepeatPasswordButton)
            {
                ToggleVisibility(RepeatPasswordBox, RepeatPasswordTextBox);
            }
        }

        private void ToggleVisibility(PasswordBox passwordBox, TextBox textBox)
        {
            if (passwordBox.Visibility == Visibility.Visible)
            {
                // Переключаем на TextBox
                textBox.Text = passwordBox.Password;
                passwordBox.Visibility = Visibility.Collapsed;
                textBox.Visibility = Visibility.Visible;
            }
            else
            {
                // Переключаем на PasswordBox
                passwordBox.Password = textBox.Text;
                textBox.Visibility = Visibility.Collapsed;
                passwordBox.Visibility = Visibility.Visible;
            }
        }
        private void CenterStackPanel()
        {
            // Вычисляем центр Canvas
            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;

            // Вычисляем позицию для центрирования StackPanel
            double stackPanelWidth1 = LoginPanel.ActualWidth;
            double stackPanelHeight1 = LoginPanel.ActualHeight;

            double stackPanelWidth2 = RegisterPanel.ActualWidth;
            double stackPanelHeight2 = RegisterPanel.ActualHeight;

            double left1 = (canvasWidth - stackPanelWidth1) / 2;
            double top1 = (canvasHeight - stackPanelHeight1) / 2;

            double left2 = (canvasWidth - stackPanelWidth2) / 2;
            double top2 = (canvasHeight - stackPanelHeight2) / 2;

            // Устанавливаем позицию StackPanel
            Canvas.SetLeft(LoginPanel, left1);
            Canvas.SetTop(LoginPanel, top1);

            Canvas.SetLeft(RegisterPanel, left2);
            Canvas.SetTop(RegisterPanel, top2);
        }
    }
}
