using RealTime_ChatApp.Models;

namespace RealTime_ChatApp.Models
{
    public class Channel
    {
        public string ChannelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<UserChannel> UserChannels { get; set; }
    }
}
