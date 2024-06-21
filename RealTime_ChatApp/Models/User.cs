using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace RealTime_ChatApp.Models
{
    public class User : IdentityUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<UserChannel> UserChannels { get; set; }
        public ICollection<Conversation> Conversations { get; set; } // Relacionamento para conversas privadas
    }
}