using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace SignalRConsoleClient
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var apiKey = _configuration["MyApiSettings:ApiKey"];
            return (IActionResult)Content($"API Key: {apiKey}");
        }
    }
    public class ConsoleChat
    {
        private HubConnection _connection { get; set; }

        public ConsoleChat()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddUserSecrets<Program>()
            .Build();

            var apiKey = configuration["MyApiSettings:ApiKey"];

            _connection = new HubConnectionBuilder()
                          .WithUrl($"{apiKey}")
                          .Build();
        }
        public void Intro()
        {
            Console.WriteLine("Welcome to the NerdCraft Chat");
            Console.WriteLine("Press ESC key to continue");
            Console.CursorVisible = false; // Hide the cursor

            int screenWidth = Console.WindowWidth;
            int position = 0;
            string hotdog = "  HOTDOG!!!";
            int animationSpeed = 100; // Adjust the speed of animation (in milliseconds)
            bool exitRequested = false;

            Thread inputThread = new Thread(() =>
            {
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(intercept: true).Key;
                        if (key == ConsoleKey.Escape)
                        {
                            exitRequested = true;
                            break;
                        }
                    }
                }
            });

            inputThread.Start();

            while (!exitRequested)
            {
                Console.SetCursorPosition(position, Console.WindowHeight / 2);
                Console.Write(hotdog);
                Thread.Sleep(animationSpeed);
                Console.SetCursorPosition(position, Console.WindowHeight / 2);
                Console.Write(new string(' ', hotdog.Length));
                position++;

                if (position >= screenWidth)
                    position = 0;
            }

            inputThread.Join(); // Wait for the input thread to exit
            Console.Clear();
        }
        public async Task ConnectChat()
        {
            try
            {
                await _connection.StartAsync();
                Console.WriteLine("Connection established.");
                Console.WriteLine("Type Exit to exit the chat.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting the connection: {ex.Message}");
                return;
            }
        }
        public async Task User()
        {
            
            Console.Write("Enter your username: ");
            var username = Console.ReadLine();

            _connection.On<string, string>("broadcastMessage", (user, message) =>
            {
                Console.WriteLine($"\n\r{user}: {message}");
            });

            while (true)
            {
                Console.Write("Your message: ");
                var message = Console.ReadLine();

                if (message.ToUpper() == "EXIT")
                {
                    var userLeaving = $"is Exiting..." + " " + DateTime.Now.ToString();
                    await _connection.InvokeAsync("Send", username, userLeaving);
                    break; // Exit the while loop and the program
                }
                message = message + " " + DateTime.Now.ToString();
                await _connection.InvokeAsync("Send", username, message);
            }
        }
    }
}




