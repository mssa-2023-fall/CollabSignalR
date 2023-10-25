using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    // Define the ChatHub class which inherits from the SignalR Hub class
    public class ChatHub : Hub
    {
        // Static list to store messages for all connected clients
        private static List<Message> Messages = new List<Message>();

        // Asynchronous method to send a message
        public async Task SendMessage(string user, string messageText)
        {
            // Create a new message object
            var message = new Message
            {
                ID = System.Guid.NewGuid().ToString(), // Assign a unique ID to the message
                Creator = user, // Set the creator of the message
                Text = messageText // Set the text of the message
            };

            // Add the created message to the list of messages
            Messages.Add(message);

            // Broadcast the message to all connected clients
            await Clients.All.SendAsync("broadcastMessage", user, message, message.ID);
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
                // Find the reaction with the given emoji for the message
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
    }
}