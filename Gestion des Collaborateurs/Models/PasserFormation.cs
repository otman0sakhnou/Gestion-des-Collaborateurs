using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_des_Collaborateurs.Models;

public partial class PasserFormation
{

    public int? IdCollaborateur { get; set; }

    public int IdFormation { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int  IdFormateur { get; set; }
    public string? NomFormateur { get; set; }

    public virtual Collaborateur? IdCollaborateurNavigation { get; set; } = null!;

    public virtual Formation? IdFormationNavigation { get; set; } = null!;

 
}
