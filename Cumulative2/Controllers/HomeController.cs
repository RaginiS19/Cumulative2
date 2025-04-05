using System.Diagnostics;
using Cumulative2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative2.Controllers
{
// HomeController handles basic pages like the home page, privacy policy, and error page.
    public class HomeController : Controller
    {
    // Constructor that accepts an ILogger<HomeController> to enable logging for this controller
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
// Index method is used to render the home page view
        public IActionResult Index()
        {
            return View();
        }
 // Privacy method renders the privacy policy view
        public IActionResult Privacy()
        {
            return View();
        }
// Error method handles and displays error pages. It uses the ResponseCache attribute to ensure the page is not cached.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
         // Returns the ErrorViewModel with a unique RequestId to help trace the error
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
