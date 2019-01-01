using Microsoft.AspNetCore.Mvc;
using RazorWebLibrary.Areas.MyFeature.Models;

namespace RazorWebLibrary.Areas.MyFeature.Controllers
{
    [Area("MyFeature")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new HomeModel
            {
                Title = "Home",
                Message = "Home Sweet Home"
            });
        }
    }
}