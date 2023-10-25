using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
class program
{
    static async void Main(string[] args)
    {   //defualt hub
        var hubUrl = "https://chatsample20231024085542.azurewebsites.net/chat";

        Console.WriteLine("Which hub do you want to connect to? Press Enter for Default");
        
        //chance to connect to a different hub
        var userHubUrl = Console.ReadLine();
        var connection = new HubConnectionBuilder()
                        .WithUrl(string.IsNullOrEmpty(userHubUrl) ? hubUrl : userHubUrl)
                        .Build();

        try
        {
            connection.StartAsync().Wait();
            Console.WriteLine("Connection established.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting the connection: {ex.Message}");
        }
        
        Dictionary<string, string> userDictionary = new Dictionary<string, string>(); //Dictionary to map connectionId with Users

        //receive message
        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"{user}: {message}");
        });

        //notificaiton of connected user
        connection.On<string>("UserConnected", (connectionId) =>
        {
            Console.WriteLine($"User connected with connectionId: {connectionId}");
            Console.WriteLine("Enter your username: ");
            var username = Console.ReadLine();
            userDictionary[connectionId] = username;
            Console.WriteLine($"Welcome, {username}");

        });

        //notification of disconnected user
        connection.On<string>("UserDisconnected", (connectionId) =>
        {
            if (userDictionary.ContainsKey(connectionId))
            {
                var disconnectedUser = userDictionary[connectionId];
                Console.WriteLine($"{disconnectedUser} has disconnected.");
                userDictionary.Remove(connectionId);
            }
        });

        //Enter message and send message async
        while(true)
        {
            Console.Write($"Enter Message: ");
            var message = Console.ReadLine();

            if (userDictionary.ContainsKey(connection.ConnectionId))
            {
                var username = userDictionary[connection.ConnectionId];
                await connection.InvokeAsync("SendMessage", username, message);
            }
        }
    }
}

// Hub: https://chatsample20231024085542.azurewebsites.net/chat
//Collect UserName

//Leaving it open, it is commited under Development-bm
