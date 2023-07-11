using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_des_Collaborateurs.Models;

public partial class Formation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdFormation { get; set; }

    public string? NomFormation { get; set; }

    public DateTime? DateDebutFormation { get; set; }

    public DateTime? DateFinFormation { get; set; }

    public virtual ICollection<PasserFormation> PasserFormations { get; set; } = new List<PasserFormation>();
}
