using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {

       
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            var msg = new Message() { Timestamp = DateTime.Now, Sender = name, TextMessage = message };
            MessageHistory.messages.Add(msg, msg);
            await Clients.All.SendAsync("broadcastMessage", name, message);

        }
        public async Task RetreiveMessageHistory(int messageCount)
        {
            var history = MessageHistory.messages.Take(messageCount).Select(m => m.Value.ToString()).ToArray();
            await Clients.Caller.SendAsync("messageHistory", history);
        }
    }
    internal class Message:IComparable<Message>
    {
        internal DateTime Timestamp { get; set; }
        internal string Sender { get; set; }
        internal string TextMessage { get; set; }

        public int CompareTo(Message other)
        {
            return this.Timestamp.CompareTo(other.Timestamp);
        }

        public override string ToString()
        {
            return $"{Timestamp.ToString("yyyy-MM-dd HH:mm z")} - {Sender} : {TextMessage}";

        }
    }
}