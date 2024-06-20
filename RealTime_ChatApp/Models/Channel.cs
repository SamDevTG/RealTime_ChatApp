namespace RealTime_ChatApp.Models
{
    public class Channel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
