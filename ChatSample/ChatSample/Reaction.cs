using System.Collections.Generic;

namespace ChatSample {
    public class Reaction {
        public string Emoji { get; set; }
        public int Count { get; set; }
        public HashSet<string> UsersReacted { get; set; }
    }
}
