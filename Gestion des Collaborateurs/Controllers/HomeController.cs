using Gestion_des_Collaborateurs.Data;
using Gestion_des_Collaborateurs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Gestion_des_Collaborateurs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GestionCollaborateursContext _context;


        public HomeController(ILogger<HomeController> logger, GestionCollaborateursContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.nombreColl = await _context.Collaborateurs.CountAsync();
            ViewBag.nombreFormation= await _context.Formations.CountAsync();
            ViewBag.nombreCertif= await _context.Certifications.CountAsync();

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