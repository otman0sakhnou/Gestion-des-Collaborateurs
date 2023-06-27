using System;
using System.Collections.Generic;

namespace Gestion_des_Collaborateurs.Models;

public partial class Collaborateur
{
    public int IdCollaborateur { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public DateTime? DateEmbauche { get; set; }

    public string? Anciennete { get; set; }

    public DateTime? DateDebutEssai { get; set; }

    public DateTime? DateFinEssai { get; set; }

    public virtual ICollection<AvoirCertification> AvoirCertifications { get; set; } = new List<AvoirCertification>();

    public virtual ICollection<PasserFormation> PasserFormations { get; set; } = new List<PasserFormation>();
}
