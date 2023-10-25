using System.Collections.Generic;

namespace ChatSample
{
    public class Reaction
    {
        public string Emoji { get; set; }
        public int Count => UsersReacted.Count;
        public HashSet<string> UsersReacted { get; set; } = new HashSet<string>();
    }
}
