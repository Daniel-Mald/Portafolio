using Microsoft.AspNetCore.Mvc;

namespace Portafolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Paint()
        {
            return View();
        }
        public async Task<IActionResult> VotePaint()
        {
            return View();
        }
        
    }
}
