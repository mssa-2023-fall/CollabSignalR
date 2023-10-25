using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        private static List<Message> Messages = new List<Message>();

        // Asynchronous method to send a message
        public async Task SendMessage(string user, string messageText)
        {
            // Create a new message object
            var message = new Message
            {
                ID = System.Guid.NewGuid().ToString(), // Assign a unique ID to the message
                Creator = user, // Set the creator of the message
                Text = messageText // Set the text of the message new
            };

            // Add the created message to the list of messages
            Messages.Add(message);

            // Broadcast the message to all connected clients
            await Clients.All.SendAsync("broadcastMessage", user, message.Text, message.ID);
        }

        // Asynchronous method to add a reaction to a message
        public async Task AddReaction(string messageId, string emoji, string user)
        {
            // Log the reaction details for debugging purposes
            Console.WriteLine($"Adding reaction: MessageId={messageId}, Emoji={emoji}, User={user}");

            // Find the message with the given ID
            var message = Messages.FirstOrDefault(m => m.ID == messageId);

            // If the message exists
            if (message != null)
            {
                // Find the reaction with the given emoji for the message test
                var reaction = message.Reactions.FirstOrDefault(r => r.Emoji == emoji);

                // If the reaction doesn't exist
                if (reaction == null)
                {
                    // Create a new reaction object
                    reaction = new Reaction
                    {
                        Emoji = emoji
                    };

                    // Add the created reaction to the message's reactions list
                    message.Reactions.Add(reaction);
                }

                // If the user has already reacted with this emoji
                if (reaction.UsersReacted.Contains(user))
                {
                    // Remove the user from the list of users who reacted with this emoji
                    reaction.UsersReacted.Remove(user);
                }
                else
                {
                    // Add the user to the list of users who reacted with this emoji
                    reaction.UsersReacted.Add(user);
                }

                // Broadcast the updated reaction count to all connected clients
                await Clients.All.SendAsync("updateReactions", messageId, emoji, reaction.Count);

                // Log the updated reaction count for debugging purposes
                Console.WriteLine($"Sending count {reaction.Count} for emoji {emoji} for message {messageId}");
            }
        }

        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            var msg = new History.Message { Timestamp = DateTime.Now, Sender = name, TextMessage = message };
            MessageHistory.Add(msg);
            await Clients.All.SendAsync("broadcastMessage", name, message);

        }
        public async Task RetreiveMessageHistory(int messageCount)
        {
            var history = MessageHistory.messages.Take(messageCount).Select(m => m.Value.ToString()).ToArray();
            await Clients.Caller.SendAsync("messageHistory", history);
        }
    }
    }
namespace History
{
    internal class Message : IComparable<Message>
    {
        internal DateTime Timestamp { get; set; }
        internal string Sender { get; set; }
        internal string TextMessage { get; set; }

        public int CompareTo(Message other)
        {
            return -this.Timestamp.CompareTo(other.Timestamp);
        }

        public override string ToString()
        {
            return $"{Timestamp.ToString("yyyy-MM-dd HH:mm z")} - {Sender} : {TextMessage}";

        }
    }

}