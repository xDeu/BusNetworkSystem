using BusNetworkSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace BusNetworkSystem.ViewModels
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly INavigationService _navigationService;

        public event PropertyChangedEventHandler PropertyChanged;

        // Свойства для входа
        public string Username { get; set; }
        public string Password { get; set; }

        // Свойства для регистрации
        public string RegisterUsername { get; set; }
        public string RegisterPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

        // Команды
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand GuestLoginCommand { get; }
        public ICommand SwitchToRegisterCommand { get; }
        public ICommand SwitchToLoginCommand { get; }

        public AuthViewModel(IAuthService authService, INavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;

            LoginCommand = new RelayCommand(OnLogin);
            RegisterCommand = new RelayCommand(OnRegister);
            GuestLoginCommand = new RelayCommand(OnGuestLogin);
            SwitchToRegisterCommand = new RelayCommand(SwitchToRegister);
            SwitchToLoginCommand = new RelayCommand(SwitchToLogin);
        }

        private async void OnLogin()
        {
            if (await _authService.LoginAsync(Username, Password))
            {
                _navigationService.NavigateTo("Main");
            }
        }

        private async void OnRegister()
        {
            if (RegisterPassword != ConfirmPassword)
            {
                // Показать ошибку
                return;
            }

            if (await _authService.RegisterAsync(RegisterUsername, RegisterPassword, Email))
            {
                // После успешной регистрации возвращаемся на панель входа
                SwitchToLogin();
            }
        }

        private void OnGuestLogin()
        {
            _authService.LoginAsGuest();
            _navigationService.NavigateTo("Main");
        }

        private void SwitchToRegister()
        {
            // Запуск анимации перехода к регистрации
            var storyboard = Application.Current.Resources["LoginToRegisterAnimation"] as Storyboard;
            storyboard?.Begin();
        }

        private void SwitchToLogin()
        {
            // Запуск анимации возврата ко входу
            var storyboard = Application.Current.Resources["RegisterToLoginAnimation"] as Storyboard;
            storyboard?.Begin();
        }
    }
}
