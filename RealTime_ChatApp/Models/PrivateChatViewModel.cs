using RealTime_ChatApp.Models;

namespace RealTime_ChatApp.ViewModels
{
    public class PrivateChatViewModel
    {
        public string ConversationId { get; set; }
        public List<Message> Messages { get; set; }
    }
}
