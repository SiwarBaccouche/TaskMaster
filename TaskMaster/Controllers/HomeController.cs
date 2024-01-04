using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskMaster.Areas.Identity.Data;
using TaskMaster.Models;

namespace TaskMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaskMasterDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, TaskMasterDBContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Retrieve project names from the database
            var projects = _context.Project
                           .Select(p => new { p.Id, p.Name })
                           .ToList();
            // Retrieve team names from the database
            var teams = _context.Team
                            .Select(t => new { t.TeamId, t.TeamName })
                            .ToList();

            // Create a Dictionary to store ID => Name pairs
            var projectDictionary = projects.ToDictionary(p => p.Id, p => p.Name);

            // Create a Dictionary to store TeamId => TeamName pairs
            var teamDictionary = teams.ToDictionary(t => t.TeamId, t => t.TeamName);

            // Pass the team dictionary to the view
            ViewData["Teams"] = teamDictionary;
            // Pass the project dictionary to the view
            ViewData["Projects"] = projectDictionary;
            return View();
        }



        public IActionResult Dashboard()
        {
            return View(); 
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}