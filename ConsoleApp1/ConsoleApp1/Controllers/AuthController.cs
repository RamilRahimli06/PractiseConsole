using ConsoleApp1.Helpers.Extensions;
using Domain.Entities;
using Service.Services.Interfaces;
using System;

namespace ConsoleApp1.Controllers
{
    public class AuthController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public bool RunRegister()
        {
            Console.WriteLine("\n--- REGISTER ---");

            Console.Write("Full name: ");
            string fullName = Console.ReadLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = ReadPassword();

            Console.Write("Confirm password: ");
            string confirmPassword = ReadPassword();

            var (success, message) = _authService.Register(fullName, username, email, password, confirmPassword);

            if (success)
                "Registration successful!".WriteSuccess();
            else
                "Registration failed!".WriteError();

            return success;
        }

        public User RunLogin()
        {
            Console.WriteLine("\n--- LOGIN ---");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = ReadPassword();

            User user = _authService.Login(username, password);

            if (user == null)
            {
                "Login failed!".WriteError();
                return null;
            }

            $"Welcome, {user.FullName}!".WriteInfo();
            return user;
        }

        private static string ReadPassword()
        {
            string password = Console.ReadLine();
            return password ?? string.Empty;
        }
        public (bool Success, string Message) VerifyOtpFlow(string otp)
        {
            return _authService.VerifyOtpPending(otp);
        }
    }
}