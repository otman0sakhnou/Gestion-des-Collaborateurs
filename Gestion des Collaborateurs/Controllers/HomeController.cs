using Azure;
using Gestion_des_Collaborateurs.Data;
using Gestion_des_Collaborateurs.Models;
using Humanizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

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



        private int calculeAge(DateTime datenaissance, DateTime dateAujourdhui)
        {
            int age = dateAujourdhui.Year - datenaissance.Year;
            if (datenaissance > dateAujourdhui.AddYears(-age))
                age--;
            return age;
        }

        private int semaineRest(DateTime datefin,DateTime datedebut)
        {   
            int semaine=datefin.DayOfWeek - datedebut.DayOfWeek;

            if (semaine<=2)
            {

                return semaine;

            }
            return semaine;
        
        }
    

        public async Task<IActionResult> Index()
        {
            ViewBag.nombreColl = await _context.Collaborateurs.CountAsync();
            ViewBag.nombreFormation= await _context.Formations.CountAsync();
            ViewBag.nombreCertif= await _context.AvoirCertifications.CountAsync();
            var DateAjourd = DateTime.Now.Date;
            ViewBag.nombrePeriodeEssai = await _context.Collaborateurs.Where(c => c.DateFinEssai > DateAjourd).CountAsync();
            ViewBag.completer = await _context.PasserFormations.Where(c => c.IdFormationNavigation.DateFinFormation < DateAjourd).CountAsync();

            ViewBag.MoyennSalaire = await _context.Collaborateurs.Select(c => c.salaire).AverageAsync();
            var coll = _context.Collaborateurs.ToList();

            var ages = coll.Select(c => new
            {
                Id = c.IdCollaborateur,
                Age = calculeAge(c.DateNaissance, DateTime.Now)
            }).ToList();
            ViewBag.MoyenAge= ages.Average(c=>c.Age);
            var diff = DateTime.Now.Day;

            var periodeEssai =_context.Collaborateurs.AsEnumerable().Where(c=>(c.DateFinEssai - DateAjourd).Value.TotalDays <14 && c.DateFinEssai > DateAjourd).Select(c => new TempsRestant
            {
                IdCollaborateur = c.IdCollaborateur,
                Nom = c.Nom,
                Prenom = c.Prenom,
                DateDebutEssai = c.DateDebutEssai,
                DateFinEssai = c.DateFinEssai,
                tempsRe = c.DateFinEssai?.Day - diff
            }).ToList();
            return View(periodeEssai) ;
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