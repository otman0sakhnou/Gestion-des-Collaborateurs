using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gestion_des_Collaborateurs.Models;

public partial class Collaborateur
{
    public int IdCollaborateur { get; set; }
    [Required]

    public string? Nom { get; set; }
    [Required]

    public string? Prenom { get; set; }
    [DataType(DataType.Date)]
    [Required]
    public DateTime? DateEmbauche { get; set; }

    [DataType(DataType.Text, ErrorMessage = "must be a text")]
    public string? Anciennete { get; set; }
    [DataType(DataType.Date)]

    public DateTime? DateDebutEssai { get; set; }
    [DataType(DataType.Date)]

    public DateTime? DateFinEssai { get; set; }

    public decimal? salaire { get; set; }
    [DataType(DataType.Date)]

    public DateTime? DateNaissance { get; set; }

    public virtual ICollection<AvoirCertification> AvoirCertifications { get; set; } = new List<AvoirCertification>();

    public virtual ICollection<PasserFormation> PasserFormations { get; set; } = new List<PasserFormation>();
}
