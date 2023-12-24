using Databaser_Labb3_V2.Application.Navigation;
using Databaser_Labb3_V2.Models;
using Databaser_Labb3_V2.Repositories;
using Spectre.Console;

namespace Databaser_Labb3_V2.Application;

public class App
{
    private bool _isAppRunning = true;
    private UserChoice UserChoice { get; set; }
    private IRepository Repository { get; set; }

    public App(IRepository repository)
    {
        UserChoice = UserChoice.Invalid;
        Repository = repository;
    }
    public async Task Run()
    {


        AnsiConsole.Cursor.Hide();

        while (_isAppRunning)
        {
            UserChoice = MenuOptions.MainMenu();

            switch (UserChoice)
            {
                case UserChoice.GetPersonal:

                    var personalChoice = MenuOptions.PersonalMenu();

                    switch (personalChoice)
                    {
                        case UserChoice.GetPersonalAll:
                            List<Personal> allPersonal = Repository.GetAllPersonal().Result;
                            PrintQueries.PrintPersonalInformation(allPersonal);
                            break;

                        case UserChoice.GetPersonalTeachersOnly:
                            PrintQueries.PrintPersonalInformation(Repository.GetAllPersonalsByRole(UserType.Teacher).Result);
                            break;

                        case UserChoice.GetPersonalAdminsOnly:
                            PrintQueries.PrintPersonalInformation(Repository.GetAllPersonalsByRole(UserType.Admin).Result);
                            break;

                        case UserChoice.GetPersonalLeadersOnly:
                            PrintQueries.PrintPersonalInformation(Repository.GetAllPersonalsByRole(UserType.EducationLeader).Result);
                            break;

                        case UserChoice.Exit:
                            Exit();
                            break;
                    }
                    break;

                case UserChoice.GetStudents:
                    var studentChoice = MenuOptions.StudentMenu();
                    switch (studentChoice)
                    {
                        case UserChoice.GetStudentsAll:
                            PrintQueries.PrintStudentInformationAsTable(Repository.GetAllStudents().Result);
                            break;

                        case UserChoice.GetStudentsByClass:

                            string className = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                .Title("From which class?")
                                .AddChoices(Repository.GetAllClassNames().Result));

                            var nameSorting = HelperMethods.GetOrderOptionFirstOrLastName();
                            var ascOrDesc = HelperMethods.GetOrderOptionAscendingOrDescending();

                            PrintQueries.PrintStudentsInClass(Repository.GetAllStudentsInClass(className, nameSorting, ascOrDesc).Result);
                            break;

                        case UserChoice.Exit:
                            Exit();
                            break;
                    }
                    break;

                case UserChoice.AddUser:
                    var addUserChoice = MenuOptions.AddUserMenu();
                    switch (addUserChoice)
                    {
                        case UserChoice.AddStudent:
                            var newStudent = HelperMethods.CreateNewStudent();
                            await Repository.AddStudentToDB(newStudent);
                            break;

                        case UserChoice.AddPersonal:
                            var newPersonal = HelperMethods.CreateNewPersonal();
                            await Repository.AddPersonalToDB(newPersonal);
                            break;

                        case UserChoice.Exit:
                            Exit();
                            break;
                    }
                    break;

                case UserChoice.GetGradesLastMonth:
                    PrintQueries.PrintGradesInformation(Repository.GetAllGradesLastMonth().Result);
                    break;

                case UserChoice.GetAllCoursesWithGradeInfo:
                    await Repository.GetCourseInformation();
                    Console.ReadKey();
                    PrintQueries.PrintCourseInformation(Repository.GetCourseInformation().Result);
                    break;

                case UserChoice.GetAllCoursesWithAverageAgeGender:
                    PrintQueries.PrintAverageGradeByGenderAndAgeGroup(Repository.GetAverageGradesBasedByAgeAndGender().Result);
                    break;

                case UserChoice.DatabaseProjectQuestion:
                    var databaseProjectChoice = MenuOptions.DatabaseProjectMenu();

                    switch (databaseProjectChoice)
                    {
                        case UserChoice.DepartmentInfoTeachers:
                            PrintQueries.PrintAmountOfTeachersInEachDepartMent(Repository.GetTeachersInEveryDepartMent().Result);
                            break;

                        case UserChoice.AllStudentInfo:
                            PrintQueries.PrintAllStudentInfo(Repository.GetAllStudentInfo().Result);
                            break;

                        case UserChoice.GetAllActiveCourses:
                            PrintQueries.PrintActiveCourses(Repository.GetAllActiveCourses().Result);
                            break;

                        case UserChoice.GetMaxAndAveragePayoutDepartment:
                            PrintQueries.PrintMaxAndAveragePayoutDepartmentInfo(Repository.GetDepartmentPayoutInformation().Result);
                            break;

                        case UserChoice.Exit:
                            Exit();
                            break;
                    }

                    break;

                case UserChoice.Exit:
                    Exit();
                    break;
            }
        }
    }
    private void Exit()
    {
        _isAppRunning = false;
        UserChoice = UserChoice.Exit;
        AnsiConsole.Cursor.Show();
    }
}
