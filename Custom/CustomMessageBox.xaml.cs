using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BusNetworkSystem.Custom
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string message, string title, MessageBoxImage image = MessageBoxImage.None)
        {
            InitializeComponent();

            this.Title = title;
            MessageText.Text = message;

            switch (image)
            {
                case MessageBoxImage.Error:
                    IconText.Text = "❌"; // Иконка ошибки
                    IconText.Foreground = Brushes.Red;
                    break;
                case MessageBoxImage.Warning:
                    IconText.Text = "⚠️"; // Иконка предупреждения
                    IconText.Foreground = Brushes.Orange;
                    break;
                case MessageBoxImage.Information:
                    IconText.Text = "ℹ️"; // Иконка информации
                    IconText.Foreground = Brushes.Blue;
                    break;
                default:
                    IconText.Text = ""; // Без иконки
                    break;
            }

            this.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
