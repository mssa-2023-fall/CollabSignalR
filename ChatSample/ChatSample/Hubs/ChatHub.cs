using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string usersName, string usersText)
        {
            Message message = new Message() {
                Creator = usersName,
                Text = usersText
            };
            // Call the broadcastMessage method to update clients.

            await Clients.All.SendAsync("broadcastMessage", message);
        }
    }
}