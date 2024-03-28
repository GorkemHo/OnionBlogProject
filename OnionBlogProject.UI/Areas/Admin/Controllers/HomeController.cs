using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnionBlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles ="member")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
