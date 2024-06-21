using Microsoft.AspNetCore.Mvc;

namespace RealTime_ChatApp.Controllers
{
    public class PrivateMessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
