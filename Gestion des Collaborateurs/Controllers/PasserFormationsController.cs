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
    public class PasserFormationsController : Controller
    {
        private readonly GestionCollaborateursContext _context;

        public PasserFormationsController(GestionCollaborateursContext context)
        {
            _context = context;
        }

        // GET: PasserFormations
        public async Task<IActionResult> Index()
        {
            var gestionCollaborateursContext = _context.PasserFormations.Include(p => p.IdCollaborateurNavigation).Include(p => p.IdFormationNavigation);
            return View(await gestionCollaborateursContext.ToListAsync());
        }

        // GET: PasserFormations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PasserFormations == null)
            {
                return NotFound();
            }

            var passerFormation = await _context.PasserFormations
                .Include(p => p.IdCollaborateurNavigation)
                .Include(p => p.IdFormationNavigation)
                .FirstOrDefaultAsync(m => m.IdCollaborateur == id);
            if (passerFormation == null)
            {
                return NotFound();
            }

            return View(passerFormation);   
        }

        // GET: PasserFormations/Create
        public IActionResult Create()
        {
            ViewData["IdCollaborateur"] = new SelectList(_context.Collaborateurs, "IdCollaborateur", "IdCollaborateur");
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "IdFormation");
            return View();
        }

        // POST: PasserFormations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCollaborateur,IdFormation,IdFormateur,NomFormateur")] PasserFormation passerFormation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passerFormation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCollaborateur"] = new SelectList(_context.Collaborateurs, "IdCollaborateur", "IdCollaborateur", passerFormation.IdCollaborateur);
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "IdFormation", passerFormation.IdFormation);
            return View(passerFormation);
        }

        // GET: PasserFormations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PasserFormations == null)
            {
                return NotFound();
            }

            var passerFormation = await _context.PasserFormations.FindAsync(id);
            if (passerFormation == null)
            {
                return NotFound();
            }
            ViewData["IdCollaborateur"] = new SelectList(_context.Collaborateurs, "IdCollaborateur", "IdCollaborateur", passerFormation.IdCollaborateur);
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "IdFormation", passerFormation.IdFormation);
            return View(passerFormation);
        }

        // POST: PasserFormations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCollaborateur,IdFormation,IdFormateur,NomFormateur")] PasserFormation passerFormation)
        {
            if (id != passerFormation.IdCollaborateur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passerFormation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasserFormationExists((int)passerFormation.IdCollaborateur))
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
            ViewData["IdCollaborateur"] = new SelectList(_context.Collaborateurs, "IdCollaborateur", "IdCollaborateur", passerFormation.IdCollaborateur);
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "IdFormation", passerFormation.IdFormation);
            return View(passerFormation);
        }

        // GET: PasserFormations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PasserFormations == null)
            {
                return NotFound();
            }

            var passerFormation = await _context.PasserFormations
                .Include(p => p.IdCollaborateurNavigation)
                .Include(p => p.IdFormationNavigation)
                .FirstOrDefaultAsync(m => m.IdCollaborateur == id);
            if (passerFormation == null)
            {
                return NotFound();
            }

            return View(passerFormation);
        }

        // POST: PasserFormations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PasserFormations == null)
            {
                return Problem("Entity set 'GestionCollaborateursContext.PasserFormations'  is null.");
            }
            var passerFormation = await _context.PasserFormations.FindAsync(id);
            if (passerFormation != null)
            {
                _context.PasserFormations.Remove(passerFormation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasserFormationExists(int id)
        {
          return (_context.PasserFormations?.Any(e => e.IdCollaborateur == id)).GetValueOrDefault();
        }
    }
}
