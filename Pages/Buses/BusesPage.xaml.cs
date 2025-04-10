using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace BusNetworkSystem.Pages.Buses
{
    public partial class BusesPage : Page
    {
        public MainWindow mainWindow;
        private List<Bus> buses;
        private int currentIndex;

        public BusesPage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            LoadBuses();
            DisplayBus(currentIndex);
        }

        private void LoadBuses()
        {
            buses = new List<Bus>
            {
                new Bus { ImagePath = "\\Pages\\Buses\\busImages\\bus1.jpg", LicencePlate = "Р934ВХ12", Model = "ППаЗ 36124", Capacity = 50, YearOfManufacture = 1996 },
                new Bus { ImagePath = "\\Pages\\Buses\\busImages\\bus2.jpg", LicencePlate = "Н846ОО21", Model = "МАЗ Победа", Capacity = 70, YearOfManufacture = 2005 },
                new Bus { ImagePath = "\\Pages\\Buses\\busImages\\bus3.jpg", LicencePlate = "М945МИ51", Model = "Ford Galaxy", Capacity = 30, YearOfManufacture = 2015 }
            };
        }

        private void DisplayBus(int index)
        {
            BusImage.Source = new BitmapImage(new Uri(buses[index].ImagePath, UriKind.Relative));
            LicencePlate.Text = "Номерной знак: " + buses[index].LicencePlate;
            Model.Text = "Модель: " + buses[index].Model;
            Capacity.Text = "Вместимость: " + Convert.ToString(buses[index].Capacity);
            YearOfManufacture.Text = "Год производства: " + Convert.ToString(buses[index].YearOfManufacture);

            BusImage.Opacity = 1;
            LicencePlate.Opacity = 1;
            Model.Opacity = 1;
            Capacity.Opacity = 1;
            YearOfManufacture.Opacity = 1;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < buses.Count - 1)
            {
                currentIndex++;
                AnimateTransition();
            }
            else if (currentIndex == buses.Count - 1 && NextImage.IsPressed)
            {
                currentIndex = 0;
                AnimateTransition();
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                AnimateTransition();
            }
            else if (currentIndex == 0 && PreviousImage.IsPressed)
            {
                currentIndex = 2;
                AnimateTransition();
            }
        }

        private void AnimateTransition()
        {
            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
            fadeOut.Completed += (s, e) =>
            {
                DisplayBus(currentIndex);
                DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
                BusImage.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                LicencePlate.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                Model.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                Capacity.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                YearOfManufacture.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            };
            BusImage.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            LicencePlate.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            Model.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            Capacity.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            YearOfManufacture.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для возврата на предыдущую страницу
            NavigationService.GoBack();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                PreviousImage.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            if (e.Key == Key.Right)
            {
                NextImage.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}
