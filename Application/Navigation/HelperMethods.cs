using Databaser_Labb3_V2.Models;
using Spectre.Console;

namespace Databaser_Labb3_V2.Application.Navigation;
public static class HelperMethods
{
    public static UserChoice GetUserChoiceFromString(string choice)
    {
        string cleanedChoiceString = Markup.Remove(choice);
        return cleanedChoiceString switch
        {
            "Get Personal" => UserChoice.GetPersonal,
            "Get All Personal" => UserChoice.GetPersonalAll,
            "Get All Teachers" => UserChoice.GetPersonalTeachersOnly,
            "Get All Administrators" => UserChoice.GetPersonalAdminsOnly,
            "Get All Education Leaders" => UserChoice.GetPersonalLeadersOnly,
            "Get Students" => UserChoice.GetStudents,
            "Get All Students" => UserChoice.GetStudentsAll,
            "Get Students By Class" => UserChoice.GetStudentsByClass,
            "Get All Grades From Last Month" => UserChoice.GetGradesLastMonth,
            "Get Grade Info From All Courses" => UserChoice.GetAllCoursesWithGradeInfo,
            "Get Grade Info From Average Age And Gender Groups" => UserChoice.GetAllCoursesWithAverageAgeGender,
            "Add New User" => UserChoice.AddUser,
            "Add New Student" => UserChoice.AddStudent,
            "Add New Personal" => UserChoice.AddPersonal,
            "Database Project Questions" => UserChoice.DatabaseProjectQuestion,
            "Get Department Information (Teachers In Each Department)" => UserChoice.DepartmentInfoTeachers,
            "Get All Info On All Students" => UserChoice.AllStudentInfo,
            "Get All Active Courses" => UserChoice.GetAllActiveCourses,
            "Get Maximum And Average Payout In Each Department" => UserChoice.GetMaxAndAveragePayoutDepartment,
            "Exit" => UserChoice.Exit,
            _ => UserChoice.Invalid
        };
    }
    public static OrderOption GetOrderOptionFromString(string choice)
    {
        string cleanedChoiceString = Markup.Remove(choice);
        return cleanedChoiceString switch
        {
            "Sort By First Name" => OrderOption.FirstName,
            "Sort By Last Name" => OrderOption.LastName,
            "Sort Ascending" => OrderOption.Ascending,
            "Sort Descending" => OrderOption.Descending,
            _ => OrderOption.Invalid
        };
    }
    public static OrderOption GetOrderOptionFirstOrLastName()
    {
        return GetOrderOptionFromString(AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to sort the students by:")
                .AddChoices(new[]
                {
                        "Sort By First Name",
                        "Sort By Last Name"
                })));
    }
    public static OrderOption GetOrderOptionAscendingOrDescending()
    {
        return GetOrderOptionFromString(AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to sort the students:")
                .AddChoices(new[]
                {
                        "Sort Ascending",
                        "Sort Descending"
                })));
    }
    public static Personal CreateNewPersonal()
    {
        var personalFörnamn = AnsiConsole.Ask<string>("Enter the first name of the Personal: ");
        var personalEfternamn = AnsiConsole.Ask<string>("Enter the last name of the Personal: ");
        var personalBefattning = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("What is the personal work title?")
            .AddChoices(new[]
            {
                    "Teacher",
                    "Administrator",
                    "Education Leader"
            }));
        return new Personal
        {
            PersonalNamn = personalFörnamn.Trim() + " " + personalEfternamn.Trim(),
            PersonalFörnamn = personalFörnamn,
            PersonalEfternamn = personalEfternamn,
            PersonalBefattning = (byte)(personalBefattning == "Teacher" ? 1 : personalBefattning == "Administrator" ? 2 : 3),
        };
    }
    public static Studenter CreateNewStudent()
    {
        var studentFörnamn = AnsiConsole.Ask<string>("Enter the first name of the Student: ");
        var studentEfternamn = AnsiConsole.Ask<string>("Enter the last name of the Student: ");
        return new Studenter
        {
            StudentNamn = studentFörnamn.Trim() + " " + studentEfternamn.Trim(),
            StudentFörnamn = studentFörnamn,
            StudentEfternamn = studentEfternamn,
            StudentSsn = AnsiConsole.Prompt(
                         new TextPrompt<string>("Enter the Social Security Number of the Student (10 or 12 numbers): ")
                         .Validate(
                             ssn =>
                                 string.IsNullOrWhiteSpace(ssn)
                                 ? ValidationResult.Error($"[red]Invalid Social Security Number[/]")
                                 : ssn.Length < 10
                                 ? ValidationResult.Error($"[red]Social Security Number must be at least 10 characters long[/]")
                                 : ssn.Length > 12
                                 ? ValidationResult.Error($"[red]Social Security Number must be maximum 12 characters long[/]")
                                 : ssn.Length != 10 || ssn.Length != 12
                                 ? ValidationResult.Error($"[red]Social Security Number must be 10 or 12 numbers[/]")
                                 : ValidationResult.Success()
                         )),
            StudentStartDatum = DateOnly.FromDateTime(DateTime.Now)
        };
    }
}