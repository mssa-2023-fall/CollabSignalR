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

        public async Task Send(string name, string message)
        {
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public async Task SendMessage(string user, string messageText)
        {
            var message = new Message
            {
                ID = Guid.NewGuid().ToString(),
                Creator = user,
                Text = messageText
            };

            Messages.Add(message);

            await Clients.All.SendAsync("broadcastMessage", user, message.Text, message.ID);
        }

        public async Task AddReaction(string messageId, string emoji, string user)
        {
            Console.WriteLine($"Adding reaction: MessageId={messageId}, Emoji={emoji}, User={user}");

            var message = Messages.FirstOrDefault(m => m.ID == messageId);

            if (message != null)
            {
                // Find the reaction with the given emoji for the message test
                var reaction = message.Reactions.FirstOrDefault(r => r.Emoji == emoji);

                if (reaction == null)
                {
                    reaction = new Reaction
                    {
                        Emoji = emoji
                    };

                    message.Reactions.Add(reaction);
                }

                if (reaction.UsersReacted.Contains(user))
                {
                    reaction.UsersReacted.Remove(user);
                }
                else
                {
                    reaction.UsersReacted.Add(user);
                }

                await Clients.All.SendAsync("updateReactions", messageId, emoji, reaction.Count);

                Console.WriteLine($"Sending count {reaction.Count} for emoji {emoji} for message {messageId}");
            }
        }

        public async Task SendReactionSupported(string usersName, string usersText)
        {
            var message = new Message
            {
                Creator = usersName,
                Text = usersText
            };

            await Clients.All.SendAsync("BroadcastReactionSupportedMessage", message);
        }

    }
}
