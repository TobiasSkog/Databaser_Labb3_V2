﻿using Databaser_Labb3_V2.Models;
using Spectre.Console;

namespace Databaser_Labb3_V2.Application.Navigation
{
    internal static class HelperMethods
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
                "Add New User" => UserChoice.AddUser,
                "Add New Student" => UserChoice.AddStudent,
                "Add New Personal" => UserChoice.AddPersonal,
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
                StudentSsn = AnsiConsole.Ask<string>("Enter the Social Security Number of the Student: ")
            };
        }
    }
}
