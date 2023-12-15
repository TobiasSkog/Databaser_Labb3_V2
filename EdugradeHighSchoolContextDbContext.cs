using Databaser_Labb3_V2.Application.Navigation;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace Databaser_Labb3_V2.Models;

public partial class EdugradeHighSchoolContext : DbContext
{
    //public void UpdateUsersWithFirstAndLastNames()
    //{
    //foreach (var s in Studenters)
    //{
    //    s.StudentFörnamn = string.IsNullOrEmpty(s.StudentFörnamn) ? s.StudentNamn.Substring(0, s.StudentNamn.IndexOf(' ')) : s.StudentFörnamn;
    //    s.StudentEfternamn = string.IsNullOrEmpty(s.StudentEfternamn) ? s.StudentNamn.Substring(s.StudentNamn.IndexOf(' ') + 1) : s.StudentEfternamn;
    //}
    //    foreach (var p in Personals)
    //    {
    //        p.PersonalFörnamn = string.IsNullOrEmpty(p.PersonalFörnamn) ? p.PersonalNamn.Substring(0, p.PersonalNamn.IndexOf(' ')) : p.PersonalFörnamn;
    //        p.PersonalEfternamn = string.IsNullOrEmpty(p.PersonalEfternamn) ? p.PersonalNamn.Substring(p.PersonalNamn.IndexOf(' ') + 1) : p.PersonalEfternamn;
    //    }
    //    SaveChanges();
    //}
    public virtual DbSet<View_GetGradesFromLastMonth> LastMonthsGrades { get; set; }





    public async Task<List<Personal>> GetAllPersonal() => await Personals.ToListAsync();
    public async Task<List<Studenter>> GetAllStudents() => await Studenters.ToListAsync();
    public async Task<List<string>> GetAllClassNames() => await (from c in Klassers select c.KlassNamn).ToListAsync();
    public async Task<List<View_GetGradesFromLastMonth>> GetAllGradesLastMonth() => await LastMonthsGrades.ToListAsync();
    public async Task<List<Personal>> GetAllPersonalsByRole(UserType userType)
    {
        try
        {
            return await (from p in Personals
                          where p.PersonalBefattning == (byte)userType
                          select p).ToListAsync();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return default;
        }
    }
    public async Task<List<KlassList>> GetAllStudentsInClass(string className, OrderOption nameSort, OrderOption ascOrDesc)
    {
        try
        {
            return nameSort switch
            {
                OrderOption.FirstName =>
                    ascOrDesc == OrderOption.Ascending ?
                        await KlassLists
                            .Include(cl => cl.FkKlass)
                            .Include(cl => cl.FkStudent)
                            .Where(cl => cl.FkKlass.KlassNamn == className)
                            .OrderBy(cl => cl.FkStudent.StudentFörnamn)
                            .ToListAsync()

                        : await KlassLists
                            .Include(cl => cl.FkKlass)
                            .Include(cl => cl.FkStudent)
                            .Where(cl => cl.FkKlass.KlassNamn == className)
                            .OrderByDescending(cl => cl.FkStudent.StudentFörnamn)
                            .ToListAsync(),

                OrderOption.LastName =>
                    ascOrDesc == OrderOption.Ascending ?
                        await KlassLists
                            .Include(cl => cl.FkKlass)
                            .Include(cl => cl.FkStudent)
                            .Where(cl => cl.FkKlass.KlassNamn == className)
                            .OrderBy(cl => cl.FkStudent.StudentEfternamn)
                            .ToListAsync()

                        : await KlassLists
                            .Include(cl => cl.FkKlass)
                            .Include(cl => cl.FkStudent)
                            .Where(cl => cl.FkKlass.KlassNamn == className)
                            .OrderByDescending(cl => cl.FkStudent.StudentEfternamn)
                            .ToListAsync()
            };
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return default;
        }
    }
    //public List<Betyg> GetAllGradesLastMonth()
    //{
    //    return Betygs
    //        .Include(b => b.FkÄmne)
    //        .Where(b => b.BetygDatum <= DateOnly.FromDateTime(DateTime.Today))
    //        .Where(b => b.BetygDatum >= DateOnly.FromDateTime(DateTime.Today.AddDays(-30)))
    //        .OrderBy(b => b.BetygDatum)
    //        .ToList();
    //}


    public async Task<List<CourseInformation>> GetCourseInformation()
    {
        try
        {
            var courses = await Ämnens
            .Include(g => g.Betygs)
            .ToListAsync();

            return courses.Select(course => new CourseInformation
            {
                CourseName = course.ÄmneNamn,
                AverageGrade = CalculateAverageGrade(course.Betygs),
                LowestGrade = CalculateLowestGrade(course.Betygs),
                HighestGrade = CalculateHighestGrade(course.Betygs)
            }).ToList();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return default;
        }
    }
    public async Task AddPersonalToDB(Personal personal)
    {
        try
        {
            await Personals.AddAsync(personal);
            await SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }
    public async Task AddPersonalToDB(List<Personal> personal)
    {
        try
        {
            await Personals.AddRangeAsync(personal);
            await SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }
    public async Task AddStudentToDB(Studenter student)
    {
        try
        {
            await Studenters.AddAsync(student);
            await SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }
    public async Task AddStudentToDB(List<Studenter> studenter)
    {
        try
        {
            await Studenters.AddRangeAsync(studenter);
            await SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }
    public async Task AssignStudentsToKlassList()
    {
        try
        {
            var studentsNotAssigned = await Studenters
                .Where(s => !KlassLists.Any(kl => kl.FkStudentId == s.StudentId))
                .ToListAsync();

            Random random = new Random();

            foreach (var student in studentsNotAssigned)
            {
                int randomValue = random.Next(1, 15);

                KlassLists.Add(new KlassList
                {
                    FkStudentId = student.StudentId,
                    FkKlassId = randomValue
                });
            }

            await SaveChangesAsync();
        }
        catch (Exception ex)
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
        return convertedGrades.Count > 0 ? (Grade)convertedGrades.Min() : Grade.F;
    }
    private Grade CalculateHighestGrade(ICollection<Betyg> grades)
    {
        var convertedGrades = grades.Select(b => MapGradesToNumericValue(b.Betyg1)).ToList();
        return convertedGrades.Count > 0 ? (Grade)convertedGrades.Max() : Grade.F;
    }


    public async Task GenerateRandomBetyg(int minStudents, int maxStudents, int minCourses, int maxCourses)
    {
        try
        {
            Random random = new();
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
            var cutoffDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));

            var randomPersonalForGrades = Ämnens
                .SelectMany(a => Personals
                    .Where(p => p.PersonalBefattning == 1)
                    .Select(p => new { a.ÄmneId, PersonalId = p.PersonalId })
                )
                .Take(maxCourses);

            var students = await Studenters
                .Where(s => string.Compare(s.StudentSsn.Substring(0, 8), cutoffDate.ToString("yyyyMMdd")) <= 0)
                .Take(maxStudents)
                .ToListAsync();

            foreach (var student in students.Take(random.Next(minStudents, maxStudents + 1)))
            {
                var selectedCourses = randomPersonalForGrades.Take(random.Next(minCourses, maxCourses + 1)).ToList();

                foreach (var course in selectedCourses)
                {
                    var betygExists = Betygs
                        .Any(b => b.FkStudentId == student.StudentId && b.FkÄmneId == course.ÄmneId);

                    if (!betygExists)
                    {
                        var betyg = new Betyg
                        {
                            Betyg1 = GenerateRandomGrade(random),
                            BetygDatum = GenerateRandomBetygDatum(startDate, random),
                            FkStudentId = student.StudentId,
                            FkÄmneId = course.ÄmneId,
                            FkPersonalId = course.PersonalId
                        };

                        await Betygs.AddAsync(betyg);
                    }
                }
            }

            await SaveChangesAsync();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }

    private string GenerateRandomGrade(Random random)
    {
        var gradeOptions = new[] { "A", "B", "C", "D", "E", "F" };
        return gradeOptions[random.Next(gradeOptions.Length)];
    }

    private DateOnly GenerateRandomBetygDatum(DateOnly startDate, Random random)
    {
        try
        {
            var isWithinOneMonth = random.NextDouble() < 0.5;
            var randomDays = (int)(random.NextDouble() * (isWithinOneMonth ? 30 : 365));

            return startDate.AddDays(randomDays);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return default;
        }
    }
}
