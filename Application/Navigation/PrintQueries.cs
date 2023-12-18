using Databaser_Labb3_V2.Models;
using Spectre.Console;

namespace Databaser_Labb3_V2.Application.Navigation;

public class PrintQueries
{
    public static void PrintPersonalInformation(List<Personal> personal)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(pWidth, 70);

        var table = new Table();
        table.AddColumns(
            new TableColumn($"Personal ID"),
            new TableColumn("First Name"),
            new TableColumn("Last Name"),
            new TableColumn($"Role"),
            new TableColumn($"Age"),
            new TableColumn($"Sex"),
            new TableColumn($"Social Security Number")
            );
        foreach (var p in personal)
        {
            {
                table.AddRow(
                    new Text(p.PersonalId.ToString()),
                    new Text(p.PersonalFörnamn),
                    new Text(p.PersonalEfternamn),
                    new Text((p.PersonalBefattning == 1 ? "Teacher" : p.PersonalBefattning == 2 ? "Administrator" : "Education Leader")),
                    new Text(p.PersonalÅlder.ToString()),
                    new Text(p.PersonalKön),
                    new Text(p.PersonalSsn)
                    );
            }

        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        //Console.SetWindowSize(pWidth, pHeight);
        AnsiConsole.Clear();
    }
    public static void PrintStudentInformationAsTable(List<Studenter> students)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(pWidth, 70);
        var table = new Table();
        table.AddColumns(
            new TableColumn("Student ID"),
            new TableColumn("First Name"),
            new TableColumn("Last Name"),
            new TableColumn($"Age"),
            new TableColumn($"Sex"),
            new TableColumn("Social Security Number")
            );
        foreach (var s in students)
        {
            {
                table.AddRow(
                    new Text(s.StudentId.ToString()),
                    new Text(s.StudentFörnamn),
                    new Text(s.StudentEfternamn),
                    new Text(s.StudentÅlder.ToString()),
                    new Text(s.StudentKön),
                    new Text(s.StudentSsn)
                    );
            }
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        //Console.SetWindowSize(pWidth, pHeight);
        AnsiConsole.Clear();
    }
    public static void PrintStudentsInClass(List<KlassList> classAndStudentList)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(pWidth, 70);
        var table = new Table();
        table.AddColumns(
            new TableColumn("Student ID"),
            new TableColumn("First Name"),
            new TableColumn("Last Name"),
            new TableColumn($"Age"),
            new TableColumn($"Sex"),
            new TableColumn("Social Security Number"),
            new TableColumn("Class"),
            new TableColumn("Class ID")
            );
        foreach (var cl in classAndStudentList)
        {
            {
                table.AddRow(
                    new Text(cl.FkStudent.StudentId.ToString()),
                    new Text(cl.FkStudent.StudentFörnamn),
                    new Text(cl.FkStudent.StudentEfternamn),
                    new Text(cl.FkStudent.StudentÅlder.ToString()),
                    new Text(cl.FkStudent.StudentKön),
                    new Text(cl.FkStudent.StudentSsn),
                    new Text(cl.FkKlass.KlassNamn),
                    new Text(cl.FkKlassId.ToString())
                    );
            }
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        //Console.SetWindowSize(pWidth, pHeight);
        AnsiConsole.Clear();
    }

    public static void PrintGradesInformation(List<View_GetGradesFromLastMonth> grades)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(pWidth, 80);
        var table = new Table();
        table.AddColumns(
            new TableColumn("Student"),
            new TableColumn("Course"),
            new TableColumn("Grade"),
            new TableColumn("Teacher Grading"),
            new TableColumn("Date")
            );
        foreach (var g in grades)
        {
            {
                table.AddRow(
                    new Text(g.Student),
                    new Text(g.Ämne),
                    new Text(g.Betyg.ToString()),
                    new Text(g.Datum.ToString()),
                    new Text(g.Lärare)
                    );
            }
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        //Console.SetWindowSize(pWidth, pHeight);
        AnsiConsole.Clear();
    }
    public static void PrintAverageGradeByGenderAndAgeGroup(List<(string? Gender, int AgeGroup, double AverageGradeNumeric, Grade AverageGradeString)> averageGradeGenderAgeGroupInfo)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(pWidth, 87);
        var table = new Table();
        table.AddColumns(
            new TableColumn("Age Group"),
            new TableColumn("Average Grade"),
            new TableColumn("Gender")
            );
        foreach (var result in averageGradeGenderAgeGroupInfo)
        {
            table.AddRow(
                new Text($"{result.AgeGroup}"),
                new Text($"{result.AverageGradeString} - {result.AverageGradeNumeric:.##}"), // - {result.AverageGrade:.##}
                new Text($"{(result.Gender == "M" ? "Male" : "Female")}")
                );
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        //Console.SetWindowSize(pWidth, pHeight);
        AnsiConsole.Clear();
    }

    public static void PrintAmountOfTeachersInEachDepartMent(Dictionary<string, int> departmentCounts)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(pWidth, 60);
        var table = new Table();
        table.AddColumns(
            new TableColumn("Amount of Teachers"),
            new TableColumn("Department")
            );

        foreach (var kvp in departmentCounts)
        {
            table.AddRow(
                new Text(kvp.Value.ToString()),
                new Text(kvp.Key)
                );
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        //Console.SetWindowSize(pWidth, pHeight);
        AnsiConsole.Clear();
    }

    public static void PrintCourseInformation(List<CourseInformation> courseInformation)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(pWidth, 40);

        var table = new Table();
        table.AddColumns(
            new TableColumn("Course Name"),
            new TableColumn("Average Grade"),
            new TableColumn("Lowest Grade"),
            new TableColumn("Highest Grade"),
            new TableColumn("Active")
            );
        foreach (var c in courseInformation)
        {
            {
                table.AddRow(
                    new Text(c.CourseName),
                    new Text(c.AverageGrade.ToString()),
                    new Text(c.LowestGrade.ToString()),
                    new Text(c.HighestGrade.ToString()),
                    new Text(c.ÄmneAktivt.ToString())
                    );
            }
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        //Console.SetWindowSize(pWidth, pHeight);
        AnsiConsole.Clear();
    }

    public static void PrintAllStudentInfo(List<StudentInfo> studentInfoList)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(234, 40);

        const int pageSize = 10;
        var totalPages = (int)Math.Ceiling((double)studentInfoList.Count / pageSize);
        var currentPage = 1;

        do
        {
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize, studentInfoList.Count);


            var table = new Table();
            table.AddColumns(
                new TableColumn("ID"),
                new TableColumn("Social Security Number"),
                new TableColumn("First Name"),
                new TableColumn("Last Name"),
                new TableColumn("Gender"),
                new TableColumn("Age"),
                new TableColumn("Start Date"),
                new TableColumn("Class"),
                new TableColumn("Grade"),
                new TableColumn("Grade Date"),
                new TableColumn("Grade Subject"),
                new TableColumn("Teacher Name"),
                new TableColumn("Teacher Gender"),
                new TableColumn("Teacher Age"),
                new TableColumn("Teacher Start Date"),
                new TableColumn("Teacher Department")
                );
            for (var i = startIndex; i < endIndex; i++)
            {
                // foreach (var studentInfo in studentInfoList)
                //{

                var studentInfo = studentInfoList[i];
                table.AddRow(
                    new Text($"{studentInfo.Student.StudentId}"),
                    new Text($"{studentInfo.Student.StudentSsn}"),
                    new Text($"{studentInfo.Student.StudentFörnamn}"),
                    new Text($"{studentInfo.Student.StudentEfternamn}"),
                    new Text($"{(studentInfo.Student.StudentKön == "M" ? "Male" : "Female")}"),
                    new Text($"{studentInfo.Student.StudentÅlder}"),
                    new Text($"{studentInfo.Student.StudentStartDatum:yyyy-MM-dd}"),
                    new Text($"{studentInfo.Klasser.KlassNamn}"),
                    new Text($"{studentInfo.Betyg.Betyg1}"),
                    new Text($"{studentInfo.Betyg.BetygDatum:yyyy-MM-dd}"),
                    new Text($"{studentInfo.Ämnen.ÄmneNamn}"),
                    new Text($"{studentInfo.Personal.PersonalNamn}"),
                    new Text($"{(studentInfo.Personal.PersonalKön == "M" ? "Male" : "Female")}"),
                    new Text($"{studentInfo.Personal.PersonalÅlder}"),
                    new Text($"{studentInfo.Personal.PersonalStartDatum:yyyy-MM-dd}"),
                    new Text($"{studentInfo.Avdelning.AvdelningNamn}")

                    );

            }
            AnsiConsole.Write(table);
            AnsiConsole.WriteLine($"\nWidth: {Console.WindowWidth}\tHeight:{Console.WindowHeight}");
            AnsiConsole.WriteLine($"\nPage {currentPage} of {totalPages}");
            AnsiConsole.WriteLine("Press Enter to view the next page (or any other key to exit)...");

            var key = Console.ReadKey().Key;
            if (key != ConsoleKey.Enter)
            {
                //Console.SetWindowSize(pWidth, pHeight);
                break;
            }

            AnsiConsole.Clear();
            currentPage++;
        } while (currentPage <= totalPages);

        //AnsiConsole.WriteLine("\nPress any key to go back...");
        //Console.ReadKey();
        AnsiConsole.Clear();
    }

    internal static void PrintActiveCourses(List<Ämnen> courses)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(pWidth, 40);

        var table = new Table();
        table.AddColumns(
            new TableColumn("Course Id"),
            new TableColumn("Course Name"),
            new TableColumn("Active")
            );
        foreach (var c in courses)
        {
            {
                table.AddRow(
                    new Text(c.ÄmneId.ToString()),
                    new Text(c.ÄmneNamn),
                    new Text(c.ÄmneAktivt == "T" ? "True" : "False")
                    );
            }
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        //Console.SetWindowSize(pWidth, pHeight);
        AnsiConsole.Clear();
    }

    internal static void PrintMaxAndAveragePayoutDepartmentInfo(List<DepartmentPayoutInformation> departmentPayoutInformation)
    {
        //int pWidth = Console.WindowWidth, pHeight = Console.WindowHeight;
        //Console.SetWindowSize(pWidth, 40);

        var table = new Table();
        table.AddColumns(
            new TableColumn("Department Name"),
            new TableColumn("Department Total Payout"),
            new TableColumn("Department Average Payout")
            );
        foreach (var c in departmentPayoutInformation)
        {
            {
                table.AddRow(
                    new Text(c.Avdelning.AvdelningNamn),
                    new Text(c.TotalPayout.ToString(".00 SEK")),
                    new Text(c.AveragePayout.ToString(".00 SEK"))
                    );
            }
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        //Console.SetWindowSize(pWidth, pHeight);
        AnsiConsole.Clear();
    }
}