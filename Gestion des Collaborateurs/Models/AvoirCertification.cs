using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_des_Collaborateurs.Models;

public partial class AvoirCertification
{
    
    public int? IdCollaborateur { get; set; }

    public int IdCertification { get; set; }
    [DataType(DataType.Date)]
    [Required(ErrorMessage ="champs obligatoire")]

    public DateTime? DatePassage { get; set; }
    [Required(ErrorMessage = "champs obligatoire")]

    public string? DuréeObtention { get; set; }

    public virtual Certification IdCertificationNavigation { get; set; } = null!;

    public virtual Collaborateur IdCollaborateurNavigation { get; set; } = null!;
}
