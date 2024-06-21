using System.ComponentModel.DataAnnotations;

namespace RealTime_ChatApp.Models
{
    public class Message
    {
        public string MessageId { get; set; }
        public string UserId { get; set; }
        public string ChannelId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
