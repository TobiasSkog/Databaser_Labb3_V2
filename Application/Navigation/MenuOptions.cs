using Spectre.Console;

namespace Databaser_Labb3_V2.Application.Navigation;

public static class MenuOptions
{
    public static UserChoice MainMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
           .Title("What would you like to do?")
           .PageSize(15)
           .AddChoices(new[]
           {
                "Get Personal",
                "Get Students",
                "Get All Grades From Last Month",
                "Get Grade Info From All Courses",
                "Get Grade Info From Average Age And Gender Groups",
                "Add New User",
                "Database Project Questions",
                "Exit"
           }));

        return HelperMethods.GetUserChoiceFromString(choice);
    }

    public static int MainMen2()
    {
        Console.WriteLine("What would you like to do today?");
        Console.WriteLine("1) Get Personal");
        Console.WriteLine("2) Get Students");
        Console.WriteLine("3) Get All Grades From Last Month");
        Console.WriteLine("4) Get Grade Info From All Courses");
        Console.WriteLine("5) Get Grade Info From Average Age And Gender Groups");
        Console.WriteLine("6) Add New User");
        Console.WriteLine("7) Database Project Questions");
        Console.WriteLine("8) Exit");

        var choice = Convert.ToInt32(Console.ReadLine());
        return choice;
    }

    public static int PersonalMenu2()
    {
        Console.WriteLine("What would you like to do today?");
        Console.WriteLine("1) Get All Personal");
        Console.WriteLine("2) Get All Teachers");
        Console.WriteLine("3) Get All Admins");
        Console.WriteLine("8) Back");

        var choice = Convert.ToInt32(Console.ReadLine());
        return choice;
    }

    internal static UserChoice PersonalMenu()
    {
        var choice = AnsiConsole.Prompt(
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
                }));

        return HelperMethods.GetUserChoiceFromString(choice);
    }

    internal static UserChoice StudentMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Get Student Information")
                .PageSize(15)
                .AddChoices(new[]
                {
                    "Get All Students",
                    "Get Students By Class",
                    "Back",
                    "Exit"
                }));
        return HelperMethods.GetUserChoiceFromString(choice);
    }

    internal static UserChoice AddUserMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .PageSize(15)
                .AddChoices(new[]
                {
                    "Add New Student",
                    "Add New Personal",
                    "Back",
                    "Exit"
                }));

        return HelperMethods.GetUserChoiceFromString(choice);
    }

    internal static UserChoice DatabaseProjectMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Get Teacher Information")
                .PageSize(15)
                .AddChoices(new[]
                {
                    "Get Department Information (Teachers In Each Department)",
                    "Get All Info On All Students",
                    "Get All Active Courses",
                    "Get Maximum And Average Payout In Each Department",
                    "Back",
                    "Exit"
                }));

        return HelperMethods.GetUserChoiceFromString(choice);
    }
}
