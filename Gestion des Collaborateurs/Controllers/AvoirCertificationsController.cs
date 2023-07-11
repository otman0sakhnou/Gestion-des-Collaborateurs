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
    public class AvoirCertificationsController : Controller
    {
        private readonly GestionCollaborateursContext _context;

        public AvoirCertificationsController(GestionCollaborateursContext context)
        {
            _context = context;
        }

        // GET: AvoirCertifications
        public async Task<IActionResult> Index()
        {
            var gestionCollaborateursContext = _context.AvoirCertifications.Include(a => a.IdCertificationNavigation).Include(a => a.IdCollaborateurNavigation);
            return View(await gestionCollaborateursContext.ToListAsync());
        }

        // GET: AvoirCertifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AvoirCertifications == null)
            {
                return NotFound();
            }

            var avoirCertification = await _context.AvoirCertifications
                .Include(a => a.IdCertificationNavigation)
                .Include(a => a.IdCollaborateurNavigation)
                .FirstOrDefaultAsync(m => m.IdCollaborateur == id);
            if (avoirCertification == null)
            {
                return NotFound();
            }

            return View(avoirCertification);
        }

        // GET: AvoirCertifications/Create
        public IActionResult Create()
        {
            ViewData["IdCertification"] = new SelectList(_context.Certifications, "IdCertification", "IdCertification");
            ViewData["IdCollaborateur"] = new SelectList(_context.Collaborateurs, "IdCollaborateur", "IdCollaborateur");
            return View();
        }

        // POST: AvoirCertifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCollaborateur,IdCertification,DatePassage,DuréeObtention")] AvoirCertification avoirCertification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avoirCertification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCertification"] = new SelectList(_context.Certifications, "IdCertification", "IdCertification", avoirCertification.IdCertification);
            ViewData["IdCollaborateur"] = new SelectList(_context.Collaborateurs, "IdCollaborateur", "IdCollaborateur", avoirCertification.IdCollaborateur);
            return View(avoirCertification);
        }

        // GET: AvoirCertifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AvoirCertifications == null)
            {
                return NotFound();
            }

            var avoirCertification = await _context.AvoirCertifications.FindAsync(id);
            if (avoirCertification == null)
            {
                return NotFound();
            }
            ViewData["IdCertification"] = new SelectList(_context.Certifications, "IdCertification", "IdCertification", avoirCertification.IdCertification);
            ViewData["IdCollaborateur"] = new SelectList(_context.Collaborateurs, "IdCollaborateur", "IdCollaborateur", avoirCertification.IdCollaborateur);
            return View(avoirCertification);
        }

        // POST: AvoirCertifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCollaborateur,IdCertification,DatePassage,DuréeObtention")] AvoirCertification avoirCertification)
        {
            if (id != avoirCertification.IdCollaborateur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avoirCertification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvoirCertificationExists((int)avoirCertification.IdCollaborateur))
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
            ViewData["IdCertification"] = new SelectList(_context.Certifications, "IdCertification", "IdCertification", avoirCertification.IdCertification);
            ViewData["IdCollaborateur"] = new SelectList(_context.Collaborateurs, "IdCollaborateur", "IdCollaborateur", avoirCertification.IdCollaborateur);
            return View(avoirCertification);
        }

        // GET: AvoirCertifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AvoirCertifications == null)
            {
                return NotFound();
            }

            var avoirCertification = await _context.AvoirCertifications
                .Include(a => a.IdCertificationNavigation)
                .Include(a => a.IdCollaborateurNavigation)
                .FirstOrDefaultAsync(m => m.IdCollaborateur == id);
            if (avoirCertification == null)
            {
                return NotFound();
            }

            return View(avoirCertification);
        }

        // POST: AvoirCertifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AvoirCertifications == null)
            {
                return Problem("Entity set 'GestionCollaborateursContext.AvoirCertifications'  is null.");
            }
            var avoirCertification = await _context.AvoirCertifications.FindAsync(id);
            if (avoirCertification != null)
            {
                _context.AvoirCertifications.Remove(avoirCertification);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvoirCertificationExists(int id)
        {
          return (_context.AvoirCertifications?.Any(e => e.IdCollaborateur == id)).GetValueOrDefault();
        }
    }
}
