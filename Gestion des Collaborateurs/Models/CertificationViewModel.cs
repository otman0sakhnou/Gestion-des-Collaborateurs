using System.ComponentModel.DataAnnotations;

namespace Gestion_des_Collaborateurs.Models
{
    public class CertificationViewModel
    {
        public string? NomCertificat { get; set; }
        public List<string>? NomCollaborateurs { get; set; }
        public List<string>? PrenomCollaborateurs { get; internal set; }

        [Display(Name = "Date d'obtention")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public List<DateTime>? DatePassage { get; set; }
        public List<string>? DuréeObtention { get; set; }
    }
}
