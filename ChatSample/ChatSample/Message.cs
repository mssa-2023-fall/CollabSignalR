using System;
using System.Collections.Generic;

namespace ChatSample {
    public class Message {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string Creator { get; set; }
        public string Text { get; set; }
        public List<Reaction> Reactions { get; set; } = new List<Reaction>();
    }
}
