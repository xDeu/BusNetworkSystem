using BusNetworkSystem.DAL.BusNetworkSystem.DAL;
using BusNetworkSystem.Services;
using BusNetworkSystem.ViewModels;
using BusNetworkSystem.Views.Auth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BusNetworkSystem
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Регистрация сервисов
            services.AddSingleton<BusStationDbContext>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IDialogService, DialogService>();

            // Настройка NavigationService
            var navigationService = new NavigationService();
            navigationService.Configure(typeof(AuthView), () => new AuthView(
                new AuthViewModel(
                    services.BuildServiceProvider().GetRequiredService<IAuthService>(),
                    navigationService)));

            services.AddSingleton<INavigationService>(navigationService);

            // Создание главного окна
            var mainWindow = new MainWindow();
            mainWindow.MainFrame.Navigate(new AuthView(
                new AuthViewModel(
                    services.BuildServiceProvider().GetRequiredService<IAuthService>(),
                    navigationService)));

            mainWindow.Show();
        }
    }
}
