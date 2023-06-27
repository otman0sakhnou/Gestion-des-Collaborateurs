using System;
using System.Collections.Generic;

namespace Gestion_des_Collaborateurs.Models;

public partial class AvoirCertification
{
    public int IdCollaborateur { get; set; }

    public int IdCertification { get; set; }

    public DateTime? DatePassage { get; set; }

    public string? DuréeObtention { get; set; }

    public virtual Certification IdCertificationNavigation { get; set; } = null!;

    public virtual Collaborateur IdCollaborateurNavigation { get; set; } = null!;
}
