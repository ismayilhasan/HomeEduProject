using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class Event1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();  
        }
    }
}
