using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace MyDiscordBot
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            var bot = new DiscordBotClient();
            await bot.StartAsync();
        }
    }

    class DiscordBotClient
    {
        private DiscordSocketClient _client;

        public async Task StartAsync()
        {
            // Налаштування клієнта (вмикаємо можливість читати повідомлення)
            var config = new DiscordSocketConfig { GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent };
            _client = new DiscordSocketClient(config);

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;

            // ВСТАВ СВІЙ ТОКЕН СЮДИ
            string token = "MTQ5NTA2Njk0MTY5Njc3MDA2OA.GAAu9N.V0ZXj7k0v35e-8-Irm0YyLyHUmNJZXbInZSVP4";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1); // Тримаємо програму відкритою
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Підключено до Gateway як {_client.CurrentUser}");
            return Task.CompletedTask;
        }

        private Task MessageReceivedAsync(SocketMessage message)
        {
            // Бот ігнорує власні повідомлення
            if (message.Author.Id == _client.CurrentUser.Id)
                return Task.CompletedTask;

            // Якщо хтось пише !ping, бот відповідає
            if (message.Content == "!ping")
            {
                Console.WriteLine($"Отримано команду від {message.Author.Username}");
                message.Channel.SendMessageAsync("Pong! Gateway працює.");
            }

            return Task.CompletedTask;
        }
    }
}