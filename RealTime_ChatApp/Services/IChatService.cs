using RealTime_ChatApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealTime_ChatApp.Services
{
    public interface IChatService
    {
        Task<Conversation> StartPrivateConversationAsync(User user1, User user2);
        Task SendMessageAsync(User sender, string conversationId, string messageContent);
        Task<IEnumerable<UserChannel>> GetChannelUsersAsync(string channelId);
        Task<IEnumerable<Channel>> GetUserChannelsAsync(string userId);
        Task<IEnumerable<Message>> GetMessagesAsync(string conversationId);
    }
}
