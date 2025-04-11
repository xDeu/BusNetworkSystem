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
using System.Windows.Shapes;
using BusNetworkSystem.Custom;

namespace BusNetworkSystem.Pages.PersonalCabinet
{
    /// <summary>
    /// Логика взаимодействия для TopupBalance.xaml
    /// </summary>
    public partial class TopupBalance : Window
    {
        CustomMessageBox messageBox;
        public TopupBalance()
        {
            InitializeComponent();    
        }

        private void TopupAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderText.Visibility = string.IsNullOrEmpty(TopupAmount.Text) ? Visibility.Visible : Visibility.Collapsed;
            if (!string.IsNullOrEmpty(TopupAmount.Text))
                btnOK.IsEnabled = true;
            else btnOK.IsEnabled = false;
                
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RotatingIconButton_Click(object sender, RoutedEventArgs e)
        {
            messageBox = new CustomMessageBox("Операция выполнена.", "Выполнено!", MessageBoxImage.Exclamation);
            messageBox.ShowDialog();
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
