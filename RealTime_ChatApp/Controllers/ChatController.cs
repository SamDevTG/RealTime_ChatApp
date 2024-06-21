using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTime_ChatApp.Models;
using RealTime_ChatApp.Services;
using RealTime_ChatApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;

public class ChatController : Controller
{
    private readonly IChatService _chatService;
    private readonly UserManager<User> _userManager;

    public ChatController(IChatService chatService, UserManager<User> userManager)
    {
        _chatService = chatService;
        _userManager = userManager;
    }

    [Authorize]
    public async Task<IActionResult> StartPrivateChat(string username)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var otherUser = await _userManager.FindByNameAsync(username);

        if (otherUser == null)
        {
            // Handle error: user not found
            return RedirectToAction("Index", "Home");
        }

        var conversation = await _chatService.StartPrivateConversationAsync(currentUser, otherUser);

        return RedirectToAction("PrivateChat", new { conversationId = conversation.ConversationId });
    }

    [Authorize]
    public async Task<IActionResult> PrivateChat(string conversationId)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var messages = await _chatService.GetMessagesAsync(conversationId);

        if (messages == null || !messages.Any(m => m.UserId == currentUser.Id))
        {
            return RedirectToAction("Index", "Home");
        }

        var model = new PrivateChatViewModel
        {
            ConversationId = conversationId,
            Messages = messages.ToList() 
        };
        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SendMessage(string conversationId, string messageContent)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        await _chatService.SendMessageAsync(currentUser, conversationId, messageContent);

        return RedirectToAction("PrivateChat", new { conversationId = conversationId });
    }
}
