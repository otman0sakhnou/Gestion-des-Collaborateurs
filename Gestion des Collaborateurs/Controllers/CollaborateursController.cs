using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestion_des_Collaborateurs.Data;
using Gestion_des_Collaborateurs.Models;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace Gestion_des_Collaborateurs.Controllers
{
    public class CollaborateursController : Controller
    {
        private readonly GestionCollaborateursContext _context;

        public CollaborateursController(GestionCollaborateursContext context)
        {
            _context = context;
        }
        //Get :le nombre des collaborateurs
        int nombreColl;
       

        // GET: Collaborateurs
        public async Task<IActionResult> Index()
        {
            return _context.Collaborateurs != null ? 
                          View(await _context.Collaborateurs.ToListAsync()) :
                          Problem("Entity set 'GestionCollaborateursContext.Collaborateurs'  is null.");
        }

        // GET: Collaborateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Collaborateurs == null)
            {
                return NotFound();
            }

            var collaborateur = await _context.Collaborateurs
                .FirstOrDefaultAsync(m => m.IdCollaborateur == id);
            if (collaborateur == null)
            {
                return NotFound();
            }

            return View(collaborateur);
        }

        // GET: Collaborateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collaborateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCollaborateur,Nom,Prenom,DateEmbauche,Anciennete,DateDebutEssai,DateFinEssai,DateNaissance,salaire")] Collaborateur collaborateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collaborateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collaborateur);
        }

        // GET: Collaborateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collaborateurs == null)
            {
                return NotFound();
            }

            var collaborateur = await _context.Collaborateurs.FindAsync(id);
            if (collaborateur == null)
            {
                return NotFound();
            }
            return View(collaborateur);
        }

        // POST: Collaborateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCollaborateur,Nom,Prenom,DateEmbauche,Anciennete,DateDebutEssai,DateFinEssai,DateNaissance,salaire")] Collaborateur collaborateur)
        {
            if (id != collaborateur.IdCollaborateur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collaborateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaborateurExists(collaborateur.IdCollaborateur))
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
            return View(collaborateur);
        }

        // GET: Collaborateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Collaborateurs == null)
            {
                return NotFound();
            }

            var collaborateur = await _context.Collaborateurs
                .FirstOrDefaultAsync(m => m.IdCollaborateur == id);
            if (collaborateur == null)
            {
                return NotFound();
            }

            return View(collaborateur);
        }

        // POST: Collaborateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Collaborateurs == null)
            {
                return Problem("Entity set 'GestionCollaborateursContext.Collaborateurs'  is null.");
            }
            var collaborateur = await _context.Collaborateurs.FindAsync(id);
            if (collaborateur != null)
            {
                _context.Collaborateurs.Remove(collaborateur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaborateurExists(int id)
        {
          return (_context.Collaborateurs?.Any(e => e.IdCollaborateur == id)).GetValueOrDefault();
        }
    }
}
