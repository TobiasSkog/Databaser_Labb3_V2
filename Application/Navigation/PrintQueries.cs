using Databaser_Labb3_V2.Models;
using Spectre.Console;

namespace Databaser_Labb3_V2.Application.Navigation
{
    public class PrintQueries
    {
        public static void PrintPersonalInformation(List<Personal> personal)
        {
            var table = new Table();
            table.AddColumns(
                new TableColumn($"Personal ID"),
                new TableColumn($"Name"),
                new TableColumn($"Role")
                );

            foreach (var p in personal)
            {
                {
                    table.AddRow(
                        new Text(p.PersonalId.ToString()),
                        new Text(p.PersonalNamn),
                        new Text((p.PersonalBefattning == 1 ? "Teacher" : p.PersonalBefattning == 2 ? "Administrator" : "Education Leader"))
                        );
                }
            }

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
            AnsiConsole.Clear();
        }

        //public static void PrintStudentInformation(List<Studenter> students)
        //{

        //    var nameSorting = HelperMethods.GetOrderOptionFirstOrLastName();
        //    var ascOrDesc = HelperMethods.GetOrderOptionAscendingOrDescending();
        //    switch (nameSorting)
        //    {
        //        case OrderOption.FirstName:
        //            if (ascOrDesc == OrderOption.Ascending)
        //            {
        //                var firstNameAscending = students.OrderBy(s => s.StudentFörnamn).ToList();
        //                PrintStudentInformationAsTable(firstNameAscending);
        //            }
        //            else
        //            {
        //                var firstNameDescending = students.OrderByDescending(s => s.StudentFörnamn).ToList();
        //                PrintStudentInformationAsTable(firstNameDescending);
        //            }
        //            break;

        //        case OrderOption.LastName:
        //            if (ascOrDesc == OrderOption.Ascending)
        //            {
        //                var lastNameAscending = students.OrderBy(s => s.StudentEfternamn).ToList();
        //                PrintStudentInformationAsTable(lastNameAscending);
        //            }
        //            else
        //            {
        //                var lastNameDescending = students.OrderByDescending(s => s.StudentEfternamn).ToList();
        //                PrintStudentInformationAsTable(lastNameDescending);
        //            }
        //            break;
        //    }
        //}


        public static void PrintStudentInformationAsTable(List<Studenter> students)
        {
            var table = new Table();
            table.AddColumns(
                new TableColumn("Student ID"),
                new TableColumn("First Name"),
                new TableColumn("Last Name"),
                new TableColumn("Social Security Number")
                );

            foreach (var s in students)
            {
                {
                    table.AddRow(
                        new Text(s.StudentId.ToString()),
                        new Text(s.StudentFörnamn),
                        new Text(s.StudentEfternamn),
                        new Text(s.StudentSsn)
                        );

                }
            }

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
            AnsiConsole.Clear();
        }



        public static void PrintStudentsInClass(List<KlassList> classAndStudentList)
        {
            var table = new Table();
            table.AddColumns(
                new TableColumn("Student ID"),
                new TableColumn("First Name"),
                new TableColumn("Last Name"),
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
                        new Text(cl.FkStudent.StudentSsn),
                        new Text(cl.FkKlass.KlassNamn),
                        new Text(cl.FkKlassId.ToString())
                        );
                }
            }

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
            AnsiConsole.Clear();

        }

        public static void PrintGradesInformation(List<Betyg> grades)
        {
            var table = new Table();
            table.AddColumns(
                new TableColumn("Student First Name"),
                new TableColumn("Student Last Name"),
                new TableColumn("Grade"),
                new TableColumn("Course"),
                new TableColumn("Teacher Grading First Name"),
                new TableColumn("Teacher Grading Last Name"),
                new TableColumn("Date")
                );

            foreach (var g in grades)
            {
                {
                    table.AddRow(
                        new Text(g.FkStudent.StudentFörnamn),
                        new Text(g.FkStudent.StudentEfternamn),
                        new Text(g.Betyg1),
                        new Text(g.FkÄmne.ÄmneNamn),
                        new Text(g.FkPersonal.PersonalFörnamn),
                        new Text(g.FkPersonal.PersonalFörnamn),
                        new Text(g.BetygDatum.ToShortDateString())
                        );
                }
            }

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
            AnsiConsole.Clear();
        }

        public static void PrintCourseInformation(List<CourseInformation> courseInformation)
        {
            var table = new Table();
            table.AddColumns(
                new TableColumn("Course Name"),
                new TableColumn("Average Grade"),
                new TableColumn("Lowest Grade"),
                new TableColumn("Highest Grade")
                );

            foreach (var c in courseInformation)
            {
                {
                    table.AddRow(
                        new Text(c.CourseName),
                        new Text(c.AverageGrade.ToString()),
                        new Text(c.LowestGrade.ToString()),
                        new Text(c.HighestGrade.ToString())
                        );
                }
            }

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
            AnsiConsole.Clear();
        }
    }
}
