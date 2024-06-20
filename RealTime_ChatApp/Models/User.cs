using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RealTime_ChatApp.Models
{
    public class User : IdentityUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
