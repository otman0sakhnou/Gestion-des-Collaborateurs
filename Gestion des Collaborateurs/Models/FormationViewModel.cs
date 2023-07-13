using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_des_Collaborateurs.Models
{
    public class FormationViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFormateur { get; set; }
        public string? NomFormation { get; set; }
        public string? NomFormateur { get; set; }
        public List<string>? NomCollaborateurs { get; set; }
        public List<string>? PrenomCollaborateurs { get; internal set; }
        public List<DateTime>? DateDebut { get;  set; }
        public List<DateTime>? DateFin { get;  set; }

    }
}
