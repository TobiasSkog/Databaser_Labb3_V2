using Databaser_Labb3_V2.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace Databaser_Labb3_V2.Application.DataGenerators;

public class BetygGenerator
{
    private readonly EdugradeHighSchoolContext Context = new();
    public async Task GenerateRandomBetyg(int minStudents, int maxStudents, int minCourses, int maxCourses)
    {
        try
        {
            Random random = new();
            int minMonth = 09;
            int minDay = 04;

            var courseList = await Context.Ämnens
                .Take(random.Next(minCourses, maxCourses + 1))
                .ToListAsync();

            var students = Context.Studenters.ToList();

            var studentAndClass = Context.KlassLists
                .Join(Context.Klassers, klassList => klassList.FkKlassId,
                klass => klass.KlassId,
                (klassList, klass) => new { klassList, klass })
                .Join(Context.Studenters,
                combined => combined.klassList.FkStudentId,
                student => student.StudentId,
                (combined, student) => new { combined.klassList, combined.klass, student })
                .ToList();

            foreach (var student in students.Take(random.Next(minStudents, maxStudents + 1)))
            {
                foreach (var course in courseList)
                {
                    var gradeExists = Context.Betygs
                        .FirstOrDefault(b => b.FkStudentId == student.StudentId && b.FkÄmneId == course.ÄmneId);

                    if (gradeExists is null)
                    {
                        int personalId = DataLists.GetRandomTeacherId();
                        var betyg = new Betyg
                        {
                            Betyg1 = GenerateRandomGrade(random),
                            BetygDatum = GenerateRandomBetygDatum(student.StudentStartDatum.Value.Year, minMonth, minDay, random),
                            FkStudentId = student.StudentId,
                            FkÄmneId = course.ÄmneId,
                            FkPersonalId = personalId,
                            FkStudent = student,
                            FkÄmne = course,
                            FkPersonal = Context.Personals.FirstOrDefault(p => p.PersonalId == personalId)
                        };
                        await Context.Betygs.AddAsync(betyg);
                    }
                }
            }

            await Context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }

    private string GenerateRandomGrade(Random random) =>

        random.Next(101) >= 70 ? "A" :
        random.Next(101) >= 55 ? "B" :
        random.Next(101) >= 40 ? "C" :
        random.Next(101) >= 20 ? "D" :
        random.Next(101) >= 5 ? "E" :
        "F";

    private DateOnly GenerateRandomBetygDatum(int minYear, int minMonth, int minDay, Random random)
    {

        var today = DateTime.Today;
        var year = random.Next(minYear, Math.Min(minYear + 4, today.Year + 1));
        var month = random.Next(year == today.Year ? minMonth : 1, year == today.Year ? today.Month + 1 : 13);
        var day = random.Next(year == today.Year ? minDay : 1, year == today.Year ? today.Day + 1 : DateTime.DaysInMonth(year, month) + 1);
        return DateOnly.FromDateTime(new DateTime(year, month, day));
    }
}
