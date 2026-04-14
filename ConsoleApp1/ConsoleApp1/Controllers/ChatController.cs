using Service.Services.Interfaces;

namespace ConsoleApp1.Controllers
{
    public class ChatController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task StartAsync()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Chatbot başladı (exit yaz çıx)");

            while (true)
            {
                Console.Write("Sən: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                string response = await _chatService.AskAsync(input);

                Console.WriteLine($"Bot: {response}");
            }

        }

        public string GetResponseSync(string input)
        {
            return _chatService.AskAsync(input).GetAwaiter().GetResult();
        }
    }
}