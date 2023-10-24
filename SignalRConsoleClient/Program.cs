using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
class program
{
    static void Main(string[] args)
    {
        var hubUrl = "Console.WriteLine('https://chatsample20231024085542.azurewebsites.net/chat')";

        Console.WriteLine("Which hub do you want to connect to? Press Enter for Default");
        Console.WriteLine($"[{hubUrl}]");

        var userHubUrl = Console.ReadLine();
        var _connection = new HubConnectionBuilder()
                        .WithUrl(string.IsNullOrEmpty(userHubUrl) ? hubUrl : userHubUrl)
                        .Build();
        _connection.StartAsync().Wait(); 

        Dictionary<string, string> UserInfo = new Dictionary<string, string>();

        Console.WriteLine("Whats your name?");
        string userName = Console.ReadLine();

        StoreNames(userName, UserInfo);

        Console.WriteLine($"{userName} what is your message?");
        string message = Console.ReadLine();

        //_connection.InvokeAsync("SendMessage", userName, message).Wait(); Could this be a way to send the message?

        _connection.On<string, string>("broadcastMessage", (s1, s2) => Console.WriteLine(s1));
    }
    static void StoreNames(string user, Dictionary<string, string> storeNames)
    {
        if (storeNames.Keys == null) throw new ArgumentException("Name cannot be null", "Error");
        storeNames.TryAdd(user, "Session User");
    }

}

// Hub: https://chatsample20231024085542.azurewebsites.net/chat
//Collect UserName