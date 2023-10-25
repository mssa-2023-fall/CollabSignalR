using ChatSample.Hubs;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChatSample
{
    internal static class MessageHistory
    {
        static object locker=new object();
        internal readonly static SortedList<History.Message, History.Message> messages;
        private static SortedList<History.Message, History.Message> _messages = new SortedList<History.Message, History.Message>();
        static MessageHistory()
        {
            messages = _messages;
        }
        internal static void Add(History.Message message)
        {
            lock(locker) {
                _messages.Add(message, message);
            }
            
        }
    }
}
