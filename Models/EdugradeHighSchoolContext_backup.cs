using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Databaser_Labb3_V2.Models;

public partial class EdugradeHighSchoolContext : DbContext
{
    public EdugradeHighSchoolContext()
    {
    }

    public EdugradeHighSchoolContext(DbContextOptions<EdugradeHighSchoolContext> options)
        : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine("AppDomain.CurrentDomain.BaseDirectory");
        Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("EdugradeHighSchool"));
    }

    public virtual DbSet<Avdelning> Avdelnings { get; set; }

    public virtual DbSet<Betyg> Betygs { get; set; }

    public virtual DbSet<KlassList> KlassLists { get; set; }

    public virtual DbSet<Klasser> Klassers { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Studenter> Studenters { get; set; }

    public virtual DbSet<View_GetGradesFromLastMonth> View_GetGradesFromLastMonths { get; set; }

    public virtual DbSet<Ämnen> Ämnens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<View_GetGradesFromLastMonth>()
                .ToView("View_GetGradesFromLastMonth")
                .HasNoKey();

        modelBuilder.Entity<Avdelning>(entity =>
        {
            entity.ToTable("Avdelning");

            entity.Property(e => e.AvdelningNamn).HasMaxLength(100);
        });

        modelBuilder.Entity<Betyg>(entity =>
        {
            entity.ToTable("Betyg");

            entity.Property(e => e.Betyg1)
                .HasMaxLength(3)
                .HasColumnName("Betyg");
            entity.Property(e => e.FkPersonalId).HasColumnName("FK_PersonalId");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");
            entity.Property(e => e.FkÄmneId).HasColumnName("FK_ÄmneId");

            entity.HasOne(d => d.FkPersonal).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.FkPersonalId)
                .HasConstraintName("FK_Betyg_PersonalId");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_Betyg_StudentId");

            entity.HasOne(d => d.FkÄmne).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.FkÄmneId)
                .HasConstraintName("FK_Betyg_ÄmneId");
        });

        modelBuilder.Entity<KlassList>(entity =>
        {
            //OM JAG SKA LÄGGA TILL KLASSLISTS SÅ KAN DETTA BEHÖVAS!
            entity.HasKey(kl => new { kl.FkKlassId, kl.FkStudentId });
            entity.ToTable("KlassList");
            //entity
            //    .HasNoKey()
            //    .ToTable("KlassList");

            entity.Property(e => e.FkKlassId).HasColumnName("FK_KlassId");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

            entity.HasOne(d => d.FkKlass).WithMany()
                .HasForeignKey(d => d.FkKlassId)
                .HasConstraintName("FK_KlassList_Klasser");

            entity.HasOne(d => d.FkStudent).WithMany()
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_KlassList_StudentId");
        });

        modelBuilder.Entity<Klasser>(entity =>
        {
            entity.HasKey(e => e.KlassId);

            entity.ToTable("Klasser");

            entity.Property(e => e.KlassNamn).HasMaxLength(50);
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.ToTable("Personal", tb => tb.HasTrigger("PersonalAgeGenderName"));

            entity.Property(e => e.FkAvdelningId).HasColumnName("FK_AvdelningId");
            entity.Property(e => e.PersonalEfternamn).HasMaxLength(25);
            entity.Property(e => e.PersonalFörnamn).HasMaxLength(25);
            entity.Property(e => e.PersonalKön)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PersonalLön).HasColumnType("money");
            entity.Property(e => e.PersonalNamn).HasMaxLength(50);
            entity.Property(e => e.PersonalSsn)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("PersonalSSN");

            entity.HasOne(d => d.FkAvdelning).WithMany(p => p.Personals)
                .HasForeignKey(d => d.FkAvdelningId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Personal_Avdelning");
        });

        modelBuilder.Entity<Studenter>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK_Students");

            entity.ToTable("Studenter", tb => tb.HasTrigger("StudentAgeGenderName"));

            entity.Property(e => e.StudentEfternamn).HasMaxLength(50);
            entity.Property(e => e.StudentFödelsedag).HasColumnType("datetime");
            entity.Property(e => e.StudentFörnamn).HasMaxLength(50);
            entity.Property(e => e.StudentKön)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StudentNamn).HasMaxLength(100);
            entity.Property(e => e.StudentSsn)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("StudentSSN");
        });



        modelBuilder.Entity<Ämnen>(entity =>
        {
            entity.HasKey(e => e.ÄmneId).HasName("PK_Ämne");

            entity.ToTable("Ämnen");

            entity.Property(e => e.ÄmneAktivt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ÄmneNamn).HasMaxLength(50);


            //entity.Property(e => e.HurKändesDet)
            //    .HasMaxLength(25)
            //    .HasColumnType("money");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
