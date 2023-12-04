using Databaser_Labb3_V2.Application.Navigation;
using Databaser_Labb3_V2.Models;
using Spectre.Console;

namespace Databaser_Labb3_V2.Application;

internal class App
{
    private bool _isAppRunning = true;
    private UserChoice UserChoice { get; set; }
    private EdugradeHighSchoolContext Context { get; set; }

    public App()
    {
        UserChoice = UserChoice.Invalid;
        Context = new();
        Context.UpdateUsersWithFirstAndLastNames();
    }
    public void Run()
    {
        AnsiConsole.Cursor.Hide();

        while (_isAppRunning)
        {
            UserChoice = HelperMethods.GetUserChoiceFromString(AnsiConsole.Prompt(
           new SelectionPrompt<string>()
               .Title("What would you like to do?")
               .PageSize(15)
               .AddChoices(new[]
               {
                        "Get Personal",
                        "Get Students",
                        "Get All Grades From Last Month",
                        "Get Grade Info From All Courses",
                        "Add New User",
                        "Exit"
               })));

            switch (UserChoice)
            {
                case UserChoice.GetPersonal:
                    var personalChoice = HelperMethods.GetUserChoiceFromString(AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Get Teacher Information")
                            .PageSize(15)
                            .AddChoices(new[]
                            {
                                "Get All Personal",
                                "Get All Teachers",
                                "Get All Administrators",
                                "Get All Education Leaders",
                                "Back",
                                "Exit"
                            })));

                    switch (personalChoice)
                    {
                        case UserChoice.GetPersonalAll:
                            PrintQueries.PrintPersonalInformation(Context.GetAllPersonal());
                            break;

                        case UserChoice.GetPersonalTeachersOnly:
                            PrintQueries.PrintPersonalInformation(Context.GetAllPersonalsByRole(UserType.Teacher));
                            break;

                        case UserChoice.GetPersonalAdminsOnly:
                            PrintQueries.PrintPersonalInformation(Context.GetAllPersonalsByRole(UserType.Admin));
                            break;

                        case UserChoice.GetPersonalLeadersOnly:
                            PrintQueries.PrintPersonalInformation(Context.GetAllPersonalsByRole(UserType.EducationLeader));
                            break;

                        case UserChoice.Exit:
                            Exit();
                            break;
                    }
                    break;

                case UserChoice.GetStudents:
                    var studentChoice = HelperMethods.GetUserChoiceFromString(AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Get Student Information")
                            .PageSize(15)
                            .AddChoices(new[]
                            {
                                "Get All Students",
                                "Get Students By Class",
                                "Back",
                                "Exit"
                            })));
                    switch (studentChoice)
                    {
                        case UserChoice.GetStudentsAll:
                            PrintQueries.PrintStudentInformationAsTable(Context.GetAllStudents());
                            break;

                        case UserChoice.GetStudentsByClass:

                            string className = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("From which class?")
                                    .AddChoices(Context.GetAllClassNames()));
                            var nameSorting = HelperMethods.GetOrderOptionFirstOrLastName();
                            var ascOrDesc = HelperMethods.GetOrderOptionAscendingOrDescending();

                            PrintQueries.PrintStudentsInClass(Context.GetAllStudentsInClass(className, nameSorting, ascOrDesc));
                            break;

                        case UserChoice.Exit:
                            Exit();
                            break;
                    }
                    break;

                case UserChoice.AddUser:
                    var addUserChoice = HelperMethods.GetUserChoiceFromString(AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("What would you like to do?")
                            .PageSize(15)
                            .AddChoices(new[]
                            {
                                "Add New Student",
                                "Add New Personal",
                                "Back",
                                "Exit"
                            })));
                    switch (addUserChoice)
                    {
                        case UserChoice.AddStudent:
                            var newStudent = HelperMethods.CreateNewStudent();
                            Context.AddStudentToDB(newStudent);
                            break;

                        case UserChoice.AddPersonal:
                            var newPersonal = HelperMethods.CreateNewPersonal();
                            Context.AddPersonalToDB(newPersonal);
                            break;

                        case UserChoice.Exit:
                            Exit();
                            break;
                    }
                    break;

                case UserChoice.GetGradesLastMonth:
                    PrintQueries.PrintGradesInformation(Context.GetAllGradesLastMonth());
                    break;

                case UserChoice.GetAllCoursesWithGradeInfo:
                    PrintQueries.PrintCourseInformation(Context.GetCourseInformation());
                    break;

                case UserChoice.Exit:
                    Exit();
                    break;

                default:
                    break;
            }
        }
    }
    public void Exit()
    {
        _isAppRunning = false;
        UserChoice = UserChoice.Exit;
        AnsiConsole.Cursor.Show();

    }
}
