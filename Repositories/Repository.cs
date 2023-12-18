using Databaser_Labb3_V2.Application.Navigation;
using Databaser_Labb3_V2.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace Databaser_Labb3_V2.Repositories;

public class Repository : IRepository
{
    private EdugradeHighSchoolContext Context { get; set; }
    public Repository(EdugradeHighSchoolContext context)
    {
        Context = context;
    }

    public async Task<List<Personal>> GetAllPersonal() => await Context.Personals.ToListAsync();
    public async Task<List<Studenter>> GetAllStudents() => await Context.Studenters.ToListAsync();
    public async Task<List<string>> GetAllClassNames() => await (from c in Context.Klassers select c.KlassNamn).ToListAsync();
    public async Task<List<View_GetGradesFromLastMonth>> GetAllGradesLastMonth() => await Context.View_GetGradesFromLastMonths.ToListAsync();
    public async Task<List<Personal>> GetAllPersonalsByRole(UserType userType)
    {
        try
        {
            return await (from p in Context.Personals
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
                        await Context.KlassLists
                            .Include(cl => cl.FkKlass)
                            .Include(cl => cl.FkStudent)
                            .Where(cl => cl.FkKlass.KlassNamn == className)
                            .OrderBy(cl => cl.FkStudent.StudentFörnamn)
                            .ToListAsync()

                        : await Context.KlassLists
                            .Include(cl => cl.FkKlass)
                            .Include(cl => cl.FkStudent)
                            .Where(cl => cl.FkKlass.KlassNamn == className)
                            .OrderByDescending(cl => cl.FkStudent.StudentFörnamn)
                            .ToListAsync(),

                OrderOption.LastName =>
                    ascOrDesc == OrderOption.Ascending ?
                        await Context.KlassLists
                            .Include(cl => cl.FkKlass)
                            .Include(cl => cl.FkStudent)
                            .Where(cl => cl.FkKlass.KlassNamn == className)
                            .OrderBy(cl => cl.FkStudent.StudentEfternamn)
                            .ToListAsync()

                        : await Context.KlassLists
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
    public async Task<List<(string? Gender, int AgeGroup, double AverageGradeNumeric, Grade AverageGradeString)>> GetAverageGradesBasedByAgeAndGender()
    {
        return Context.Betygs
            .Join(Context.Studenters, betyg => betyg.FkStudentId,
            student => student.StudentId,
            (betyg, student) => new { betyg, student })
            .Join(Context.Ämnens,
            combined => combined.betyg.FkÄmneId,
            ämne => ämne.ÄmneId,
            (combined, ämne) => new { combined.betyg, combined.student, ämne })
            .GroupBy(x => new
            {
                x.student.StudentKön,
                AgeGroup = x.student.StudentFödelsedag.HasValue
                ? x.student.StudentFödelsedag.Value.Year
                : 99

            })
            .AsQueryable()
            .AsEnumerable()
            .Select(group => new
            {
                group.Key.StudentKön,
                group.Key.AgeGroup,
                AverageGrade = group.Average(x => MapGradesToNumericValue(x.betyg.Betyg1))
            })
            .OrderBy(x => x.AgeGroup)
            .ThenBy(x => x.StudentKön)
            .Select(x => (x.StudentKön, x.AgeGroup, x.AverageGrade, MapGradesFromNumericToGrade(x.AverageGrade)))
            .ToList();
    }
    public async Task<List<CourseInformation>> GetCourseInformation()
    {
        try
        {
            var courses = await Context.Ämnens
                .Where(ämnen => ämnen.Betygs.Count != 0)
                .Include(ämnen => ämnen.Betygs)
                .ToListAsync();

            return courses.Select(course => new CourseInformation
            {
                CourseName = course.ÄmneNamn,
                AverageGrade = CalculateAverageGrade(course.Betygs),
                LowestGrade = CalculateLowestGrade(course.Betygs),
                HighestGrade = CalculateHighestGrade(course.Betygs),
                ÄmneAktivt = course.ÄmneAktivt
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
            await Context.Personals.AddAsync(personal);
            await Context.SaveChangesAsync();
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
            await Context.Personals.AddRangeAsync(personal);
            await Context.SaveChangesAsync();
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
            await Context.Studenters.AddAsync(student);
            await Context.SaveChangesAsync();
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
            await Context.Studenters.AddRangeAsync(studenter);
            await Context.SaveChangesAsync();
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
            var studentsNotAssigned = await Context.Studenters
                .Where(s => !Context.KlassLists.Any(kl => kl.FkStudentId == s.StudentId))
                .ToListAsync();

            Random random = new();

            foreach (var student in studentsNotAssigned)
            {
                int randomValue = random.Next(1, 15);

                Context.KlassLists.Add(new KlassList
                {
                    FkStudentId = student.StudentId,
                    FkKlassId = randomValue
                });
            }

            await Context.SaveChangesAsync();
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
    private Grade MapGradesFromNumericToGrade(double gradeValue) =>
        gradeValue > 4.5 ? Grade.A :
        gradeValue > 3.5 ? Grade.B :
        gradeValue > 2.5 ? Grade.C :
        gradeValue > 1.5 ? Grade.D :
        gradeValue > 0.5 ? Grade.E :
        Grade.F;


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


    public async Task<Dictionary<string, int>> GetTeachersInEveryDepartMent()
    {
        var result = await Context.Personals
            .Join(Context.Avdelnings, personal => personal.FkAvdelningId,
            avdelning => avdelning.AvdelningId,
            (personal, avdelning) => new { personal, avdelning })
            .Where(combined => combined.personal.PersonalBefattning == 1)
            .ToListAsync();

        var departmentCounts = new Dictionary<string, int>();

        foreach (var r in result)
        {
            if (departmentCounts.ContainsKey(r.avdelning.AvdelningNamn))
            {
                departmentCounts[r.avdelning.AvdelningNamn]++;
            }
            else
            {
                departmentCounts[r.avdelning.AvdelningNamn] = 1;
            }
        }

        return departmentCounts;
    }

    public async Task<List<StudentInfo>> GetAllStudentInfo()
    {
        var result = await Context.Studenters
            .Join(Context.KlassLists, student => student.StudentId,
        klassList => klassList.FkStudentId,
            (student, klassList) => new { student, klassList })
            .Join(Context.Klassers, combined => combined.klassList.FkKlassId,
        klass => klass.KlassId,
            (combined, klass) => new { combined.klassList, combined.student, klass })
            .Join(Context.Betygs, combined => combined.student.StudentId,
            betyg => betyg.FkStudentId,
            (combined, betyg) => new { combined.klassList, combined.student, combined.klass, betyg })
            .Join(Context.Ämnens, combined => combined.betyg.FkÄmneId,
            ämne => ämne.ÄmneId,
            (combined, ämne) => new { combined.klassList, combined.student, combined.klass, combined.betyg, ämne })
            .Join(Context.Personals, combined => combined.betyg.FkPersonalId,
            personal => personal.PersonalId,
            (combined, personal) => new { combined.klassList, combined.student, combined.klass, combined.betyg, combined.ämne, personal })
            .Join(Context.Avdelnings, combined => combined.personal.FkAvdelningId,
            avdelning => avdelning.AvdelningId,
            (combined, avdelning) => new StudentInfo
            {
                Student = combined.student,
                KlassList = combined.klassList,
                Klasser = combined.klass,
                Betyg = combined.betyg,
                Ämnen = combined.ämne,
                Personal = combined.personal,
                Avdelning = avdelning
            })
            .ToListAsync();


        return result;
    }

    public async Task<List<Ämnen>> GetAllActiveCourses()
    {
        return await Context.Ämnens
            .Include(ämne => ämne.Betygs)
            .Where(ämne => ämne.ÄmneAktivt == "T")
            .ToListAsync();
    }

    public async Task<List<DepartmentPayoutInformation>> GetDepartmentPayoutInformation()
    {
        //Avdelning
        //TotalPayout 
        //AveragePayout
        return await Context.Avdelnings
                             .Include(avdelning => avdelning.Personals)
                             .Select(avdelning => new DepartmentPayoutInformation
                             {
                                 Avdelning = avdelning,
                                 TotalPayout = avdelning.Personals.Sum(personal => personal.PersonalLön.Value),
                                 AveragePayout = avdelning.Personals.Average(personal => personal.PersonalLön.Value)
                             }).ToListAsync();

    }
}