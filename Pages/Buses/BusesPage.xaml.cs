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
                new Bus { ImagePath = "\\Pages\\Buses\\busImages\\bus1.jpg", Info = "Автобус 1: 50 мест, дизельный", Description = "Это надежный автобус для междугородних поездок." },
                new Bus { ImagePath = "\\Pages\\Buses\\busImages\\bus2.jpg", Info = "Автобус 2: 40 мест, электрический", Description = "Экологически чистый автобус с низким уровнем шума." },
                new Bus { ImagePath = "\\Pages\\Buses\\busImages\\bus3.jpg", Info = "Автобус 3: 30 мест, газовый", Description = "Идеален для городских маршрутов." }
            };
        }

        private void DisplayBus(int index)
        {
            BusImage.Source = new BitmapImage(new Uri(buses[index].ImagePath, UriKind.Relative));
            BusInfo.Text = buses[index].Info;
            BusDescription.Text = buses[index].Description;
            BusImage.Opacity = 1; // Убедитесь, что изображение видно
            BusInfo.Opacity = 1; // Убедитесь, что текст видно
            BusDescription.Opacity = 1;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < buses.Count - 1)
            {
                currentIndex++;
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
        }

        private void AnimateTransition()
        {
            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
            fadeOut.Completed += (s, e) =>
            {
                DisplayBus(currentIndex);
                DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
                BusImage.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                BusInfo.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                BusDescription.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            };
            BusImage.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            BusInfo.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            BusDescription.BeginAnimation(UIElement.OpacityProperty, fadeOut);
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
