using System.Collections.Generic;

<<<<<<< HEAD
namespace ChatSample {
    public class Reaction {
        public string Emoji { get; set; }
        public int Count { get; set; }
=======
namespace ChatSample
{
    public class Reaction
    {
        public string Emoji { get; set; }
        public int Count => UsersReacted.Count;
>>>>>>> emojiDev_John
        public HashSet<string> UsersReacted { get; set; } = new HashSet<string>();
    }
}
