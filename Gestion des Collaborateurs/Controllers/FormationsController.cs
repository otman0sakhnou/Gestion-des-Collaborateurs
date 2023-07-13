using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestion_des_Collaborateurs.Data;
using Gestion_des_Collaborateurs.Models;

namespace Gestion_des_Collaborateurs.Controllers
{
    public class FormationsController : Controller
    {
        private readonly GestionCollaborateursContext _context;

        public FormationsController(GestionCollaborateursContext context)
        {
            _context = context;
        }

        // GET: Formations
        public async Task<IActionResult> Index()
        {
              return _context.Formations != null ? 
                          View(await _context.Formations.ToListAsync()) :
                          Problem("Entity set 'GestionCollaborateursContext.Formations'  is null.");
        }

        // GET: Formations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // GET: Formations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormation,NomFormation,DateDebutFormation,DateFinFormation")] Formation formation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formation);
        }

        // GET: Formations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations.FindAsync(id);
            if (formation == null)
            {
                return NotFound();
            }
            return View(formation);
        }

        // POST: Formations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFormation,NomFormation,DateDebutFormation,DateFinFormation")] Formation formation)
        {
            if (id != formation.IdFormation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationExists(formation.IdFormation))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(formation);
        }

        // GET: Formations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // POST: Formations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Formations == null)
            {
                return Problem("Entity set 'GestionCollaborateursContext.Formations'  is null.");
            }
            var formation = await _context.Formations.FindAsync(id);
            if (formation == null)
            {
                return NotFound();
            }
            var formationColl = _context.PasserFormations.Where(cf => cf.IdFormation == id);

            // Remove the affected formations records
            _context.PasserFormations.RemoveRange(formationColl);

        


            // Remove the collaborator
            _context.Formations.Remove(formation);

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Formation supprimer avec success";
            return RedirectToAction(nameof(Index));
   
        }
        public IActionResult page1()
        {
            List<FormationViewModel> formations = new List<FormationViewModel>();

            var collabFormations = _context.PasserFormations.ToList();
            var formationIds = collabFormations.Select(cf => cf.IdFormation).Distinct().ToList();

            foreach (var formationId in formationIds)
            {
                var formationName = _context.Formations.FirstOrDefault(f => f.IdFormation == formationId)?.NomFormation;
                var collaboratorIds = collabFormations.Where(cf => cf.IdFormation == formationId).Select(cf => cf.IdCollaborateur);

                var collaboratorNames = _context.Collaborateurs
                    .Where(c => collaboratorIds.Contains(c.IdCollaborateur))
                    .Select(c => c.Nom)
                    .ToList();
                var collaboratorPrenom = _context.Collaborateurs
                    .Where(c => collaboratorIds.Contains(c.IdCollaborateur))
                    .Select(c => c.Prenom)
                    .ToList();
                formations.Add(new FormationViewModel
                {
                    NomFormation = formationName,
                    NomCollaborateurs = collaboratorNames,
                    PrenomCollaborateurs = collaboratorPrenom
                });
            }

            return View(formations);
        }
        private bool FormationExists(int id)
        {
          return (_context.Formations?.Any(e => e.IdFormation == id)).GetValueOrDefault();
        }
    }
}
