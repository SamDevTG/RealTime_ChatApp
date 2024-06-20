namespace RealTime_ChatApp.Models
{
    public class UserChannel
    {
        public required Guid UserId { get; set; }
        public required Guid ChannelId { get; set; }
    }
}
