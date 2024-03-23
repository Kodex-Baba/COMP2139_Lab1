using COMP2139_Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace COMP2139_Lab1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult GeneralSearch(string searchType, string searchString)
        {
            if(searchType == "Projects")
            {
                return RedirectToAction("Search", "Project", new {area = "ProjectManagement", searchString});
            }
            else if (searchType == "Tasks")
            {
                // var url = Url.Action("Search", "Task", new { searchString });
                return RedirectToAction("Search", "Task", new { area = "ProjectManagement", searchString });
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NotFound( int statusCode)
        {
            if(statusCode == 404)
            {
                return View("NotFound");
            }
            
            
                return View("Error");
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}