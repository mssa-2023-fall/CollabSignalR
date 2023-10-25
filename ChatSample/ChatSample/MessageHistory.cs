using ChatSample.Hubs;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChatSample
{
    internal static class MessageHistory
    {
        static object locker;
        internal readonly static SortedList<Message, Message> messages;
        private static SortedList<Message, Message> _messages = new SortedList<Message, Message>();
        static MessageHistory()
        {
            messages = _messages;
        }
        internal static void Add(Message message)
        {
            lock(locker) {
                _messages.Add(message, message);
            }
            
        }
    }
}
