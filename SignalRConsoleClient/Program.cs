using Microsoft.AspNetCore.SignalR.Client;
using SignalRConsoleClient;

ConsoleChat consoleChat = new ConsoleChat();
consoleChat.Intro();
await consoleChat.ConnectChat();
await consoleChat.User();