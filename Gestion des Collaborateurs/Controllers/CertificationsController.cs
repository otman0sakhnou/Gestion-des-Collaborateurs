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
    public class CertificationsController : Controller
    {
        private readonly GestionCollaborateursContext _context;

        public CertificationsController(GestionCollaborateursContext context)
        {
            _context = context;
        }

        // GET: Certifications
        public async Task<IActionResult> Index()
        {
              return _context.Certifications != null ? 
                          View(await _context.Certifications.ToListAsync()) :
                          Problem("Entity set 'GestionCollaborateursContext.Certifications'  is null.");
        }

        // GET: Certifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Certifications == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications
                .FirstOrDefaultAsync(m => m.IdCertification == id);
            if (certification == null)
            {
                return NotFound();
            }

            return View(certification);
        }

        // GET: Certifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCertification,NomCertification,Durée,Coût")] Certification certification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(certification);
        }

        // GET: Certifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Certifications == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications.FindAsync(id);
            if (certification == null)
            {
                return NotFound();
            }
            return View(certification);
        }

        // POST: Certifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCertification,NomCertification,Durée,Coût")] Certification certification)
        {
            if (id != certification.IdCertification)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificationExists(certification.IdCertification))
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
            return View(certification);
        }

        // GET: Certifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Certifications == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications
                .FirstOrDefaultAsync(m => m.IdCertification == id);
            if (certification == null)
            {
                return NotFound();
            }

            return View(certification);
        }

        // POST: Certifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Certifications == null)
            {
                return Problem("Entity set 'GestionCollaborateursContext.Certifications'  is null.");
            }
            var certification = await _context.Certifications.FindAsync(id);
            if (certification != null)
            {
                _context.Certifications.Remove(certification);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificationExists(int id)
        {
          return (_context.Certifications?.Any(e => e.IdCertification == id)).GetValueOrDefault();
        }
    }
}
