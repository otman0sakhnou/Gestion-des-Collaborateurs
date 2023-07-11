using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_des_Collaborateurs.Models;

public partial class Collaborateur
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCollaborateur { get; set; }



    [Required(ErrorMessage = "la saisie de votre nom est obligatoire")]
    public string? Nom { get; set; }
    [Required(ErrorMessage = "la saisie de votre prénom est obligatoire")]

    public string? Prenom { get; set; }
    [DataType(DataType.Date)]
    [Required(ErrorMessage ="la saisie de votre date d'embauchment est obligatoire")]
    public DateTime? DateEmbauche { get; set; }

    [Required(ErrorMessage ="la saisie de votre anciennete est obligatoire")]
    public string? Anciennete { get; set; }
    [DataType(DataType.Date,ErrorMessage ="la date du debut d'essai doit etre une date valide")]

    public DateTime? DateDebutEssai { get; set; }
    [DataType(DataType.Date, ErrorMessage = "la date du fin d'essai doit etre une date valide")]

    public DateTime? DateFinEssai { get; set; }
    [DataType(DataType.Currency)]
    [RegularExpression(@"^\d*\.?\d*$", ErrorMessage = "Format incorrecte du salaire")]
    [Required(ErrorMessage ="la saisie du votre salaire est obligatoire")]

    public decimal? salaire { get; set; }
    [DataType(DataType.Date,ErrorMessage ="la date de naissance doit etre valide")]
    [Required(ErrorMessage ="la saisie du date de naissance est obligatoire")]

    public DateTime DateNaissance { get; set; }
    
    public virtual ICollection<AvoirCertification> AvoirCertifications { get; set; } = new List<AvoirCertification>();

    public virtual ICollection<PasserFormation> PasserFormations { get; set; } = new List<PasserFormation>();
}
