using Microsoft.EntityFrameworkCore;
using RealTime_ChatApp.Data;
using RealTime_ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTime_ChatApp.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;

        public ChatService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation> StartPrivateConversationAsync(User user1, User user2)
        {
            var conversation = new Conversation
            {
                ConversationId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                Participants = new List<User> { user1, user2 },
                Messages = new List<Message>()
            };

            _context.PrivateChat.Add(conversation);
            await _context.SaveChangesAsync();

            return conversation;
        }

        public async Task SendMessageAsync(User sender, string conversationId, string messageContent)
        {
            var conversation = await _context.PrivateChat
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.ConversationId == conversationId);

            if (conversation == null)
            {
                // Handle error: conversation not found
                return;
            }

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                UserId = sender.Id,
                ChannelId = conversationId, // Ensure conversationId is set
                Content = messageContent,
                SentAt = DateTime.Now
            };

            conversation.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserChannel>> GetChannelUsersAsync(string channelId)
        {
            return await _context.UserChannels
                .Where(uc => uc.ChannelId == channelId)
                .Include(uc => uc.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Channel>> GetUserChannelsAsync(string userId)
        {
            return await _context.Channels
                .Where(c => c.UserChannels.Any(uc => uc.UserId == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string conversationId)
        {
            return await _context.Messages
                .Where(m => m.ChannelId == conversationId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }
}
