using System;
using System.Collections.Generic;

namespace Gestion_des_Collaborateurs.Models;

public partial class Certification
{
    public int IdCertification { get; set; }

    public string? NomCertification { get; set; }

    public string? Durée { get; set; }

    public decimal? Coût { get; set; }

    public virtual ICollection<AvoirCertification> AvoirCertifications { get; set; } = new List<AvoirCertification>();
}
