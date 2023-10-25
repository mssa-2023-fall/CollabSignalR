using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Welcome to the NerdCraft Chat");
Console.WriteLine("Press ESC key to continue");
Console.CursorVisible = false; // Hide the cursor

int screenWidth = Console.WindowWidth;
int position = 0;
string hotdog = "HOTDOG!!!";
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

var connection = new HubConnectionBuilder()
    .WithUrl("https://chatsample20231024085542.azurewebsites.net/chat")
    .Build();

try
{
    await connection.StartAsync();
    Console.WriteLine("Connection established.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error starting the connection: {ex.Message}");
    return;
}

var userDictionary = new Dictionary<string, string>(); // Dictionary to map connections to usernames

connection.On<string>("UserConnected", (connectionId) =>
{
    Console.WriteLine($"User connected with connectionId: {connectionId}");
    // Prompt the user to enter their username here
    Console.Write("Enter your username: ");
    var username = Console.ReadLine();
    //add the username to the dictionary
    //userDictionary[connectionId] = username;
    Console.WriteLine($"Welcome, {username}!");
});

connection.On<string, string>("ReceiveMessage", (user, message) =>
{
    Console.WriteLine($"{user}: {message}");
});

connection.On<string>("UserDisconnected", (connectionId) =>
{
    if (userDictionary.ContainsKey(connectionId))
    {
        var disconnectedUser = userDictionary[connectionId];
        Console.WriteLine($"{disconnectedUser} has disconnected.");
        userDictionary.Remove(connectionId);
    }
});

while (true)
{
    Console.Write("Your message: ");
    var message = Console.ReadLine();

    // Send the message with the current user's username
    if (userDictionary.ContainsKey(connection.ConnectionId))
    {
        var username = userDictionary[connection.ConnectionId];
        await connection.InvokeAsync("SendMessage", username, message);
    }
}