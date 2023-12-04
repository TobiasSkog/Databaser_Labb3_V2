using Databaser_Labb3_V2.Application.Navigation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

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

    public virtual DbSet<Betyg> Betygs { get; set; }

    public virtual DbSet<KlassList> KlassLists { get; set; }

    public virtual DbSet<Klasser> Klassers { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Studenter> Studenters { get; set; }

    public virtual DbSet<Ämnen> Ämnens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("EdugradeHighSchool");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Betyg_PersonalId");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.FkStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Betyg_StudentId");

            entity.HasOne(d => d.FkÄmne).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.FkÄmneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Betyg_Ämnen");
        });

        modelBuilder.Entity<KlassList>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("KlassList");

            entity.Property(e => e.FkKlassId).HasColumnName("FK_KlassId");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

            entity.HasOne(d => d.FkKlass).WithMany()
                .HasForeignKey(d => d.FkKlassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KlassList_KlassId");

            entity.HasOne(d => d.FkStudent).WithMany()
                .HasForeignKey(d => d.FkStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
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
            entity.ToTable("Personal");

            entity.Property(e => e.PersonalEfternamn).HasMaxLength(25);
            entity.Property(e => e.PersonalFörnamn).HasMaxLength(25);
            entity.Property(e => e.PersonalNamn).HasMaxLength(50);
        });

        modelBuilder.Entity<Studenter>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK_Students");

            entity.ToTable("Studenter");

            entity.Property(e => e.StudentEfternamn).HasMaxLength(50);
            entity.Property(e => e.StudentFörnamn).HasMaxLength(50);
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

            entity.Property(e => e.ÄmneNamn).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public void UpdateUsersWithFirstAndLastNames()
    {
        foreach (var s in Studenters)
        {
            s.StudentFörnamn = string.IsNullOrEmpty(s.StudentFörnamn) ? s.StudentNamn.Substring(0, s.StudentNamn.IndexOf(' ')) : s.StudentFörnamn;
            s.StudentEfternamn = string.IsNullOrEmpty(s.StudentEfternamn) ? s.StudentNamn.Substring(s.StudentNamn.IndexOf(' ') + 1) : s.StudentEfternamn;
        }
        foreach (var p in Personals)
        {
            p.PersonalFörnamn = string.IsNullOrEmpty(p.PersonalFörnamn) ? p.PersonalNamn.Substring(0, p.PersonalNamn.IndexOf(' ')) : p.PersonalFörnamn;
            p.PersonalEfternamn = string.IsNullOrEmpty(p.PersonalEfternamn) ? p.PersonalNamn.Substring(p.PersonalNamn.IndexOf(' ') + 1) : p.PersonalEfternamn;
        }
        SaveChanges();
    }

    public List<Personal> GetAllPersonal()
    {
        return Personals.ToList();
    }
    public List<Personal> GetAllPersonalsByRole(UserType userType)
    {
        return (from p in Personals
                where p.PersonalBefattning == (byte)userType
                select p).ToList();
    }
    public List<Studenter> GetAllStudents()
    {
        // ORDER BY FIRST / LAST NAME ORDER ASC AND DESC!!!

        return (from s in Studenters
                select s
               ).ToList();


    }
    public List<KlassList> GetAllStudentsInClass(string className, OrderOption nameSort, OrderOption ascOrDesc)
    {
        // ORDER BY FIRST / LAST NAME ORDER ASC AND DESC!!!

        return nameSort switch
        {
            OrderOption.FirstName =>
            ascOrDesc == OrderOption.Ascending ?
                        KlassLists
                            .Include(cl => cl.FkKlass)
                            .Include(cl => cl.FkStudent)
                            .Where(cl => cl.FkKlass.KlassNamn == className)
                            .OrderBy(cl => cl.FkStudent.StudentFörnamn)
                            .ToList()

                        : KlassLists
                            .Include(cl => cl.FkKlass)
                            .Include(cl => cl.FkStudent)
                            .Where(cl => cl.FkKlass.KlassNamn == className)
                            .OrderByDescending(cl => cl.FkStudent.StudentFörnamn)
                            .ToList(),

            OrderOption.LastName =>
            ascOrDesc == OrderOption.Ascending ?
                    KlassLists
                        .Include(cl => cl.FkKlass)
                        .Include(cl => cl.FkStudent)
                        .Where(cl => cl.FkKlass.KlassNamn == className)
                        .OrderBy(cl => cl.FkStudent.StudentEfternamn)
                        .ToList()

                    : KlassLists
                        .Include(cl => cl.FkKlass)
                        .Include(cl => cl.FkStudent)
                        .Where(cl => cl.FkKlass.KlassNamn == className)
                        .OrderByDescending(cl => cl.FkStudent.StudentEfternamn)
                        .ToList()
        };
    }
    public List<string> GetAllClassNames() => (from c in Klassers select c.KlassNamn).ToList();
    public List<Betyg> GetAllGradesLastMonth()
    {
        return Betygs
            .Include(b => b.FkÄmne)
            .Where(b => b.BetygDatum <= DateOnly.FromDateTime(DateTime.Today))
            .Where(b => b.BetygDatum >= DateOnly.FromDateTime(DateTime.Today.AddDays(-30)))
            .OrderBy(b => b.BetygDatum)
            .ToList();
    }
    public List<CourseInformation> GetCourseInformation()
    {
        return Ämnens
            .Include(g => g.Betygs)
            .ToList()
            .Select(course => new CourseInformation
            {
                CourseName = course.ÄmneNamn,
                AverageGrade = CalculateAverageGrade(course.Betygs),
                LowestGrade = CalculateLowestGrade(course.Betygs),
                HighestGrade = CalculateHighestGrade(course.Betygs)
            })
            .ToList();
    }
    public void AddPersonalToDB(Personal personal)
    {
        try
        {
            Personals.Add(personal);
            //Personals.Add(new Personal
            //{
            //    PersonalNamn = personal.PersonalNamn,
            //    PersonalBefattning = personal.PersonalBefattning,
            //    PersonalFörnamn = personal.PersonalFörnamn,
            //    PersonalEfternamn = personal.PersonalEfternamn
            //});
            SaveChanges();

        }
        catch (DbUpdateException ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }

    public void AddStudentToDB(Studenter student)
    {
        try
        {
            Studenters.Add(student);
            //Studenters.Add(new Studenter
            //{
            //    StudentNamn = student.StudentNamn,
            //    StudentSsn = student.StudentSsn,
            //    StudentFörnamn = student.StudentFörnamn,
            //    StudentEfternamn = student.StudentEfternamn
            //});
            SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }



    private int MapGradesToNumericValue(string gradeChar)
    {
        return gradeChar.Trim() switch
        {
            "A" => (int)Grade.A,
            "B" => (int)Grade.B,
            "C" => (int)Grade.C,
            "D" => (int)Grade.D,
            "E" => (int)Grade.E,
            "F" => (int)Grade.F
        };
    }
    private Grade CalculateAverageGrade(ICollection<Betyg> grades)
    {
        var convertedGrades = grades.Select(b => MapGradesToNumericValue(b.Betyg1)).ToList();
        return convertedGrades.Count > 0 ? (Grade)convertedGrades.Average() : Grade.F;
    }

    private Grade CalculateLowestGrade(ICollection<Betyg> grades)
    {
        var convertedGrades = grades.Select(b => MapGradesToNumericValue(b.Betyg1)).ToList();
        return convertedGrades.Count > 0 ? (Grade)convertedGrades.Max() : Grade.F;
    }

    private Grade CalculateHighestGrade(ICollection<Betyg> grades)
    {
        var convertedGrades = grades.Select(b => MapGradesToNumericValue(b.Betyg1)).ToList();
        return convertedGrades.Count > 0 ? (Grade)convertedGrades.Min() : Grade.F;
    }
}
