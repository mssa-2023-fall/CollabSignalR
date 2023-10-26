using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

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
            AnsiConsole.MarkupLine("[blue]Welcome to the NerdCraft Chat[/]");
            AnsiConsole.MarkupLine("[yellow]Press ESC key to continue[/]");
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
                AnsiConsole.MarkupLine($"[yellow]{hotdog}[/]");
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
                await AnsiConsole.Status()
            .StartAsync("Connecting...", async ctx =>
            {
                // Omitted
                await _connection.StartAsync();
                AnsiConsole.MarkupLine("[green]Connection established.[/]");
                AnsiConsole.MarkupLine("[yellow]Type Exit to exit the chat.[/]");
            });
                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting the connection: {ex.Message}");
                return;
            }

            _connection.On<string, string>("broadcastMessage", (user, message) =>
            {
                AnsiConsole.MarkupLine($"\n\r[teal]{DateTime.Now.ToString()} [/][red]{user}[/]: [blue]{message}[/]");
            });
        }
        public async Task User()
        {
            
            Console.Write("Enter your username: ");
            var username = Console.ReadLine();
            //sends a broadcast of a new Console User
            while (true)
            {
                //This new user connection needs to be broadcasted to everyone
                var newUserConnected = $"[aqua]{username} has connected.[/]";
                await _connection.InvokeAsync("Send", "", newUserConnected);
                break;
            }

           //The user can type a message to broadcast to the other users connected
            while (true)
            {
                Console.CursorVisible = true;
                
                var message = Console.ReadLine();
                
                if (message.ToUpper() == "EXIT")
                {
                    AnsiConsole.MarkupLine($"[aqua]{DateTime.Now.ToString()} {username} is exiting...[/]");
                    break; // Exit the while loop and the program
                }
                else
                {
                    await _connection.InvokeAsync("Send", username, message);
                }
               
                
            }
        }
    }
}




