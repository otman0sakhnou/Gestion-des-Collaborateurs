using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_des_Collaborateurs.Models;

public partial class Certification
{
    [Required(ErrorMessage ="saisie d'Id est obligatoire")]
    [Key]
    public int IdCertification { get; set; }
    [MaxLength(50, ErrorMessage = "le champs de doit pas passer 50 caractères")]

    [Required(ErrorMessage ="la saisie de nom du certification est obligatoire")]
    public string? NomCertification { get; set; }
    [Required(ErrorMessage = "la saisie du durée du certification est obligatoire")]
    public string? Durée { get; set; }

    [Required(ErrorMessage = "la saisie du Coût du certif est obligatoire")]
    public decimal? Coût { get; set; }

    public virtual ICollection<AvoirCertification> AvoirCertifications { get; set; } = new List<AvoirCertification>();
}
