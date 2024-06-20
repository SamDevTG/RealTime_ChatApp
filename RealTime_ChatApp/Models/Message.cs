using System.ComponentModel.DataAnnotations;

namespace RealTime_ChatApp.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ChannelId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
