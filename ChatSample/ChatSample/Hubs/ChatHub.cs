using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
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

        public async Task AddEmojiCountAsync(string usersName, string emojiValue, Message message) {
            // if not already in reactions add it
            if (!message.Reactions.Any(x => x.Emoji == emojiValue)) {
                message.Reactions.Add(new Reaction() { Emoji = emojiValue, Count = 0 });
            }

            // find reaction
            var reactionIndex = message.Reactions.IndexOf(message.Reactions.Find(x => x.Emoji == emojiValue));

            // if user already reacted this emoji remove it, else add
            if (message.Reactions[reactionIndex].UsersReacted.Contains(usersName)) {
                message.Reactions[reactionIndex].UsersReacted.Remove(usersName);
                message.Reactions[reactionIndex].Count--;
                if (message.Reactions[reactionIndex].Count == 0) {
                    message.Reactions.RemoveAt(reactionIndex);
                }
            } else {
                message.Reactions[reactionIndex].Count++;
            }

            // update everyones emoji count
            await Clients.All.SendAsync("UpdateMessageReactions", message.ID, message.Reactions);
        }
    }
}