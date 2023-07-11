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
using System.Globalization;

namespace Gestion_des_Collaborateurs.Controllers
{
    public class CollaborateursController : Controller
    {
        private readonly GestionCollaborateursContext _context;

        public CollaborateursController(GestionCollaborateursContext context)
        {
            _context = context;
        }
        
        
       

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
        public async Task<IActionResult> Create([Bind("IdCollaborateur,Nom,Prenom,DateEmbauche,Anciennete,DateDebutEssai,DateFinEssai,salaire,DateNaissance")] Collaborateur collaborateur)
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
        public async Task<IActionResult> Edit(int id, [Bind("IdCollaborateur,Nom,Prenom,DateEmbauche,Anciennete,DateDebutEssai,DateFinEssai,salaire,DateNaissance")] Collaborateur collaborateur)
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
            // Retrieve the associated records in collab_formation
            var collabFormations = _context.PasserFormations.Where(cf => cf.IdCollaborateur == id);
            var collabCertifications = _context.AvoirCertifications.Where(cc => cc.IdCollaborateur == id);

            // Remove the associated collab_formation records
          /*  _context.Collaborateurs.RemoveRange(collabFormations);*/

            // Remove the associated collab_certification records
            _context.AvoirCertifications.RemoveRange(collabCertifications);

            // Remove the associated records


            // Remove the collaborator
            _context.Collaborateurs.Remove(collaborateur);


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult AssignFormationsCertificates(string formationName, string certificateName, List<int> selectedCollaborateurs)
        {
            // Validate the selected collaborators
            if (selectedCollaborateurs == null || !selectedCollaborateurs.Any())
            {
                return BadRequest("Invalid selected collaborators.");
            }

            if (!string.IsNullOrEmpty(formationName))
            {

                // Check if the formation is already assigned to any of the selected collaborators
                bool isFormationAssigned = _context.PasserFormations.Any(cf => selectedCollaborateurs.Contains(cf.IdCollaborateur.Value) && cf.IdFormationNavigation.NomFormation == formationName);
                if (isFormationAssigned)
                {
/*                    TempData["ErrorMessage"] = "Formation is already assigned to one or more selected collaborators.";
*/                    return RedirectToAction("/Home/Index");
                }



                // Retrieve the formation from the database by name
                var formation = _context.Formations.FirstOrDefault(f => f.NomFormation == formationName);
                if (formation == null)
                {
                    return NotFound("Formation not found.");
                }

                // Iterate over the selected collaborators and save the assignments
                foreach (var collaborateurId in selectedCollaborateurs)
                {
                    var collaborateur = _context.Collaborateurs.FirstOrDefault(c => c.IdCollaborateur == collaborateurId);
                    if (collaborateur != null)
                    {
                        // Create a new CollabFormation instance
                        var collabFormation = new PasserFormation
                        {
                            IdCollaborateur = collaborateurId,
                            IdFormation = formation.IdFormation
                        };

                        // Add the CollabFormation to the database
                        _context.PasserFormations.Add(collabFormation);
                    }
                }

                // Save the changes to the database
                _context.SaveChanges();
/*                TempData["SuccessMessage"] = "Formation assigned successfully.";
*/
                // Redirect to the Collaborateurs Index after assigning the formation
                return RedirectToAction("Index");
            }
            else if (!string.IsNullOrEmpty(certificateName))
            {
                // Check if the certificate is already assigned to any of the selected collaborators
                bool isCertificateAssigned = _context.AvoirCertifications.Any(cc => selectedCollaborateurs.Contains(cc.IdCollaborateur.Value) && cc.IdCertificationNavigation.NomCertification == certificateName);
                if (isCertificateAssigned)
                {
/*                    TempData["ErrorMessage"] = "Certificate is already assigned to one or more selected collaborators.";
*/                    return RedirectToAction("Index");
                }

                // Retrieve the certificate from the database by name
                var certificate = _context.Certifications.FirstOrDefault(c => c.NomCertification == certificateName);
                if (certificate == null)
                {
                    return NotFound("Certificate not found.");
                }

                // Iterate over the selected collaborators and save the assignments
                foreach (var collaborateurId in selectedCollaborateurs)
                {
                    var collaborateur = _context.Collaborateurs.FirstOrDefault(c => c.IdCollaborateur == collaborateurId);
                    if (collaborateur != null)
                    {
                        string certificateDateObtentionString = Request.Form["certificateDateObtention"];
                        if (!DateTime.TryParseExact(certificateDateObtentionString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime certificateDateObtention))
                        {
                            return BadRequest("Invalid date format for certificateDateObtention. Please enter the date in the format 'yyyy-MM-dd'.");
                        }
                        // Create a new CollabCertificate instance
                        var CollabCertification = new AvoirCertification
                        {
                            IdCollaborateur = collaborateurId,
                            IdCertification = certificate.IdCertification,
                            DatePassage = certificateDateObtention,
                        };

                        // Add the CollabCertificate to the database
                        _context.AvoirCertifications.Add(CollabCertification);
                    }
                }

                // Save the changes to the database
                _context.SaveChanges();
                TempData["SuccessMessage"] = "certificat assigned successfully.";


                // Redirect to the Collaborateurs Index after assigning the certificate
                return RedirectToAction("Index");
            }

            return BadRequest("Invalid assign type.");
        }

        [HttpGet]
        public IActionResult GetFormationNames()
        {
            var NomsFormations = _context.Formations.Select(f => f.NomFormation).ToList();
            return Json(NomsFormations);
        }

        [HttpGet]
        public IActionResult GetCertificateNames()
        {
            var NomsCertification = _context.Certifications.Select(c => c.NomCertification).ToList();
            return Json(NomsCertification);
        }
        private bool CollaborateurExists(int id)
        {
          return (_context.Collaborateurs?.Any(e => e.IdCollaborateur == id)).GetValueOrDefault();
        }
    }
}
