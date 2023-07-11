using Microsoft.Identity.Client;

namespace Gestion_des_Collaborateurs.Models
{
    public class FormationViewModel
    {
        public string? NomFormation { get; set; }
        
        public string? NomFormateur { get; set; }
        public List<string>? NomCollaborateurs { get; set; }
        public List<string>? PrenomCollaborateurs { get; internal set; }

    }
}
