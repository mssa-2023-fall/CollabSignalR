using ChatSample.Hubs;
using System.Collections.Generic;

namespace ChatSample
{
    public class MessageHistory
    {
        internal static SortedList<Message, Message> messages= new ();
    }
}
