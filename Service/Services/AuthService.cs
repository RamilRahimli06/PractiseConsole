using Domain.Common;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public (bool Success, string Message) Register(string fullName, string username, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return (false, "Full name cannot be empty.");
            }

            if (!ValidationHelper.FullNameHasNoDigits(fullName))
            {
                return (false, "Full name cannot contain digits.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return (false, "Username cannot be empty.");
            }

            username = username.Trim();
            if (_userRepository.UsernameExists(username))
            {
                return (false, "Username is already taken.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return (false, "Email cannot be empty.");
            }

            email = email.Trim();
            if (!ValidationHelper.EmailHasValidAt(email))
            {
                return (false, "Email must contain @ with text before and after it.");
            }

            if (_userRepository.EmailExists(email))
            {
                return (false, "Email is already registered.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return (false, "Password cannot be empty.");
            }

            if (!IsPasswordStrong(password))
            {
                return (false, "Password must have at least 1 uppercase, 1 lowercase, and 1 symbol.");
            }

            if (confirmPassword != password)
            {
                return (false, "Password and confirm password do not match.");
            }

            User user = new User
            {
                FullName = fullName.Trim(),
                Username = username,
                Email = email,
                Password = HashPassword(password)
            };

            _userRepository.Create(user);
            return (true, "Registration successful.");
        }

        public User Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            User user = _userRepository.GetByUsername(username.Trim());
            if (user == null)
            {
                return null;
            }

            string hash = HashPassword(password);
            if (user.Password != hash)
            {
                return null;
            }

            return user;
        }

        private static bool IsPasswordStrong(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasSymbol = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasSymbol;
        }

        private static string HashPassword(string password)
        {
            using SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
