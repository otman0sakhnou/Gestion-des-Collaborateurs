using System;
using System.Collections.Generic;
using Gestion_des_Collaborateurs.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_des_Collaborateurs.Data;

public partial class GestionCollaborateursContext : DbContext
{
    public GestionCollaborateursContext()
    {
    }

    public GestionCollaborateursContext(DbContextOptions<GestionCollaborateursContext> options)
        : base(options) 
    {
    }

    public virtual DbSet<AvoirCertification> AvoirCertifications { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }

    public virtual DbSet<Collaborateur> Collaborateurs { get; set; }

    public virtual DbSet<Formation> Formations { get; set; }

    public virtual DbSet<PasserFormation> PasserFormations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
*/        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-RG67H8NT;Initial Catalog=GestionCollaborateurs;Integrated Security=True;TrustServerCertificate=True;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvoirCertification>(entity =>
        {
            entity.HasKey(e => new { e.IdCollaborateur, e.IdCertification }).HasName("PK__AvoirCer__34BAB85901683ABF");

            entity.ToTable("AvoirCertification");

            entity.Property(e => e.IdCollaborateur).HasColumnName("#id_collaborateur");
            entity.Property(e => e.IdCertification).HasColumnName("#id_certification");
            entity.Property(e => e.DatePassage)
                .HasColumnType("date")
                .HasColumnName("datePassage");
            entity.Property(e => e.DuréeObtention)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("duréeObtention");

            entity.HasOne(d => d.IdCertificationNavigation).WithMany(p => p.AvoirCertifications)
                .HasForeignKey(d => d.IdCertification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AvoirCert__#id_c__34C8D9D1");

            entity.HasOne(d => d.IdCollaborateurNavigation).WithMany(p => p.AvoirCertifications)
                .HasForeignKey(d => d.IdCollaborateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AvoirCert__#id_c__33D4B598");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.HasKey(e => e.IdCertification).HasName("PK__Certific__5476C1F4ADF9D45D");

            entity.ToTable("Certification");

            entity.Property(e => e.IdCertification)
                .ValueGeneratedNever()
                .HasColumnName("id_certification");
            entity.Property(e => e.Coût)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("coût");
            entity.Property(e => e.Durée)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("durée");
            entity.Property(e => e.NomCertification)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nom_certification");
        });

        modelBuilder.Entity<Collaborateur>(entity =>
        {
            entity.HasKey(e => e.IdCollaborateur).HasName("PK__Collabor__6BF9219EB2D161D3");

            entity.ToTable("Collaborateur");

            entity.Property(e => e.IdCollaborateur).HasColumnName("id_collaborateur");
            entity.Property(e => e.Anciennete)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("anciennete");
            entity.Property(e => e.DateDebutEssai)
                .HasColumnType("date")
                .HasColumnName("date_debut_essai");
            entity.Property(e => e.DateEmbauche)
                .HasColumnType("date")
                .HasColumnName("date_embauche");
            entity.Property(e => e.DateFinEssai)
                .HasColumnType("date")
                .HasColumnName("date_fin_essai");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("prenom");
            entity.Property(e => e.salaire)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("salaire");
            entity.Property(e => e.DateNaissance)
                .HasColumnType("date")
                .HasColumnName("date_naissance");
        });

        modelBuilder.Entity<Formation>(entity =>
        {
            entity.HasKey(e => e.IdFormation).HasName("PK__Formatio__0B5751333FB655EC");

            entity.ToTable("Formation");

            entity.Property(e => e.IdFormation)
                .ValueGeneratedNever()
                .HasColumnName("id_formation");
            entity.Property(e => e.DateDebutFormation)
                .HasColumnType("date")
                .HasColumnName("date_DebutFormation");
            entity.Property(e => e.DateFinFormation)
                .HasColumnType("date")
                .HasColumnName("date_finFormation");
            entity.Property(e => e.NomFormation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nom_formation");
        });

        modelBuilder.Entity<PasserFormation>(entity =>
        {
            entity.HasKey(e => new { e.IdCollaborateur, e.IdFormation, e.IdFormateur }).HasName("PK__PasserFo__C61B7FAE9F9C8AD0");

            entity.ToTable("PasserFormation");

            entity.Property(e => e.IdCollaborateur).HasColumnName("#id_collaborateur");
            entity.Property(e => e.IdFormation).HasColumnName("#id_formation");
            entity.Property(e => e.IdFormateur).HasColumnName("id_formateur");
            entity.Property(e=>e.NomFormateur)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NomFormateur");

            entity.HasOne(d => d.IdCollaborateurNavigation).WithMany(p => p.PasserFormations)
                .HasForeignKey(d => d.IdCollaborateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PasserFor__#id_c__2A4B4B5E");

            entity.HasOne(d => d.IdFormationNavigation).WithMany(p => p.PasserFormations)
                .HasForeignKey(d => d.IdFormation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PasserFor__#id_f__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
