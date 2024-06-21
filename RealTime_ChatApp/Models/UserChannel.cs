namespace RealTime_ChatApp.Models
{
    public class UserChannel
    {
        public string UserId { get; set; }
        public string ChannelId { get; set; }
        public string Nickname { get; set; }

        public User User { get; set; }
        public Channel Channel { get; set; }
    }
}