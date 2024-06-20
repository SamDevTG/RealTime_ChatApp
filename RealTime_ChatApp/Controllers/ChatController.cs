﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTime_ChatApp.Hubs;

public class ChatController : Controller
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public IActionResult Index()
    {
        return View();
    }
}
