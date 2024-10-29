using Microsoft.AspNetCore.Mvc;

namespace LogInOut.Controllers
{
    public class MyInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
