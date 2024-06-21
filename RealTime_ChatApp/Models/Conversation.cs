// Conversation.cs
using System;
using System.Collections.Generic;

namespace RealTime_ChatApp.Models
{
    public class Conversation
    {
        public string ConversationId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Participants { get; set; } 
    }
}
