using System;
using System.Collections.Generic;

namespace Gestion_des_Collaborateurs.Models;

public partial class PasserFormation
{
    public int IdCollaborateur { get; set; }

    public int IdFormation { get; set; }

    public int IdFormateur { get; set; }

    public virtual Collaborateur IdCollaborateurNavigation { get; set; } = null!;

    public virtual Formation IdFormationNavigation { get; set; } = null!;
}
