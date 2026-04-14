using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services;
using Service.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly EmailService _emailService;

        private static Dictionary<string, (string Code, DateTime ExpireTime)> _otpStorage
            = new();

        public AuthService(IUserRepository userRepository, EmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

  
        public (bool Success, string Message) Register(
            string fullName,
            string username,
            string email,
            string password,
            string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return (false, "Full name empty");

            if (_userRepository.UsernameExists(username))
                return (false, "Username exists");

            if (_userRepository.EmailExists(email))
                return (false, "Email exists");

            if (password != confirmPassword)
                return (false, "Passwords not match");

            var user = new User
            {
                FullName = fullName,
                Username = username,
                Email = email,
                Password = HashPassword(password),
                IsVerified = false
            };

            _userRepository.Create(user);

            string code = new Random().Next(100000, 999999).ToString();

            _otpStorage[email] = (code, DateTime.Now.AddMinutes(3));

            _emailService.SendVerificationCode(email, code);

            return (true, "OTP sent to email");
        }

       
        public (bool Success, string Message) VerifyOtp(string email, string code)
        {
            if (!_otpStorage.ContainsKey(email))
                return (false, "OTP not found");

            var data = _otpStorage[email];

            if (DateTime.Now > data.ExpireTime)
            {
                _otpStorage.Remove(email);
                return (false, "OTP expired");
            }

            if (data.Code != code)
                return (false, "Wrong OTP");

            var user = _userRepository.GetByEmail(email);

            if (user == null)
                return (false, "User not found");

            user.IsVerified = true;

            _userRepository.Save(); 

            _otpStorage.Remove(email);

            return (true, "Email verified successfully");
        }

       
        public User Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user == null)
                return null;

            if (user.Password != HashPassword(password))
                return null;

            if (!user.IsVerified)
                return null;

            return user;
        }

        
        public (bool Success, string Message) ResendOtp(string email)
        {
            string code = new Random().Next(100000, 999999).ToString();

            _otpStorage[email] = (code, DateTime.Now.AddMinutes(3));

            _emailService.SendVerificationCode(email, code);

            return (true, "OTP resent");
        }

      
        private static string HashPassword(string password)
        {
            using SHA256 sha = SHA256.Create();
            return Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        
        public (bool Success, string Message) VerifyOtpPending(string code)
        {
            throw new NotImplementedException();
        }
    }
}