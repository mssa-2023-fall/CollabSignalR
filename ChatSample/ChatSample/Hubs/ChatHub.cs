using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        private static List<Message> Messages = new List<Message>();

        public async void SendMessage(string user, string messageText)
        {
            var message = new Message
            {
                Creator = user,
                Text = messageText
            };

            Messages.Add(message);

            await Clients.All.SendAsync("broadcastMessage", user, messageText, message.ID);
        }

        public async void AddReaction(string messageId, string emoji, string user)
        {
            var message = Messages.FirstOrDefault(m => m.ID == messageId);
            if (message != null)
            {
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
                    // user has already reacted with this emoji
                    reaction.UsersReacted.Remove(user);
                }
                else
                {
                    reaction.UsersReacted.Add(user);
                }

                await Clients.All.SendAsync("updateReactions", messageId, emoji, reaction.Count);
            }
        }
    }
}