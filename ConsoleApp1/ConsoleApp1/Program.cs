using ConsoleApp1.Controllers;
using ConsoleApp1.Helpers;
using ConsoleApp1.Helpers.Enums;
using ConsoleApp1.Helpers.Extensions;
using ConsoleApp1.Services;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Data;
using Repository.Repositories.Interfaces;
using Service.Services;
using Service.Services.Interfaces;
using System.Text;

namespace ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();

            services.AddScoped<AppDbContext>();

            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAudioService, AudioService>();

            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<ChatController>();

            services.AddScoped<VoiceService>();

            services.AddScoped<EducationController>();
            services.AddScoped<GroupController>();
            services.AddScoped<StudentController>();
            services.AddScoped<AuthController>();

            services.AddScoped<EmailService>();

            var serviceProvider = services.BuildServiceProvider();

            var authController = serviceProvider.GetRequiredService<AuthController>();
            var educationController = serviceProvider.GetRequiredService<EducationController>();
            var groupController = serviceProvider.GetRequiredService<GroupController>();
            var studentController = serviceProvider.GetRequiredService<StudentController>();
            var audioService = serviceProvider.GetRequiredService<IAudioService>();
            var chatController = serviceProvider.GetRequiredService<ChatController>();
            var voiceService = serviceProvider.GetRequiredService<VoiceService>();

            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            "=================================".WriteInfo();
            "   STUDENT MANAGEMENT SYSTEM     ".WriteSuccess();
            "=================================".WriteInfo();

            while (true)
            {
                User user = null;

                while (user == null)
                {
                    "\n=== AUTH MENU ===".WriteTitle();
                    "1. Register".WriteInfo();
                    "2. Login".WriteInfo();
                    "3. Play Music".WriteSuccess();
                    "4. Next Song".WriteInfo();
                    "5. Volume Up".WriteInfo();
                    "6. Volume Down".WriteInfo();
                    "7. Stop Music".WriteWarning();
                    "8. Chatbot (AI)".WriteSuccess();
                    "9. Voice Bot (Speech)".WriteSuccess();
                    "0. Exit".WriteWarning();

                    Console.Write("Select: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            authController.RunRegister();
                            break;

                        case "2":
                            user = authController.RunLogin();
                            if (user != null)
                                "Login successful!".WriteSuccess();
                            break;

                        case "3":
                            audioService.Play();
                            break;

                        case "4":
                            audioService.Next();
                            break;

                        case "5":
                            audioService.VolumeUp();
                            break;

                        case "6":
                            audioService.VolumeDown();
                            break;

                        case "7":
                            audioService.Stop();
                            break;

                        case "8":
                            await chatController.StartAsync();
                            break;

                        case "9":
                            StartVoiceBot(chatController, voiceService);
                            break;

                        case "0":
                            audioService.Stop();
                            return;

                        default:
                            "Invalid choice!".WriteError();
                            break;
                    }
                }

                while (true)
                {
                    WriteMainMenu();

                    if (!MenuInputHelper.TryReadMenuChoice("Select: ", out MainMenuOption mainChoice))
                    {
                        "Invalid choice.".WriteError();
                        continue;
                    }

                    switch (mainChoice)
                    {
                        case MainMenuOption.EducationMethods:
                            ShowEducationMenu(educationController);
                            break;

                        case MainMenuOption.GroupMethods:
                            ShowGroupMenu(groupController);
                            break;

                        case MainMenuOption.StudentMethods:
                            ShowStudentMenu(studentController);
                            break;

                        case MainMenuOption.Exit:
                            audioService.Stop();
                            user = null;
                            break;
                    }

                    if (user == null)
                        break;
                }
            }
        }

        private static void StartVoiceBot(ChatController chatController, VoiceService voice)
        {
            Console.WriteLine("\nVoice Bot started (type exit)");

            voice.Speak("Salam");

            while (true)
            {
                var input = voice.Listen();

                Console.WriteLine("Sən: " + input);

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.ToLower().Contains("exit"))
                    break;

                var response = chatController.GetResponseSync(input);

                Console.WriteLine("Bot: " + response);

                voice.Speak(response);
            }
        }

        private static void WriteMainMenu()
        {
            "\n--- MAIN MENU ---".WriteTitle();
            $"{(int)MainMenuOption.EducationMethods}. Education methods".WriteInfo();
            $"{(int)MainMenuOption.GroupMethods}. Group methods".WriteInfo();
            $"{(int)MainMenuOption.StudentMethods}. Student methods".WriteInfo();
            $"{(int)MainMenuOption.Exit}. Exit".WriteWarning();
        }

        private static void ShowEducationMenu(EducationController educationController) { }
        private static void ShowGroupMenu(GroupController groupController) { }
        private static void ShowStudentMenu(StudentController studentController) { }
    }
}