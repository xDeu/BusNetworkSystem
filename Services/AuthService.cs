using BusNetworkSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusNetworkSystem.Services
{
    public interface IAuthService
    {
        bool IsLoggedIn { get; }
        User CurrentUser { get; }
        bool Login(string username, string password);
        void Logout();
        bool Register(User newUser);
        void LoginAsGuest();
    }
    internal class AuthService : IAuthService
    {
        private readonly DataService _dataService;
        private User _currentUser;

        public AuthService(DataService dataService)
        {
            _dataService = dataService;
        }

        public bool IsLoggedIn => _currentUser != null;
        public User CurrentUser => _currentUser;

        public bool Login(string email, string password)
        {
            var user = new DataService().GetUserByEmail(email);
            if (user.UserID == 0) return false;

            var hashedInput = ComputeSha256Hash(password);
            if (user.PasswordHash != hashedInput) return false;

            _currentUser = user;
            return true;
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        public void Logout()
        {
            _currentUser = null;
        }
        public bool Register(User newUser)
        {
            return _dataService.RegisterUser(newUser);
        }
        public void LoginAsGuest()
        {
            _currentUser = new User { Name = "Гость", Role = 0 };
        }
    }      
}
