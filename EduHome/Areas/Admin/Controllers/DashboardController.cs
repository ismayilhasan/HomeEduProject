using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.Admin.Controllers
{
    public class DashboardController : BaseController   
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
